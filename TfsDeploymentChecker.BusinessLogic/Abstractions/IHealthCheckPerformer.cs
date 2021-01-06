using System.Threading.Tasks;

namespace TfsDeploymentChecker.BusinessLogic.Abstractions
{
    public interface IHealthCheckPerformer
    {
        Task<bool> IsHealthy();
    }
}
