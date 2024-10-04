﻿using financial_manager.Entities.DTOs;
using financial_manager.Entities.Extentions;
using financial_manager.Entities.Models;
using financial_manager.Entities.Requests;
using financial_manager.Repositories.Interfaces;
using financial_manager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace financial_manager.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FinancialManagerContext _financialManagerContext;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryRepository(
            FinancialManagerContext financialManagerContext,
            ICategoryService categoryService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _financialManagerContext = financialManagerContext;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(IEnumerable<CategoryDTO>, int)> GetCategoriesAsync(PageRequest request)
        {
            int userId = _httpContextAccessor.HttpContext!.GetUserId();

            var query = _financialManagerContext.Categories.Where(c => c.UserId == userId);

            int totalCount = query.Count();

            List<CategoryDTO> categories = await query
                .Skip(request.Take * request.Skip)
                .Take(request.Take)
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    CreatedAt = c.CreatedAt
                })
                .AsNoTracking()
                .ToListAsync();

            return (categories, totalCount);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            if (categoryId < 0)
                throw new Exception("The identifier can only be a non-negative integer");

            Category? category = await _financialManagerContext
                .Categories.Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();

            if (category is null)
            {
                throw new Exception("The provided category does not exist");
            }

            _financialManagerContext.Categories.Remove(category);
            await _financialManagerContext.SaveChangesAsync();
        }

        public async Task CreateCategoryAsync(CategoryRequest request)
        {
            if (request is null)
            {
                throw new Exception(nameof(Category));
            }

            try
            {
                await _categoryService.GetTransactionCategoryAsync(request.Title);
                throw new Exception("The category you are trying to create already exists");
            }
            catch (NullReferenceException)
            {
                int userId = _httpContextAccessor.HttpContext!.GetUserId();
                _financialManagerContext.Categories.Add(
                    new Category { Title = request.Title, UserId = userId }
                );

                await _financialManagerContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCategoryAsync(CategoryRequest request)
        {
            Category? existingCategory = await _financialManagerContext.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (existingCategory is null)
            {
                throw new Exception("The category does not exist");
            }

            try
            {
                await _categoryService.GetTransactionCategoryAsync(request.Title);
                throw new Exception("A category with this title already exists");
            }
            catch (Exception)
            {
                existingCategory.Title = request.Title;

                await _financialManagerContext.SaveChangesAsync();
            }
        }
    }
}