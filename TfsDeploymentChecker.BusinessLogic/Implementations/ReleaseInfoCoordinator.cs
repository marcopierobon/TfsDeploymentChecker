using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TfsDeploymentChecker.BusinessLogic.Abstractions;
using TfsDeploymentChecker.Models.Internal;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class ReleaseInfoCoordinator : IReleaseInfoCoordinator
    {
        public ReleaseInfoCoordinator(
                IReleaseEnvironmentsGetter releaseEnvironmentsGetter, 
                IEnvironmentDeployedGetter environmentDeployedGetter,
                IConfiguration configuration)
        {
            _releaseEnvironmentsGetter = releaseEnvironmentsGetter;
            _environmentDeployedGetter = environmentDeployedGetter;
            _tfsUrl = configuration.GetValue<string>("TfsUrl");
            _systemsOfInterest = configuration.GetValue<string>("SystemsOfInterest").Split(',').ToList();
            var releasesForTeamProjectsStr = configuration.GetValue<string>("TeamProjectsAndReleaseIdsPairs");
            _teamProjectsAndReleaseIdsPairs = JsonSerializer.Deserialize<IEnumerable<ReleasesForTeamProject>>(releasesForTeamProjectsStr);
            _targetSystems = new List<TargetSystem>();
        }

        private IEnumerable<ReleasesForTeamProject> _teamProjectsAndReleaseIdsPairs { get; set; }

        private string _tfsUrl { get; set; }

        private IEnumerable<string> _systemsOfInterest { get; set; }

        private Dictionary<int, string> _envIdEnvNameDictionary { get; set; }

        private IList<TargetSystem> _targetSystems { get; set; }

        private IReleaseEnvironmentsGetter _releaseEnvironmentsGetter { get; set; }

        private IEnvironmentDeployedGetter _environmentDeployedGetter { get; }

        public async Task<IEnumerable<TargetSystem>> Get()
        {
            foreach (var systemOfInterest in _systemsOfInterest)
            {
                _targetSystems.Add(new TargetSystem()
                {
                    TargetSystemName = systemOfInterest,
                    ReleasedEnvironments = new List<TargetDeployedApplication>()
                });
            }

            foreach (var releasesForTeamProject in _teamProjectsAndReleaseIdsPairs)
            {
                foreach (var releaseDefinitionId in releasesForTeamProject.ProjectReleaseDefinitionIds)
                {
                    _envIdEnvNameDictionary = await _releaseEnvironmentsGetter.GetEnvironmentsForRelease(_tfsUrl, releasesForTeamProject.TfsTeamProjectName, releaseDefinitionId);

                    foreach (var envIdEnvNameCouple in _envIdEnvNameDictionary)
                    {
                        var matchingEnv = _targetSystems.Where(targetSystem =>
                            envIdEnvNameCouple.Value.ToUpperInvariant().StartsWith(targetSystem.TargetSystemName.ToUpperInvariant()));
                        if (matchingEnv.Count() > 1)
                        {
                            throw new InvalidOperationException($"There are multiple target environments matching the name " +
                                $"{envIdEnvNameCouple.Value.ToUpperInvariant()}." +
                                $"Please use more specific environments names in the release definition.");
                        }
                        else if (matchingEnv.Count() == 1)
                        {
                            matchingEnv.FirstOrDefault().ReleasedEnvironments.Add(new TargetDeployedApplication()
                            {
                                EnvironmentId = envIdEnvNameCouple.Key,
                                EnvironmentName = envIdEnvNameCouple.Value,
                                ReleaseDefinitionId = releaseDefinitionId
                            });
                        }
                    }
                }
            }

            foreach (var targetSystem in _targetSystems)
            {
                foreach (var releasedEnvironment in targetSystem.ReleasedEnvironments)
                {
                    var targetProjectName = _teamProjectsAndReleaseIdsPairs.FirstOrDefault(releaseForTeamProjects => releaseForTeamProjects.ProjectReleaseDefinitionIds.Contains(releasedEnvironment.ReleaseDefinitionId)).TfsTeamProjectName;
                    releasedEnvironment.DeploymentResult = await _environmentDeployedGetter.GetReleasedEnvironmentInformation(_tfsUrl, targetProjectName, releasedEnvironment.ReleaseDefinitionId, releasedEnvironment.EnvironmentId);
                }
            }

            return _targetSystems;
        }
    }
}
