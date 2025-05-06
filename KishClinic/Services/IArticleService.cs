﻿using KishClinic.Entities;

namespace KishClinic.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task<Article> CreateAsync(Article article);
        Task<Article?> UpdateAsync(int id, Article article);
        Task<bool> DeleteAsync(int id);
    }
}
