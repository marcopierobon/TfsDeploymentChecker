using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TfsDeploymentChecker.BusinessLogic.Abstractions;
using TfsDeploymentChecker.Models.AppSetting;
using TfsDeploymentChecker.Models.ErrorCodes;
using TfsDeploymentChecker.Models.Exceptions;
using TfsDeploymentChecker.Models.Internal;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class ConfigurationRetriever : IConfigurationRetriever
    {
        public ConfigurationRetriever (IConfiguration configuration)
        {
            var systemsOfInterestConfigValue = EnsureParameterIsPresentAndReturnIt<string>(
                configuration, 
                AppSettingEntries.SYSTEMS_OF_INTEREST, 
                ConfigurationErrorCodes.MISSING_SYSTEMS_OF_INTEREST);
            
            SystemsOfInterest = systemsOfInterestConfigValue.Replace(" ", string.Empty).Split(',').ToList();
            var teamProjectsAndReleaseIdsPairsConfigValue = EnsureParameterIsPresentAndReturnIt<string>(
                configuration, 
                AppSettingEntries.TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS, 
                ConfigurationErrorCodes.MISSING_TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS);

            try
            {
                TeamProjectsAndReleaseIdsPairs = 
                    JsonSerializer.Deserialize<IEnumerable<ReleasesForTeamProject>>(teamProjectsAndReleaseIdsPairsConfigValue);
            }
            catch (JsonException jsonEx)
            { 
                throw new InvalidConfigurationException(
                    jsonEx,
                    ConfigurationErrorCodes.INVALID_JSON_MODEL,
                    $"The {AppSettingEntries.TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS} cannot be parsed onto a valid json model");
            }

            if (TeamProjectsAndReleaseIdsPairs.Any(teamProjectsAndReleaseIdsPair => 
                string.IsNullOrEmpty(teamProjectsAndReleaseIdsPair.TfsTeamProjectName) ||
                teamProjectsAndReleaseIdsPair.ProjectReleaseDefinitionIds == null || teamProjectsAndReleaseIdsPair.ProjectReleaseDefinitionIds.Count == 0))
            {
                throw new InvalidConfigurationException(
                    ConfigurationErrorCodes.INVALID_JSON_MODEL,
                    $"The {AppSettingEntries.TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS} cannot be parsed onto a valid json model");
            }

            TfsApiVersion = EnsureParameterIsPresentAndReturnIt<string>(
                configuration, 
                AppSettingEntries.TFS_API_VERSION, 
                ConfigurationErrorCodes.MISSING_TFS_API_VERSION);
                
            TfsUrl = EnsureParameterIsPresentAndReturnIt<string>(
                configuration, 
                AppSettingEntries.TFS_URL, 
                ConfigurationErrorCodes.MISSING_TFS_URL);

            HealthCheckIntervalInSeconds = EnsureParameterIsPresentAndReturnIt<int>(
                configuration, 
                AppSettingEntries.HEALTHCHECK_INTERVAL_IN_SECONDS, 
                ConfigurationErrorCodes.MISSING_HEALTHCHECK_INTERVAL_IN_SECONDS);

            TfsToken = EnsureParameterIsPresentAndReturnIt<string>(
                configuration, 
                AppSettingEntries.TFS_TOKEN, 
                ConfigurationErrorCodes.MISSING_TFS_TOKEN);

            AllowUntrustedSslCertificates = EnsureParameterIsPresentAndReturnIt<bool>(
                configuration, 
                AppSettingEntries.ALLOW_UNTRUSTED_SSL_CERTIFICATES, 
                ConfigurationErrorCodes.MISSING_ALLOW_UNTRUSTED_SSL_CERTIFICATES);
        }
        
        public IEnumerable<string> SystemsOfInterest { get; set; }

        public IEnumerable<ReleasesForTeamProject> TeamProjectsAndReleaseIdsPairs { get; set; }

        public string TfsUrl { get; set; }

        public int HealthCheckIntervalInSeconds { get; set; }
        
        public string TfsToken { get; set; }
        
        public string TfsApiVersion { get; set; }

        public bool AllowUntrustedSslCertificates { get; set; }

        private T EnsureParameterIsPresentAndReturnIt<T>(IConfiguration configuration, string appSettingKey, string errorCode) 
        {
            var appSettingValue = configuration.GetValue<T>(appSettingKey);

            if (appSettingValue == null || appSettingValue.ToString() == string.Empty)
            {
                throw new InvalidConfigurationException(
                    errorCode,
                    $"The {appSettingKey} setting key is missing or empty in the config file");
            }

            return appSettingValue;
        }
    }
}
