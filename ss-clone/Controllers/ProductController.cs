using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Commands.Products;
using WebShop.WebShop.Core.Dto.Products;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.Dto.Sort;
using WebShop.WebShop.Core.Queries.Products;
using WebShop.WebShop.Data.Models;

namespace ss_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ShowProductsOfProfile/{id}")]
        public async Task<ActionResult<List<ProductResponse>>> GetProductsOfProfile(int id)
        {
            var res = await _mediator.Send(new GetProductsOfProfileQuery() { Id = id });
            if (res.Count == 0)
            {
                return NotFound("Products of profile are not found!");
            }
            return res;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductResponse>> Get(int id)
        {
            var res = await _mediator.Send(new GetProductByIdQuery() { Id = id });
            if (res == null)
            {
                return NotFound("Product not found!");
            }
            return res;
        }

        [HttpPost("FilterProductsByPrice")]
        public async Task<ActionResult<ProductResponse>> GetProductsByPrice(GetProductsByPriceDto getProductsByPriceDto)
        {
            var command = new FilterProductsByPriceCommand
            {
                MinPrice = (float)getProductsByPriceDto.minPrice,
                MaxPrice = getProductsByPriceDto.maxPrice
            };

            var filteredProducts = await _mediator.Send(command);

            return Ok(filteredProducts);
        }

        [HttpGet("GetProductsOfBrand/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductsOfBrand(int id)
        {
            var res = await _mediator.Send(new GetProductsOfBrandQuery() { Id = id });
            if(res.Count == 0)
            {
                return NotFound("This Brand does not exist!");
            }
            return Ok(res);
        }

        [HttpPost("SortByPhraseInDescription")]
        public async Task<ActionResult<ProductResponse>> SortByPhraseInDescription(SortByPhraseDto sortByPhraseDto)
        {
            var command = new SortByPhraseInDescriptionCommand { sortByPhraseDto = sortByPhraseDto };
            var res = await _mediator.Send(command);
            if (res.Count == 0)
            {
                return NotFound("This Product does not exist!");
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(CreateProductDto createProductDto) 
        {
            var command = new CreateProductCommand { CreateProductDto = createProductDto };
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, UpdateProductDto updateProductDto)
        {
            var command = new UpdateProductCommand { Id = id, updateProductDto = updateProductDto };
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProductQuery { Id = id });
            return Ok("Product deleted successfully");
        }
    }
}
