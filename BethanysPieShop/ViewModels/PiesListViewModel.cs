using System.Collections.Generic;
using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
    public class PiesListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }
        public Pie Pie { get; set; }
        public string CurrentCategory { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
