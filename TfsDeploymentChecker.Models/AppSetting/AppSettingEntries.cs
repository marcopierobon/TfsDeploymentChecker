namespace TfsDeploymentChecker.Models.AppSetting
{
    public static class AppSettingEntries
    {
        public static string TFS_TOKEN { get; } = "TfsToken";

        public static string TFS_URL { get; } = "TfsUrl";
        
        public static string TFS_API_VERSION { get; } = "TfsApiVersion";
        
        public static string ALLOW_UNTRUSTED_SSL_CERTIFICATES { get; } = "AllowUntrustedSslCertificates";
        
        public static string SYSTEMS_OF_INTEREST { get; } = "SystemsOfInterest";
        
        public static string TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS { get; } = "TeamProjectsAndReleaseIdsPairs";
        
        public static string HEALTHCHECK_INTERVAL_IN_SECONDS { get; } = "HealthCheckIntervalInSeconds";
    }
}
