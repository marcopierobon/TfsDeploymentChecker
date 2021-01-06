using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TfsDeploymentChecker.BusinessLogic.Abstractions;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class TfsClient : ITfsClient
    {
        public TfsClient(IConfigurationRetriever configurationRetriever)
        {
            _configurationRetriever = configurationRetriever;
        }

        private IConfigurationRetriever _configurationRetriever { get; set; }

        public HttpClient GetClient()
        {
            var handler = _configurationRetriever.AllowUntrustedSslCertificates
                            ?
                                new HttpClientHandler()
                                {
                                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                                }
                            :
                                new HttpClientHandler();
            var client = new HttpClient(handler);
            var base64TfsToken = Convert.ToBase64String(
                Encoding.ASCII.GetBytes(
                    $"API_KEY:{_configurationRetriever.TfsToken}"));
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Basic", base64TfsToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
