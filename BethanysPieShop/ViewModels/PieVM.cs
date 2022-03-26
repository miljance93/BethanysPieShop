using BethanysPieShop.Models;
using System.Collections.Generic;

namespace BethanysPieShop.ViewModels
{
    public class PieVM
    {
        public int PieId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<Category> Categories { get; set; }
    }
}
