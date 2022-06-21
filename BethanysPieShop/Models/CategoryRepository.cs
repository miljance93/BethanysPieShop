using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;

namespace BethanysPieShop.Models
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> AllCategories => _appDbContext.Categories;

        public Category GetCategory => _appDbContext.Categories.FirstOrDefault();

        public async Task<Category> CreateCategory(Category category)
        {
            try
            {
                _appDbContext.Add(category);
                await _appDbContext.SaveChangesAsync();

                return category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category> DeleteCategory(Category category)
        {
            try
            {
                _appDbContext.Remove(category);
                await _appDbContext.SaveChangesAsync();

                return category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Category GetCategoryById(int id)
        {
            return _appDbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            try
            {
                _appDbContext.Update(category);
                await _appDbContext.SaveChangesAsync();

                return category;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
