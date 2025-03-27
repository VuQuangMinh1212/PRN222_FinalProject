using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Services;
using MedicalSearchingPlatform.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MedicalSearchingPlatform.Pages.ArticlesPage
{
    public class IndexModel : PageModel
    {
        private readonly IArticleService _articleService;

        public IndexModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [BindProperty]
        public IList<Article> Articles { get; set; } = default!;

        public PaginatedList<Article> ArticlePaginated { get; set; } = default!;

        public int PageIndex { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            int pageSize = 5;
            PageIndex = pageIndex ?? 1;
            var articles = _articleService.GetArticlesQueryable();
            ArticlePaginated = await PaginatedList<Article>.CreateAsync(articles, PageIndex, pageSize);
            Articles = (await _articleService.GetAllArticlesAsync()).ToList();
            return Page();
        }
    }
}
