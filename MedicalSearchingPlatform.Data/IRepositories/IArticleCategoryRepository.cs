using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data.IRepositories
{
    public interface IArticleCategoryRepository
    {
        Task<IList<ArticleCategory>> GetAllCategoryAsync();
    }
}
