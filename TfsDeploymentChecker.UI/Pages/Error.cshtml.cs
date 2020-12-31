using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TfsDeploymentChecker.UI.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed.")]
    public class ErrorModel : PageModel
    {
        public ErrorModel()
        {
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}