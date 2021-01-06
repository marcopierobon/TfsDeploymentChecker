using System.Collections.Generic;
using TfsDeploymentChecker.Models.Internal;

namespace TfsDeploymentChecker.BusinessLogic.Abstractions
{
    public interface IConfigurationRetriever
    {
        IEnumerable<string> SystemsOfInterest { get; set; }

        IEnumerable<ReleasesForTeamProject> TeamProjectsAndReleaseIdsPairs { get; set; }

        string TfsUrl { get; set; }

        string TfsApiVersion { get; set; }

        int HealthCheckIntervalInSeconds { get; set; }
        
        string TfsToken { get; set; }

        bool AllowUntrustedSslCertificates { get; set; }
    }
}