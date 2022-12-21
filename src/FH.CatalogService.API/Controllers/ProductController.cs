using AutoMapper;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Application.Helpers;
using FH.CatalogService.Application.Products.Commands.AddProductAttribute;
using FH.CatalogService.Application.Products.Commands.CreateProduct;
using FH.CatalogService.Application.Products.Commands.DeleteProduct;
using FH.CatalogService.Application.Products.Commands.RemoveProductAttribute;
using FH.CatalogService.Application.Products.Commands.UpdateProduct;
using FH.CatalogService.Application.Products.Queries.GetProducts;
using FH.CatalogService.Application.Specifications.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FH.CatalogService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _Mapper;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _Mapper = mapper;
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductCommand command)
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
            await _mediator.Send(new DeleteProductCommand(id));

            return NoContent();
        }

        [HttpGet(nameof(GetProductsByPaging))]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProductsByPaging(
          [FromQuery] ProductSpecPrams productSpecPrams)
        {
            return Ok(await _mediator.Send(new GetProductsByPagingQuery(productSpecPrams)));
        }

        [HttpGet(nameof(GetProducts))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }

        [HttpGet(nameof(GetProductsByCategory))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory([FromQuery] GetProductsByCategoryQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet(nameof(GetProductsWithAttributes))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsWithAttributes()
        {
            return Ok(await _mediator.Send(new GetAllProductsWithAttributesQuery()));
        }

        [HttpPost(nameof(AddAttribute))]
        public async Task<ActionResult<int>> AddAttribute(AddProductAttributeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost(nameof(RemoveAttribute))]
        public async Task<ActionResult<int>> RemoveAttribute(RemoveProductAttributeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

