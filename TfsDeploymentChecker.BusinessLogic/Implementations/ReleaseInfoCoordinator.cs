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
                IConfigurationRetriever configurationRetriever)
        {
            _releaseEnvironmentsGetter = releaseEnvironmentsGetter;
            _environmentDeployedGetter = environmentDeployedGetter;
            _configurationRetriever = configurationRetriever;
            _targetSystems = new List<TargetSystem>();
        }

        private Dictionary<int, string> _envIdEnvNameDictionary { get; set; }

        private IList<TargetSystem> _targetSystems { get; set; }

        private IReleaseEnvironmentsGetter _releaseEnvironmentsGetter { get; set; }

        private IEnvironmentDeployedGetter _environmentDeployedGetter { get; }

        private IConfigurationRetriever _configurationRetriever { get; set; }

        public async Task<IEnumerable<TargetSystem>> Get()
        {
            foreach (var systemOfInterest in _configurationRetriever.SystemsOfInterest)
            {
                _targetSystems.Add(new TargetSystem()
                {
                    TargetSystemName = systemOfInterest,
                    ReleasedEnvironments = new List<TargetDeployedApplication>()
                });
            }

            foreach (var releasesForTeamProject in _configurationRetriever.TeamProjectsAndReleaseIdsPairs)
            {
                foreach (var releaseDefinitionId in releasesForTeamProject.ProjectReleaseDefinitionIds)
                {
                    _envIdEnvNameDictionary = await _releaseEnvironmentsGetter.GetEnvironmentsForRelease(releasesForTeamProject.TfsTeamProjectName, releaseDefinitionId);

                    foreach (var envIdEnvNameCouple in _envIdEnvNameDictionary)
                    {
                        var matchingEnv = _targetSystems.Where(targetSystem =>
                            envIdEnvNameCouple.Value.ToUpperInvariant().Contains(targetSystem.TargetSystemName.ToUpperInvariant()));
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
                    var targetProjectName = _configurationRetriever.TeamProjectsAndReleaseIdsPairs.FirstOrDefault(releaseForTeamProjects => releaseForTeamProjects.ProjectReleaseDefinitionIds.Contains(releasedEnvironment.ReleaseDefinitionId)).TfsTeamProjectName;
                    releasedEnvironment.DeploymentResult = await _environmentDeployedGetter.GetReleasedEnvironmentInformation(targetProjectName, releasedEnvironment.ReleaseDefinitionId, releasedEnvironment.EnvironmentId);
                }
            }

            return _targetSystems;
        }
    }
}
