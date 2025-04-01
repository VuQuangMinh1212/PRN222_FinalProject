using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.ArticlesPage
{
    public class DeleteModel : PageModel
    {
        private readonly IArticleService _articleService;

        public DeleteModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _articleService.GetArticleByIdAsync(id);
            if (article != null)
            {
                Article = article;
                await _articleService.DeleteArticleAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
