using System.Net.Http;

namespace TfsDeploymentChecker.BusinessLogic.Abstractions
{
    public interface ITfsClient
    {
        HttpClient GetClient();
    }
}