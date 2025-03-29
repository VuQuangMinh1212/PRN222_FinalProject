using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Pages.ArticlesPage
{
    public class CreateModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;

        public CreateModel(IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
        }

        public SelectList ArticleCategories { get; set; }
        public SelectList ArticleStatus { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var categories = await _articleCategoryService.GetAllArticleCategoryAsync();
            var status = new List<String>
            {
                "Published",
                "Draft"
            };
            ArticleCategories = new SelectList(categories, "CategoryId", "Name");
            ArticleStatus = new SelectList(status);
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; } = new Article();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _articleService.CreateArticleAsync(Article);

            return RedirectToPage("/ArticlesPage/Index");
        }
    }
}
