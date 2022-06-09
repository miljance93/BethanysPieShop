using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

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
            var vm = new CombinedPiesAndListViewModel()
            {
                Pies = pies,
                Categories = _categoryRepository.AllCategories
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

        private string UploadedFile(PieVM vm)
        {
            string uniqueFileName = null;

            if (vm.UploadImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "PieImages");
                uniqueFileName = "Picture " + "_" + Guid.NewGuid().ToString() + "_" + vm.UploadImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                vm.UploadImage.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        public IActionResult CreatePie(PieVM vm)
        {
            var pie = new Pie();
            if (ModelState.IsValid)
            {
                if (vm.Upload == "FromUrl")
                {
                    pie = new Pie
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

                else if(vm.Upload == "FromComputer")
                {
                    string uniqueFileName = UploadedFile(vm);

                    pie = new Pie
                    {
                        PieId = vm.PieId,
                        ImageThumbnailUrl = uniqueFileName,
                        ImageUrl = uniqueFileName,
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