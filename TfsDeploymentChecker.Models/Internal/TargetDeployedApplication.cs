namespace TfsDeploymentChecker.Models.Internal
{
    public class TargetDeployedApplication
    {
        public string EnvironmentName { get; set; }

        public int EnvironmentId { get; set; }

        public int ReleaseDefinitionId { get; set; }

        public DeploymentResult DeploymentResult { get; set; }
    }
}
