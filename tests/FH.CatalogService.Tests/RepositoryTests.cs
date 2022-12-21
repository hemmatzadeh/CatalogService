using FH.CatalogService.Application.ProductCategories.Commands.CreateCategory;
using FluentAssertions;
using FluentValidation;

namespace FH.CatalogService.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public async Task ShouldRequireMinimumFields()
        {
            var command = new CreateCategoryCommand();

            //await FluentActions.Invoking(() =>
            //    SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}