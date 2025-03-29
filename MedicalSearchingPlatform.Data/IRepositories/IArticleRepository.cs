using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public interface IArticleRepository
    {
        IQueryable<Article> GetAllQueryable();
        IQueryable<Article> GetAllArticlesPublish();
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<Article> GetArticleByIdAsync(string articleId);
        Task<IEnumerable<Article>> GetArticlesByCategoryAsync(string category);
        Task AddArticleAsync(Article article);
        Task UpdateArticleAsync(Article article);
        Task DeleteArticleAsync(string articleId);
    }
}
