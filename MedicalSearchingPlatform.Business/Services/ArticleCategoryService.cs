using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;

namespace MedicalSearchingPlatform.Business.Services
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        public ArticleCategoryService(IArticleCategoryRepository articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
        }
        public async Task<IList<ArticleCategory>> GetAllArticleCategoryAsync()
        {
            return await _articleCategoryRepository.GetAllCategoryAsync();
        }
    }
}
