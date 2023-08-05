using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Commands;
using WebShop.WebShop.Core.Dto.Products;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.Dto.Sort;
using WebShop.WebShop.Core.Queries;
using WebShop.WebShop.Data.Models;

namespace ss_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMediator _mediator;
        public ProductController(ApiDbContext context, IMediator mediator)
        {
            _context = context;
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
            var keyPhraseLowerCase = sortByPhraseDto.KeyPhrase.ToLower();
            var productsSorted = await _context.Products
                .Where(p => p.Description.ToLower().Contains(keyPhraseLowerCase))
                .OrderByDescending(p => p.Created)
                .Select(c => new ProductResponse
                {
                    Id = c.Id,
                    Year = c.Year,
                    ShortBio = c.ShortBio,
                    Description = c.Description,
                    Photo = c.Photo,
                    Price = c.Price,
                    Created = c.Created,
                    Updated = c.Updated,
                    Profile = c.Profile
                })
                .ToListAsync();
            if (productsSorted.Count == 0)
            {
                return NotFound("This Product does not exist!");
            }
            return Ok(productsSorted);
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
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) {
                return BadRequest("Invalid ProductId. Product not found!");
            }
            product.Year = updateProductDto.Year;
            product.Photo = updateProductDto.Photo;
            product.Price = updateProductDto.Price;
            product.ShortBio = updateProductDto.ShortBio;
            product.Description = updateProductDto.Description;
            product.Updated = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found!s");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Product deleted successfully");
        }
    }
}
