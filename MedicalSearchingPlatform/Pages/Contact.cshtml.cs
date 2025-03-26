using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MedicalSearchingPlatform.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ContactModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public ContactInputModel Input { get; set; } = new ContactInputModel();

        public string SuccessMessage { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Load email settings from appsettings.json
            string smtpServer = _configuration["EmailSettings:SmtpServer"];
            int smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
            string senderEmail = _configuration["EmailSettings:SenderEmail"];
            string senderPassword = _configuration["EmailSettings:SenderPassword"];
            string recipientEmail = _configuration["EmailSettings:RecipientEmail"];

            try
            {
                var smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "New Contact Form Submission",
                    Body = $"Name: {Input.Name}\nEmail: {Input.Email}\nMessage:\n{Input.Message}",
                    IsBodyHtml = false
                };
                mailMessage.To.Add(recipientEmail);

                await smtpClient.SendMailAsync(mailMessage);

                SuccessMessage = "Thank you for reaching out! Your message has been sent successfully.";
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Failed to send the message. Please try again later.");
            }

            return Page();
        }

        public class ContactInputModel
        {
            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(500)]
            public string Message { get; set; }
        }
    }
}
