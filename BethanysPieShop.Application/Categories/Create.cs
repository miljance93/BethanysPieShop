using BethanysPieShop.Application.Core;
using BethanysPieShop.Application.DTO;
using BethanysPieShop.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BethanysPieShop.Application.Categories
{
    public class Create
    {
        public record Command(CategoryDTO Category) : IRequest<Result<bool>>;

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly ICategoryRepository _categoryRepository;

            public Handler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var exists = await _categoryRepository.Exists(request.Category);

                if(exists == true)
                {
                    return new Result<bool> { IsSuccess = false, Error = "Category is not created. It already exists" };
                }

                var result = await _categoryRepository.Create(request.Category);
                return new Result<bool> { IsSuccess = true, Value = result };
            }
        }
    }
}
