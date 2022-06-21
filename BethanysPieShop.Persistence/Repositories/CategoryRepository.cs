using AutoMapper;
using BethanysPieShop.Application.Interfaces;
using BethanysPieShop.Domain.Models;

namespace BethanysPieShop.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext, IMapper mapper) : base(appDbContext, mapper)
        {
        }
    }
}
