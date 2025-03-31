using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace MedicalSearchingPlatform.Pages
{
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }
        public string? ErrorMessage { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet(string? errorMessage = null)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorMessage = errorMessage;
        }
    }
}
