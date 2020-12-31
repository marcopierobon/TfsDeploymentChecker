using System;

namespace TfsDeploymentChecker.Models.Internal
{
    public class DeploymentResult
    {
        public string EnvironmentName { get; set; }

        public int EnvironmentId { get; set; }

        public string PackageVersion { get; set; }

        public string DeploymentStatus { get; set; }

        public string DefinitionName { get; set; }

        public string ReleaseName { get; set; }

        public string ReleaseUrl { get; set; }

        public string PackageUrl { get; set; }

        public string BranchName { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime CompletedOn { get; set; }
    }
}
