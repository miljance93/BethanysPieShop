﻿using System;
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

        public Category GetCategoryById(int id)
        {
            return _appDbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
        }
    }
}
