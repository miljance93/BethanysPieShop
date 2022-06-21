using BethanysPieShop.Application.Core;
using BethanysPieShop.Application.DTO;
using BethanysPieShop.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BethanysPieShop.Application.Categories
{
    public class Update
    {
        public record Command(int Id, CategoryDTO CategoryDTO) : IRequest<Result<bool>>;

        public class Handler : IRequestHandler<Command, Result<bool>>
        {
            private readonly ICategoryRepository _categoryRepository;

            public Handler(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await _categoryRepository.GetById<CategoryDTO>(x => x.CategoryId == request.Id);

                if (category != null)
                {
                    category.CategoryName = request.CategoryDTO.CategoryName;
                    category.Description = request.CategoryDTO.Description;

                    var result = await _categoryRepository.Update(category);
                    return new Result<bool> { IsSuccess = true, Value = result };
                }

                return new Result<bool> { IsSuccess = true, Error = "Category doesen't exist!" };
            }
        }
    }
}
