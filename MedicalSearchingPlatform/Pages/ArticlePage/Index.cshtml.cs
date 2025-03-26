using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalSearchingPlatform.Pages.ArticlePage
{
    public class IndexModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;

        public IndexModel(IArticleService articleService, IArticleCategoryService articleCategoryService)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
        }

        [BindProperty]
        public Article Article { get; set; } = new Article();

        public IList<Article> Articles { get; set; } = default!;


        public async Task<IActionResult> OnGetCreateArticleAsync()
        {
            var categories = await _articleCategoryService.GetAllArticleCategoryAsync();
            ViewData["ArticleCategory"] = new SelectList(categories, "CategoryId", "Name");
            return Partial("Create", Article);
        }

        public async Task OnGetAsync()
        {
            Articles = (await _articleService.GetAllArticlesAsync()).ToList();
        }
    }
}
