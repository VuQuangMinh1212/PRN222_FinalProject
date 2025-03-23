using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Repositories;
using MedicalSearchingPlatform.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _articleRepository.GetAllArticlesAsync();
        }

        public async Task<Article> GetArticleByIdAsync(string articleId)
        {
            return await _articleRepository.GetArticleByIdAsync(articleId);
        }

        public async Task<IEnumerable<Article>> GetArticlesByCategoryAsync(string category)
        {
            return await _articleRepository.GetArticlesByCategoryAsync(category);
        }

        public async Task<bool> CreateArticleAsync(Article article)
        {
            await _articleRepository.AddArticleAsync(article);
            return true;
        }

        public async Task<bool> UpdateArticleAsync(Article article)
        {
            await _articleRepository.UpdateArticleAsync(article);
            return true;
        }

        public async Task<bool> DeleteArticleAsync(string articleId)
        {
            await _articleRepository.DeleteArticleAsync(articleId);
            return true;
        }
    }
}
