using System.ComponentModel.DataAnnotations;

namespace MedicalSearchingPlatform.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Email invalid format")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Password is required"),
            DataType(DataType.Password),
            Compare("Password", ErrorMessage = "Confirm password does not match password")]
        public string? ConfirmPassword { get; set; }
    }
}
