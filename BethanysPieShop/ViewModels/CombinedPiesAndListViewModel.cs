using BethanysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.ViewModels
{
    public class CombinedPiesAndListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }
        public Pie Pie { get; set; }
        public string CurrentCategory { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int PieId { get; set; }
        [Required(ErrorMessage = "Please enter a name of the pie!")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please enter the price!")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter the short descripiton")]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public string AllergyInformation { get; set; }
        public bool IsPieOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public Category Category { get; set; }
        [BindProperty]
        public string Upload { get; set; }
        public IFormFile UploadImage { get; set; }
    }
}
