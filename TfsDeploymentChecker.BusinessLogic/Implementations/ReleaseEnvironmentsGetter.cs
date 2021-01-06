using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TfsDeploymentChecker.BusinessLogic.Abstractions;
using TfsDeploymentChecker.Models.Tfs;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class ReleaseEnvironmentsGetter : IReleaseEnvironmentsGetter
    {
        public ReleaseEnvironmentsGetter(
            ITfsClient tfsClient,
            IConfigurationRetriever configurationRetriever)
        {
            _tfsClient = tfsClient;
            _configurationRetriever = configurationRetriever;
        }

        private ITfsClient _tfsClient { get; }

        private IConfigurationRetriever _configurationRetriever { get; set; }

        public async Task<Dictionary<int, string>> GetEnvironmentsForRelease(string tfsTeamProjectName, int releaseDefinitionId)
        {
            var urlToCall = UrlUtils.GetReleaseDefinitionUrl(_configurationRetriever.TfsUrl, tfsTeamProjectName, releaseDefinitionId, _configurationRetriever.TfsApiVersion);

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
