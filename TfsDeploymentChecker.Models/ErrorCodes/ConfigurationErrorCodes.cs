namespace TfsDeploymentChecker.Models.ErrorCodes
{
    public static class ConfigurationErrorCodes
    {
        public static string MISSING_SYSTEMS_OF_INTEREST { get; } = "MissingSystemsOfInterest";

        public static string MISSING_TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS { get; } = "MissingTeamProjectsAndReleaseIdsPairs";

        public static string MISSING_TFS_API_VERSION { get; } = "MissingTfsApiVersion";

        public static string MISSING_TFS_URL { get; } = "MissingTfsUrl";
        
        public static string MISSING_HEALTHCHECK_INTERVAL_IN_SECONDS { get; } = "MissingHealthCheckIntervalInSeconds";
        
        public static string MISSING_TFS_TOKEN { get; } = "MissingTfsToken";
        
        public static string MISSING_ALLOW_UNTRUSTED_SSL_CERTIFICATES { get; } = "MissingAllowUntrustedSslCertificates";
        
        public static string INVALID_JSON_MODEL { get; } = "InvalidJsonModel";
    }
}