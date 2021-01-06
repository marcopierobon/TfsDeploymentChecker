using TfsDeploymentChecker.Models.Tfs;

namespace TfsDeploymentChecker.BusinessLogic
{
    public static class UrlUtils
    {
        public static string GetReleaseDefinitionUrl (string tfsUrl, string tfsTeamProjectName, int releaseDefinitionId, string tfsApiVersion)
        {
            return $"{tfsUrl}/{tfsTeamProjectName}/_apis/release/definitions/{releaseDefinitionId}?api-version={tfsApiVersion}";
        }

        public static string GetProjectDefinitionUrl (string tfsUrl, string tfsTeamProjectName, string tfsApiVersion)
        {
            return $"{tfsUrl}/_apis/projects/{tfsTeamProjectName}?api-version={tfsApiVersion}";
        }

        public static string GetProjectCollectionsUrl (string tfsUrl, string tfsApiVersion)
        {
            return $"{tfsUrl}/_apis/projects?api-version={tfsApiVersion}&top=1";
        }

        public static string GetReleasedEnvironmentUrl (string tfsUrl, string projectCollection, int releaseId, int environmentId, string queryOrder, string tfsApiVersion)
        {
            return $"{tfsUrl}/{projectCollection}/_apis/release/deployments?definitionId={releaseId}&definitionEnvironmentId={environmentId}&queryOrder={queryOrder}&$top=1&api-version={tfsApiVersion}";
        }

        public static string GetTfsVersionControlChangesetUrl (string tfsUrl, string projectCollection, Definitionreference definitionReference)
        {
            if (definitionReference.sourceVersion == null)
            {
                return $"{tfsUrl}/{projectCollection}/_versionControl";
            }

            return $"{tfsUrl}/{projectCollection}/_versionControl/changeset/{definitionReference.sourceVersion.id}";
        }

        public static string GetGitCommitUrl (string tfsUrl, string projectCollection, Definitionreference definitionReference)
        {
            if (definitionReference.sourceVersion == null)
            { 
                return $"{tfsUrl}/_git/{projectCollection}?" +
                    $"version=GB{definitionReference.branch.name.Replace(@"refs/heads/", string.Empty)}";
            }

            return $"{tfsUrl}/_git/{projectCollection}/commit/" +
                                $"{definitionReference.sourceVersion.id}" +
                                $"?refName={definitionReference.branch.name}";
        }
    }
}
