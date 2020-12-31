using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TfsDeploymentChecker.BusinessLogic.Abstractions;
using TfsDeploymentChecker.Models.Tfs;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class ReleaseEnvironmentsGetter : IReleaseEnvironmentsGetter
    {
        public ReleaseEnvironmentsGetter(ITfsClient tfsClient)
        {
            _tfsClient = tfsClient;
        }

        private ITfsClient _tfsClient { get; }

        public async Task<Dictionary<int, string>> GetEnvironmentsForRelease(string tfsUrl, string tfsTeamProjectName, int releaseDefinitionId)
        {
            var urlToCall = $"{tfsUrl}/{tfsTeamProjectName}/_apis/release/definitions/{releaseDefinitionId}?api-version=4.0";

            var envIdEnvNameDictionary = new Dictionary<int, string>();
            ReleaseDefinition releaseDefinition = null;

            using (var response = await _tfsClient.GetClient().GetAsync(urlToCall))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                releaseDefinition = JsonSerializer.Deserialize<ReleaseDefinition>(responseBody);
            }

            foreach (var environment in releaseDefinition.environments)
            {
                envIdEnvNameDictionary.Add(environment.id, environment.name);
            }

            return envIdEnvNameDictionary;
        }
    }
}
