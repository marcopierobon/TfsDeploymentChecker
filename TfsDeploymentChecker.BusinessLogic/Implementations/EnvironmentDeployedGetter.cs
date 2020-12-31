using System.Text.Json;
using System.Threading.Tasks;
using TfsDeploymentChecker.BusinessLogic.Abstractions;
using TfsDeploymentChecker.Models.Internal;
using TfsDeploymentChecker.Models.Tfs;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class EnvironmentDeployedGetter : IEnvironmentDeployedGetter
    {
        public EnvironmentDeployedGetter (ITfsClient tfsClient)
        {
            _tfsClient = tfsClient;
        }

        private ITfsClient _tfsClient { get; }

        public async Task<DeploymentResult> GetReleasedEnvironmentInformation (string tfsUrl, string projectCollection, int releaseId, int environmentId)
        {
            var queryOrder = "descending";
            var urlToCall = $"{tfsUrl}/{projectCollection}/_apis/release/deployments?definitionId={releaseId}&definitionEnvironmentId={environmentId}&queryOrder={queryOrder}&$top=1&api-version=4.0";
            EnvironmentDeployedExecution environmentDeployedExecution = null;

            using (var response = await _tfsClient.GetClient().GetAsync(urlToCall))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                environmentDeployedExecution = JsonSerializer.Deserialize<EnvironmentDeployedExecution>(responseBody);
            }

            var deploymentResult = new DeploymentResult()
            {
                BranchName = environmentDeployedExecution.value[0].release.artifacts[0].definitionReference.branch.name.Replace(@"refs/heads/", string.Empty),
                DefinitionName = environmentDeployedExecution.value[0].release.artifacts[0].definitionReference.definition.name,
                DeploymentStatus = environmentDeployedExecution.value[0].deploymentStatus,
                EnvironmentName = environmentDeployedExecution.value[0].releaseEnvironment.name,
                EnvironmentId = environmentDeployedExecution.value[0].releaseEnvironment.id,
                PackageUrl = environmentDeployedExecution.value[0].release.artifacts[0].definitionReference.artifactSourceVersionUrl.id,
                PackageVersion = environmentDeployedExecution.value[0].release.artifacts[0].definitionReference.version.name,
                ReleaseName = environmentDeployedExecution.value[0].releaseDefinition.name,
                ReleaseUrl = environmentDeployedExecution.value[0].release.webAccessUri,
                CompletedOn = environmentDeployedExecution.value[0].completedOn,
                StartedOn = environmentDeployedExecution.value[0].startedOn
            };

            return deploymentResult;
        }
    }
}
