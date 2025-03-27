using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IArticleCategoryService
    {
        public Task<IList<ArticleCategory>> GetAllArticleCategoryAsync();
    }
}
