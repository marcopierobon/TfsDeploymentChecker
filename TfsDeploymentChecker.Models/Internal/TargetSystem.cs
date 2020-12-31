using System.Collections.Generic;

namespace TfsDeploymentChecker.Models.Internal
{
    public class TargetSystem
    {
        public string TargetSystemName { get; set; }

        public IList<TargetDeployedApplication> ReleasedEnvironments { get; set; }
    }
}
