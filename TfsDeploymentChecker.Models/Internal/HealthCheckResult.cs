using System.Collections.Generic;

namespace TfsDeploymentChecker.Models.Internal
{
    public class HealthCheckResult
    {
        public bool IsHealthy { get; set; }

        public List<string> HealthCheckDetails { get; set; }
    }
}
