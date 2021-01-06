using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TfsDeploymentChecker.BusinessLogic.Abstractions;
using TfsDeploymentChecker.Models.Cache;
using TfsDeploymentChecker.Models.Exceptions;
using TfsDeploymentChecker.Models.Internal;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class HealthCheckPerformer : IHealthCheckPerformer
    {
        public HealthCheckPerformer (
            ITfsClient tfsClient,
            IConfigurationRetriever configurationRetriever,
            IMemoryCache memoryCache)
        {
            _tfsClient = tfsClient;
            _configurationRetriever = configurationRetriever;
            _memoryCache = memoryCache;
        }

        private ITfsClient _tfsClient { get; }

        private IConfigurationRetriever _configurationRetriever { get; set; }

        private IMemoryCache _memoryCache { get; set; }

        public async Task<bool> IsHealthy ()
        {
            var isElementPresentInCache = _memoryCache.TryGetValue(CacheKeys.HealthCheckResult, out IEnumerable<string> healthCheckDetails);

            if (isElementPresentInCache)
            {
                if (healthCheckDetails == null || healthCheckDetails.Count() == 0)
                {
                    return true;
                }
                else
                {
                    throw new CustomBlazorException(string.Join(Environment.NewLine, healthCheckDetails));
                }
            }

            var currentHealthCheckDetails = new List<string>();
            if (string.IsNullOrEmpty(_configurationRetriever.TfsToken))
            {
                currentHealthCheckDetails.Add("The value for the app setting Token cannot be nor null nor empty.");
            }

            await CheckUrlAndAppendErrorIfNeeded(
                UrlUtils.GetProjectCollectionsUrl(_configurationRetriever.TfsUrl, _configurationRetriever.TfsApiVersion), 
                $"The url {_configurationRetriever.TfsUrl} was not reachable", 
                currentHealthCheckDetails);

            foreach (var teamProjectAndReleaseIdsPairs in _configurationRetriever.TeamProjectsAndReleaseIdsPairs)
            {
                await CheckUrlAndAppendErrorIfNeeded(
                    UrlUtils.GetProjectDefinitionUrl(
                        _configurationRetriever.TfsUrl,
                        teamProjectAndReleaseIdsPairs.TfsTeamProjectName,
                        _configurationRetriever.TfsApiVersion),
                    $"The team project {teamProjectAndReleaseIdsPairs.TfsTeamProjectName} was not reachable",
                    currentHealthCheckDetails);
                foreach (var projectReleaseDefinitionId in teamProjectAndReleaseIdsPairs.ProjectReleaseDefinitionIds)
                {
                    await CheckUrlAndAppendErrorIfNeeded(
                        UrlUtils.GetReleaseDefinitionUrl(
                            _configurationRetriever.TfsUrl,
                            teamProjectAndReleaseIdsPairs.TfsTeamProjectName,
                            projectReleaseDefinitionId,
                            _configurationRetriever.TfsApiVersion),
                        $"The release definiton with id {projectReleaseDefinitionId} in the project {teamProjectAndReleaseIdsPairs.TfsTeamProjectName} was not reachable",
                        currentHealthCheckDetails);
                }
            }

            if (_configurationRetriever.SystemsOfInterest == null || _configurationRetriever.SystemsOfInterest.Count() == 0)
            {
                currentHealthCheckDetails.Add("At least one entry in the app setting SystemsOfInterest must be specific (as a csv list)");
            }

            if (_configurationRetriever.HealthCheckIntervalInSeconds < 0)
            {
                currentHealthCheckDetails.Add("The value in the app setting for HealthCheckIntervalInSeconds must must be >= 0");
            }

            CheckTfsApiVersionAndAppendErrorIfNeeded(currentHealthCheckDetails);

            _memoryCache.Set(CacheKeys.HealthCheckResult, currentHealthCheckDetails, new TimeSpan(0, 0, 20));
            if (currentHealthCheckDetails.Count() > 0)
            {
                throw new CustomBlazorException(string.Join(Environment.NewLine, currentHealthCheckDetails));
            }

            return true;
        }

        public async Task CheckUrlAndAppendErrorIfNeeded (string urlToCheck, string errorMessageToAppendOnError, List<string> currentHealthCheckDetails)
        {
            try
            {
                using (var response = await _tfsClient.GetClient().GetAsync(urlToCheck))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        currentHealthCheckDetails.Add(errorMessageToAppendOnError);
                    }
                }
            }
            catch
            {
                currentHealthCheckDetails.Add(errorMessageToAppendOnError);
            }
        }

        private void CheckTfsApiVersionAndAppendErrorIfNeeded (List<string> currentHealthCheckDetails)
        {
            string apiVersionStringWithoutPreviewsSuffix;
            if (_configurationRetriever.TfsApiVersion.Contains("preview"))
            {
                apiVersionStringWithoutPreviewsSuffix = _configurationRetriever.TfsApiVersion.Substring(0, _configurationRetriever.TfsApiVersion.IndexOf("preview") + 1);
            }
            else
            {
                apiVersionStringWithoutPreviewsSuffix = _configurationRetriever.TfsApiVersion;
            }

            var isParsedActualApiVersionValid = double.TryParse(apiVersionStringWithoutPreviewsSuffix, out var parsedActualApiVersion);
            if (!isParsedActualApiVersionValid)
            {
                currentHealthCheckDetails.Add($"The value in the app setting for TfsApiVersion ({_configurationRetriever.TfsApiVersion}) is not valid " +
                    $"as it cannot be parsed onto a double value.");
            }

            if (parsedActualApiVersion == AllowedTfsApiVersions.FourPointZero)
            {
                if (apiVersionStringWithoutPreviewsSuffix != _configurationRetriever.TfsApiVersion)
                { 
                    currentHealthCheckDetails.Add($"The value in the app setting for TfsApiVersion ({_configurationRetriever.TfsApiVersion}) is not valid: " +
                        $"the first supported version is 4.0 stable (preview versions not supported on 4.0)");
                }
            }
            else if (parsedActualApiVersion < AllowedTfsApiVersions.FourPointZero)
            {
                currentHealthCheckDetails.Add($"The value in the app setting for TfsApiVersion ({_configurationRetriever.TfsApiVersion}) is not valid: " +
                    $"the first supported version is 4.0 stable (preview versions not supported on 4.0)");
            }
        }
    }
}
