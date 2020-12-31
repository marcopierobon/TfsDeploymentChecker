using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TfsDeploymentChecker.BusinessLogic.Abstractions;

namespace TfsDeploymentChecker.BusinessLogic.Implementations
{
    public class TfsClient : ITfsClient
    {
        public TfsClient(string token, bool allowUntrustedSslCertificates)
        {
            _token = token;
            _allowUntrustedSslCertificates = allowUntrustedSslCertificates;
        }

        private string _token { get; set; }

        private bool _allowUntrustedSslCertificates { get; set; }

        public HttpClient GetClient()
        {
            var handler = _allowUntrustedSslCertificates
                            ?
                                new HttpClientHandler()
                                {
                                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                                }
                            :
                                new HttpClientHandler();
            var client = new HttpClient(handler);
            var base64TfsToken =  Convert.ToBase64String(
                Encoding.ASCII.GetBytes(
                    $"API_KEY:{_token}"
                ));
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Basic", base64TfsToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
