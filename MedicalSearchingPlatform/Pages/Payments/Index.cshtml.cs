using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MedicalSearchingPlatform.Services;
using MedicalSearchingPlatform.Data.Entities;
using System.Threading.Tasks;
using MedicalSearchingPlatform.Business.Interfaces;

namespace MedicalSearchingPlatform.Pages.Payments
{
    public class IndexModel : PageModel
    {
        private readonly IPaymentService _paymentService;

        public IndexModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [BindProperty]
        public decimal Amount { get; set; }

        [BindProperty]
        public string PatientId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid().ToString(),
                PatientId = PatientId,
                Amount = Amount,
                Status = "Pending"
            };

            string paymentUrl = await _paymentService.ProcessPaymentAsync(payment);

            if (paymentUrl.StartsWith("http"))
            {
                return Redirect(paymentUrl);
            }

            ModelState.AddModelError(string.Empty, "Payment processing failed.");
            return Page();
        }
    }
}
