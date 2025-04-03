using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Diagnostics;

namespace MedicalSearchingPlatform.Pages
{
    public class SuccessModel : PageModel
    {
        public string Status { get; set; }
        public string Cancel { get; set; }
        public string OrderCode { get; set; }

        public void OnGet(string code, string id, string cancel, string status, string orderCode)
        {
            Debug.WriteLine("OnGet called, regardless of parameters");
        }
    }
}