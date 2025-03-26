using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using MedicalSearchingPlatform.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

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

        public SelectList ArticleCategories { get; set; } = default!;

        public IList<Article> Articles { get; set; } = default!;

        public PaginatedList<Article> ArticlePaginated { get; set; } = default!;

        public int PageIndex { get; set; }

        public async Task<IActionResult> OnGetCreateOrEditArticleAsync(string id = "")
        {
            if (id.IsNullOrEmpty())
            {
                var categories = await _articleCategoryService.GetAllArticleCategoryAsync();
                ArticleCategories = new SelectList(categories, "CategoryId", "Name");
                return Partial("Create", this);
            }
            else
            {
                Article = await _articleService.GetArticleByIdAsync(id);
                return Partial("Edit", this);
            }
        }

        public async Task<IActionResult> OnGetDetailArticleAsync(string id = "")
        {
            if (id.IsNullOrEmpty())
            {
                return Page();
            }
            else
            {
                var categories = await _articleCategoryService.GetAllArticleCategoryAsync();
                ArticleCategories = new SelectList(categories, "CategoryId", "Name");
                Article = await _articleService.GetArticleByIdAsync(id);
                return Partial("Details", this);
            }
        }

        public async Task OnGetAsync(int? pageIndex)
        {
            int pageSize = 5;
            PageIndex = pageIndex ?? 1;
            var articles = _articleService.GetArticlesQueryable();
            ArticlePaginated = await PaginatedList<Article>.CreateAsync(articles, PageIndex, pageSize);
            Articles = (await _articleService.GetAllArticlesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostEditArticleAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _articleService.UpdateArticleAsync(Article);
            return RedirectToPage("/ArticlePage/Index");
        }

        public async Task<IActionResult> OnPostDeleteArticleAsync(string id = "")
        {
            if (id.IsNullOrEmpty())
            {
                ModelState.AddModelError("DeleteError", "Do not find the id article");
                return Page();
            }
            var isDelete = await _articleService.DeleteArticleAsync(id);
            return RedirectToPage("/ArticlePage/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _articleService.CreateArticleAsync(Article);

            return RedirectToPage("/ArticlePage/Index");
        }
    }
}
