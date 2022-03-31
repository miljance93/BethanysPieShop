using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
    }
}