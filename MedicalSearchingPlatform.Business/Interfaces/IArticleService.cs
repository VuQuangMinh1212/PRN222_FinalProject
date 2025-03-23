using MedicalSearchingPlatform.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<Article> GetArticleByIdAsync(string articleId);
        Task<IEnumerable<Article>> GetArticlesByCategoryAsync(string category);
        Task<bool> CreateArticleAsync(Article article);
        Task<bool> UpdateArticleAsync(Article article);
        Task<bool> DeleteArticleAsync(string articleId);
    }
}
