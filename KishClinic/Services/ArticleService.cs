using KishClinic.Data;
using KishClinic.Entities;
using Microsoft.EntityFrameworkCore;

namespace KishClinic.Services
{
    public class ArticleService : IArticleService
    {
        private readonly KishClinicDbContext _context;

        public ArticleService(KishClinicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task<Article> CreateAsync(Article article)
        {
            article.CreatedAt = DateTime.UtcNow;
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<Article?> UpdateAsync(int id, Article article)
        {
            var existingArticle = await _context.Articles.FindAsync(id);
            if (existingArticle == null)
            {
                return null;
            }

            existingArticle.Title = article.Title;
            existingArticle.Content = article.Content;
            existingArticle.Description = article.Description;
            existingArticle.AuthorName = article.AuthorName;

            await _context.SaveChangesAsync();
            return existingArticle;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return false;
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
