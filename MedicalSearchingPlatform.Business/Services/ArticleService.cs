using MedicalSearchingPlatform.Business.Hubs;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace MedicalSearchingPlatform.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public ArticleService(IArticleRepository articleRepository, IHubContext<SignalRServer> hubContext)
        {
            _articleRepository = articleRepository;
            _hubContext = hubContext;
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
            await _hubContext.Clients.All.SendAsync("LoadAllArticle");
            return true;
        }

        public async Task<bool> UpdateArticleAsync(Article article)
        {
            await _articleRepository.UpdateArticleAsync(article);
            await _hubContext.Clients.All.SendAsync("LoadAllArticle");
            return true;
        }

        public async Task<bool> DeleteArticleAsync(string articleId)
        {
            await _articleRepository.DeleteArticleAsync(articleId);
            await _hubContext.Clients.All.SendAsync("LoadAllArticle");
            return true;
        }

        public IQueryable<Article> GetArticlesQueryable()
        {
            return _articleRepository.GetAllQueryable();
        }

        public IQueryable<Article> GetArticlesPublished()
        {
            return _articleRepository.GetAllArticlesPublish();
        }

        public async Task<IEnumerable<Article>> GetMostLikedArticlesAsync(int top)
        {
            return await _articleRepository.GetMostLikedArticlesAsync(top);
        }

        public async Task<int> GetArticleLikeCountAsync(string articleId)
        {
            return await _articleRepository.GetArticleLikeCountAsync(articleId);
        }

    }
}
