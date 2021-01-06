using System.Collections.Generic;
using System.Threading.Tasks;

namespace TfsDeploymentChecker.BusinessLogic.Abstractions
{
    public interface IReleaseEnvironmentsGetter
    {
        Task<Dictionary<int, string>> GetEnvironmentsForRelease(string tfsTeamProjectName, int releaseDefinitionId);
    }
}