using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TfsDeploymentChecker.BusinessLogic.Abstractions;
using TfsDeploymentChecker.Models.Internal;
using TfsDeploymentChecker.Models.Tfs;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class EnvironmentDeployedGetter : IEnvironmentDeployedGetter
    {
        private string queryOrder = "descending";
        
        private int digitsOfInterest = 7;

        public EnvironmentDeployedGetter (
            ITfsClient tfsClient,
            IConfigurationRetriever configurationRetriever)
        {
            _tfsClient = tfsClient;
            _configurationRetriever = configurationRetriever;
        }
        
        private ITfsClient _tfsClient { get; }

        private IConfigurationRetriever _configurationRetriever { get; set; }
        
        public async Task<DeploymentResult> GetReleasedEnvironmentInformation (string projectCollection, int releaseId, int environmentId)
        {
            var tfsUrl = _configurationRetriever.TfsUrl;
            var tfsApiVersion = _configurationRetriever.TfsApiVersion;

            var urlToCall = UrlUtils.GetReleasedEnvironmentUrl(tfsUrl, projectCollection, releaseId, environmentId, queryOrder, tfsApiVersion);
            EnvironmentDeployedExecution environmentDeployedExecution = null;

            using (var response = await _tfsClient.GetClient().GetAsync(urlToCall))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                environmentDeployedExecution = JsonSerializer.Deserialize<EnvironmentDeployedExecution>(responseBody);
            }

            var currentAttempt = environmentDeployedExecution.value.Length - 1;
            var releaseArtifacts = environmentDeployedExecution.value[currentAttempt].release.artifacts;
            var currentDefinitionReference = releaseArtifacts[releaseArtifacts.ToList()
                .IndexOf(releaseArtifacts.ToList().FirstOrDefault(artifact => artifact.isPrimary) ?? releaseArtifacts.ToList().First())]
                    .definitionReference;
            var commitIdOrChangeSetId = GetCommitIdUpToXDigitsOrChangeSetId(currentDefinitionReference.sourceVersion, digitsOfInterest);
            var codeVersionUrl = GetCodeVersionUrl(tfsUrl, projectCollection, currentDefinitionReference, commitIdOrChangeSetId);
            var repositoryName = GetRepositoryName(currentDefinitionReference);

            var deploymentResult = new DeploymentResult()
            {
                BranchName = currentDefinitionReference.branch != null ? currentDefinitionReference.branch.name.Replace(@"refs/heads/", string.Empty) : "$/",
                DefinitionName = currentDefinitionReference.definition.name,
                DeploymentStatus = environmentDeployedExecution.value[currentAttempt].deploymentStatus,
                EnvironmentName = environmentDeployedExecution.value[currentAttempt].releaseEnvironment.name,
                RepositoryName = repositoryName,
                EnvironmentId = environmentDeployedExecution.value[currentAttempt].releaseEnvironment.id,
                PackageUrl = currentDefinitionReference.artifactSourceVersionUrl.id,
                PackageVersion = currentDefinitionReference.version.name,
                ReleaseName = environmentDeployedExecution.value[currentAttempt].releaseDefinition.name,
                ReleaseUrl = environmentDeployedExecution.value[currentAttempt].release.webAccessUri,
                CompletedOn = environmentDeployedExecution.value[currentAttempt].completedOn,
                StartedOn = environmentDeployedExecution.value[currentAttempt].startedOn,
                CommitIdOrChangeSetId = commitIdOrChangeSetId,
                CodeVersionUrl = codeVersionUrl,
                AttemptNumber = currentAttempt + 1
            };

            return deploymentResult;
        }

        private string GetRepositoryName (Definitionreference currentDefinitionReference)
        {
            if (currentDefinitionReference.repositoryProvider != null &&
                currentDefinitionReference.repositoryProvider.id == SourceControlSystems.TFS_VERSION_CONTROL)
            {
                return currentDefinitionReference.project.name;
            }

            if (currentDefinitionReference.repository == null)
            { 
                return "Missing Information";
            }

            return currentDefinitionReference.repository.name;
        }

        private string GetCodeVersionUrl (string tfsUrl, string projectCollection, Definitionreference definitionReference, string commitId)
        {
            if (definitionReference.repositoryProvider != null && definitionReference.repositoryProvider.id == SourceControlSystems.TFS_VERSION_CONTROL)
            {
                return UrlUtils.GetTfsVersionControlChangesetUrl(tfsUrl, projectCollection, definitionReference);
            }

            return UrlUtils.GetGitCommitUrl(tfsUrl, projectCollection, definitionReference);
        }

        private string GetCommitIdUpToXDigitsOrChangeSetId (SourceVersion sourceVersion, int digitsOfInterest)
        {
            if (sourceVersion == null)
            {
                return null;
            }

            if (sourceVersion.id.StartsWith("C"))
            {
                sourceVersion.id = sourceVersion.id.Substring(1, sourceVersion.id.Length - 1);
            }

            var sourceIdLength = sourceVersion.id.Length;

            return sourceVersion.id.Substring(0, Math.Min(digitsOfInterest, sourceIdLength));
        }
    }
}
