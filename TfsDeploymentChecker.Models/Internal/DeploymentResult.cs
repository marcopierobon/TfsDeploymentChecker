using System.Collections.Generic;

namespace TfsDeploymentChecker.Models.Internal
{
    public class ReleasesForTeamProject
    {
        public string TfsTeamProjectName { get; set; }

        public IList<int> ProjectReleaseDefinitionIds { get; set; }
    }
}
