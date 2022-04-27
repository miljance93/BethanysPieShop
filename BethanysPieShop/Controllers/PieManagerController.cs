using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
    public class PieManagerController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PieManagerController(IPieRepository pieRepository, ICategoryRepository categoryRepository, IWebHostEnvironment hostEnvironment)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult List()
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

        private string UploadedFile(PieVM vm)
        {
            string uniqueFileName = null;

            if (vm.UploadImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.UploadImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.UploadImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public IActionResult Create(string upload)
        {
            var categories = _categoryRepository.AllCategories;

            var vm = new PieVM
            {
                Categories = categories,
                Upload = upload
            };

            return View(vm);
        }

        public IActionResult CreatePie(PieVM vm)
        {
            if (ModelState.IsValid)
            {
                Pie pie = new Pie
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
                    CategoryId = vm.CategoryId
                };

                _pieRepository.Add(pie);
                return View(pie);
            }

            return RedirectToAction("Create");
        }

        public ViewResult Delete(int pieId)
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

        public IActionResult DeletePie(PieVM vm)
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

            _pieRepository.Delete(pie);

            return RedirectToAction("List");
        }
    }
}