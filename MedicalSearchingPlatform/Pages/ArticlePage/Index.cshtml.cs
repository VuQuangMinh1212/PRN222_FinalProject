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

namespace MedicalSearchingPlatform.Pages.ArticlePage
{
    public class IndexModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly UserManager<User> _userManager;

        public IndexModel(IArticleService articleService, IArticleCategoryService articleCategoryService, UserManager<User> userManager)
        {
            _articleService = articleService;
            _articleCategoryService = articleCategoryService;
            _userManager = userManager;
        }

        [BindProperty]
        public Article Article { get; set; } = new Article();

        public IList<Article> Articles { get; set; } = new List<Article>();

        public async Task<IActionResult> OnGetCreateArticleAsync()
        {
            var categories = await _articleCategoryService.GetAllArticleCategoryAsync();
            ViewData["ArticleCategory"] = new SelectList(categories, "CategoryId", "Name");
            return Partial("Create", Article);
        }

        public async Task OnGetAsync()
        {
            Articles = (await _articleService.GetAllArticlesAsync()).ToList();

            var user = await _userManager.GetUserAsync(User);
            ViewData["IsStaff"] = user != null && user.Role == "Staff";
        }
    }
}
