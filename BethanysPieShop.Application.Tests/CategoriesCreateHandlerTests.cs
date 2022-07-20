using AutoFixture;
using BethanysPieShop.Application.DTO;
using BethanysPieShop.Application.Interfaces;
using Moq;
using System.Threading;
using Xunit;
using static BethanysPieShop.Application.Categories.Create;

namespace BethanysPieShop.Application.Tests
{
    public class CategoriesCreateHandlerTests
    {
        [Fact]
        public async void Handle_CategoryAlreadyExists_ReturnsFalseForIsSuccess()
        {
            // ARRANGE
            var categoryRepository = new Mock<ICategoryRepository>();
            var handler = new Handler(categoryRepository.Object);
            var fixture = new Fixture();
            var categoryDto = fixture.Create<CategoryDTO>();
            var command = new Command(categoryDto);
            var cancellationToken = new CancellationToken();

            // ACT 
            var result = await handler.Handle(command, cancellationToken);

            // ASSERT
            Assert.True(result.IsSuccess);
        }
    }
}
