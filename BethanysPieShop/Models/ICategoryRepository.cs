using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
        Category GetCategoryById(int id);
    }
}
