using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Pages.MedicalServicePage
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<MedicalService> MedicalService { get; set; } = new List<MedicalService>();
        public string UserRole { get; set; } = "Guest";

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            UserRole = user != null ? (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "Guest" : "Guest";
            MedicalService = await _context.MedicalServices.ToListAsync();
        }
    }
}
