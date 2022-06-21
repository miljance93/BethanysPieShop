using BethanysPieShop.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BethanysPieShop.Application.Categories;

namespace BethanysPieShop.Api.Controllers
{
    public class CategoryController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CategoryDTO category)
        {
            return HandleResult(await Mediator.Send(new Create.Command(category)));
        }

        [HttpDelete("{categoryName}")]
        public async Task<IActionResult> Delete(string categoryName)
        {
            return HandleResult(await Mediator.Send(new Delete.Command(categoryName)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,CategoryDTO category)
        {
            return HandleResult(await Mediator.Send(new Update.Command(id, category)));
        }
    }
}
