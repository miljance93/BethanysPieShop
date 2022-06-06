using BethanysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BethanysPieShop.ViewModels
{
    public class PieVM
    {
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
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        [BindProperty]
        public string Upload { get; set; }
        public IFormFile UploadImage { get; set; }
    }
}
