using System.Threading.Tasks;
using TfsDeploymentChecker.Models.Internal;

namespace TfsDeploymentChecker.BusinessLogic.Abstractions
{
    public interface IEnvironmentDeployedGetter
    {
        Task<DeploymentResult> GetReleasedEnvironmentInformation(string tfsUrl, string projectCollection, int releaseId, int environmentId);
    }
}