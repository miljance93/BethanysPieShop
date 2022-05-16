using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: /<controller>/
        //public IActionResult List()
        //{
        //    //ViewBag.CurrentCategory = "Cheese cakes";

        //    //return View(_pieRepository.AllPies);
        //    PiesListViewModel piesListViewModel = new PiesListViewModel();
        //    piesListViewModel.Pies = _pieRepository.AllPies;

        //    piesListViewModel.CurrentCategory = "Cheese cakes";
        //    return View(piesListViewModel);
        //}

        public ViewResult List(string category, string pieNameSearch)
        {
            IEnumerable<Pie> pies;
            string categoryName = string.Empty;

            var pieName = from p in _pieRepository.AllPies select p;

            if (!String.IsNullOrEmpty(pieNameSearch) && pieNameSearch.Length >= 3)
            {
                pieName = pieName.Where(p => p.Name.ToLower()!.Contains(pieNameSearch.ToLower()));

                if (!string.IsNullOrEmpty(category) && category != "All pies")
                {
                    pieName = _pieRepository.AllPies.Where(p => p.Name.ToLower()!.Contains(pieNameSearch.ToLower()) 
                    && p.Category.CategoryName == category);
                }

                return View(new PiesListViewModel
                {
                    Pies = pieName,
                    Categories = _categoryRepository.AllCategories
                });
            }

            if (string.IsNullOrEmpty(category) || category == "All pies")
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                categoryName = "All pies";

                Category allPies = new()
                {
                    Pies = pies.ToList(),
                    CategoryId = 0,
                    CategoryName = "All pies",
                    Description = "All pies"
                };

                var categories = _categoryRepository.AllCategories.Append(allPies);

                return View(new PiesListViewModel
                {
                    Categories = categories,
                    Pies = pies,
                    CurrentCategory = categoryName
                });
            }

            else
            {
                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                categoryName = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = categoryName,
                Categories = _categoryRepository.AllCategories
            });
        }


        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            var categoryList = _categoryRepository.AllCategories;

            var vm = new PieVM()
            {
                PieId = pie.PieId,
                Name = pie.Name,
                ImageUrl = pie.ImageUrl,
                Price = Math.Round(pie.Price, 1),
                LongDescription = pie.LongDescription,
                ShortDescription = pie.ShortDescription,
                Categories = categoryList,
                ImageThumbnailUrl = pie.ImageThumbnailUrl
            };

            return View(vm);
        }
    }
}
