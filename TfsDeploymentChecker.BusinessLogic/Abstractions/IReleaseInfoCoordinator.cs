using System.Collections.Generic;
using System.Threading.Tasks;
using TfsDeploymentChecker.Models.Internal;

namespace TfsDeploymentChecker.BusinessLogic.Abstractions
{
    public interface IReleaseInfoCoordinator
    {
        Task<IEnumerable<TargetSystem>> Get();
    }
}