using BethanysPieShop.Application.Core;
using BethanysPieShop.Application.DTO;
using BethanysPieShop.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BethanysPieShop.Application.Categories
{
    public class Delete
    {
        public record Command(string CategoryName) : IRequest<Result<bool>>;

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly ICategoryRepository _categoryRepository;

            public Handler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _categoryRepository.GetById<CategoryDTO>
                    (x => x.CategoryName.ToLower() == request.CategoryName.ToLower());

                if (result != null)
                {
                    var deletedResult = await _categoryRepository.Delete(result);
                    return new Result<bool> { IsSuccess = true, Value = deletedResult };
                }

                return new Result<bool> { IsSuccess = false, Error = "Category with that name is not deleted!" };
            }
        }
    }
}
