using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.Include(a => a.Author).Include(a => a.Category).OrderByDescending(x => x.PublishedDate).ToListAsync();
        }

        public async Task<Article> GetArticleByIdAsync(string articleId)
        {
            return await _context.Articles.Include(a => a.Author).Include(a => a.Category)
                                          .FirstOrDefaultAsync(a => a.ArticleId == articleId);
        }

        public async Task<IEnumerable<Article>> GetArticlesByCategoryAsync(string category)
        {
            return await _context.Articles.Include(ac => ac.Category).ToListAsync();
        }

        public async Task AddArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArticleAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(string articleId)
        {
            var article = await _context.Articles.FindAsync(articleId);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<Article> GetAllQueryable()
        {
            return _context.Articles.Include(a => a.Category).OrderByDescending(x => x.PublishedDate);
        }

        public IQueryable<Article> GetAllArticlesPublish()
        {
            return _context.Articles.Include(a => a.Category).Where(x => x.Status == "Published").OrderByDescending(x => x.PublishedDate);
        }
    }
}
