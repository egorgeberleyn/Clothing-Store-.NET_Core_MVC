﻿namespace ClothingStore.Data.Interfaces
{
    public interface ICategoryRepository : IDisposable
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Category GetCategoryByName(string name);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task SaveAsync();
    }
}
