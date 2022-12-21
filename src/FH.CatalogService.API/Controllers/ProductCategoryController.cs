using AutoMapper;
using FH.CatalogService.Application.Abstraction;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.ProductCategories.Commands.AddCategoryAttribute;
using FH.CatalogService.Application.ProductCategories.Commands.CreateCategory;
using FH.CatalogService.Application.ProductCategories.Commands.DeleteCategory;
using FH.CatalogService.Application.ProductCategories.Commands.RemoveCategoryAttribute;
using FH.CatalogService.Application.ProductCategories.Commands.UpdateCategory;
using FH.CatalogService.Application.ProductCategories.Queries.GetCategories;
using FH.CatalogService.Application.Specifications.Categories;
using FH.CatalogService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FH.CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ILogger<ProductCategoryController> _logger;
        private readonly IMapper _Mapper;
        private readonly IMediator _mediator;

        public ProductCategoryController(ILogger<ProductCategoryController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _Mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));

            return NoContent();
        }

        [HttpGet(nameof(GetCategoriesbyPaging))]
        public async Task<ActionResult<Pagination<CategoryDto>>> GetCategoriesbyPaging(
          [FromQuery] CategorySpecPrams productSpecPrams)
        {
            return Ok(await _mediator.Send(new GetCategoriesByPagingQuery(productSpecPrams)));
        }

        [HttpGet(nameof(GetCategories))]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpGet(nameof(GetCategoriesWithAttributes))]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesWithAttributes()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesWithAttributesQuery()));
        }

        [HttpPost(nameof(AddAttribute))]
        public async Task<ActionResult<int>> AddAttribute(AddCategoryAttributeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost(nameof(RemoveAttribute))]
        public async Task<ActionResult<int>> RemoveAttribute(RemoveCategoryAttributeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
