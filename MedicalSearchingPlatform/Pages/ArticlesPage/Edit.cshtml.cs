using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalSearchingPlatform.Pages.ArticlesPage
{
    public class EditModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;

        public EditModel(IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;
        public SelectList ArticleCategories { get; set; }
        public SelectList ArticlesStatus { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var categories = await _articleCategoryService.GetAllArticleCategoryAsync();
            var status = new List<string>
            {
                "Draft",
                "Published"
            };
            ArticleCategories = new SelectList(categories, "CategoryId", "Name");
            ArticlesStatus = new SelectList(status);
            Article = await _articleService.GetArticleByIdAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _articleService.UpdateArticleAsync(Article);

            return RedirectToPage("/ArticlesPage/Index");
        }
    }
}
