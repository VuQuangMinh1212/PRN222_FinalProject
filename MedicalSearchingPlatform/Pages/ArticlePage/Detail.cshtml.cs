using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Pages.ArticlePage
{
    public class DetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Article? Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(); // Prevents null exceptions
            }

            Article = await _context.Articles
                .Include(a => a.Author)  // Ensure author data is loaded
                .Include(a => a.Category) // Ensure category data is loaded
                .FirstOrDefaultAsync(a => a.ArticleId == id);

            if (Article == null)
            {
                return NotFound(); // Handles missing articles
            }

            // Set ViewData for role verification
            ViewData["IsStaff"] = User.IsInRole("Staff");

            return Page();
        }
    }
}
