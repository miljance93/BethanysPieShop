using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    public class PieManagerController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieManagerController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List()
        {
            var pies = _pieRepository.AllPies;
            var vm = new PiesListViewModel()
            {
                Pies = pies
            };

            return View(vm);
        }

        public ViewResult Edit(int pieId) 
        {
            var pie = _pieRepository.GetPieById(pieId);
            var categories = _categoryRepository.AllCategories;

            var vm = new PieVM()
            {
                PieId = pie.PieId,
                ImageThumbnailUrl = pie.ImageThumbnailUrl,
                ImageUrl = pie.ImageUrl,
                LongDescription = pie.LongDescription,
                Name = pie.Name,
                Price = pie.Price,
                ShortDescription = pie.ShortDescription,
                AllergyInformation = pie.AllergyInformation,
                InStock = pie.InStock,
                IsPieOfTheWeek = pie.IsPieOfTheWeek,
                CategoryId = pie.CategoryId,
                Categories = categories
            };

            return View(vm);
        }

        public ViewResult EditPie(PieVM vm) 
        {
            var pie = new Pie()
            {
                PieId = vm.PieId,
                ImageThumbnailUrl = vm.ImageThumbnailUrl,
                ImageUrl = vm.ImageUrl,
                LongDescription = vm.LongDescription,
                Name = vm.Name,
                Price = vm.Price,
                ShortDescription = vm.ShortDescription,
                AllergyInformation = vm.AllergyInformation,
                InStock = vm.InStock,
                IsPieOfTheWeek = vm.IsPieOfTheWeek,
                CategoryId = vm.CategoryId,
                Category = vm.Category
            };
            _pieRepository.Update(pie);

            return View(pie);
        }

        public ViewResult Create()
        {
            var categories = _categoryRepository.AllCategories;

            var vm = new PieVM
            {
                Categories = categories
            };

            return View(vm);
        }

        public ViewResult CreatePie(PieVM vm)
        {
            var pie = new Pie
            {
                PieId = vm.PieId,
                ImageThumbnailUrl = vm.ImageThumbnailUrl,
                ImageUrl = vm.ImageUrl,
                LongDescription = vm.LongDescription,
                Name = vm.Name,
                Price = vm.Price,
                ShortDescription = vm.ShortDescription,
                AllergyInformation = vm.AllergyInformation,
                InStock = vm.InStock,
                IsPieOfTheWeek = vm.IsPieOfTheWeek,
                CategoryId = vm.CategoryId,
                Category = vm.Category
            };

            _pieRepository.Add(pie);

            return View(pie);
        }
    }
}