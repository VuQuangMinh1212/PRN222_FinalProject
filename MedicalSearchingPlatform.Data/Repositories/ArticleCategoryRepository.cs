using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ArticleCategory>> GetAllCategoryAsync()
        {
            return await _context.ArticleCategories.ToListAsync();
        }
    }
}
