using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.Brands;
using WebShop.WebShop.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ss_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public BrandController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Brand>> Get()
        {
            return Ok(await _context.Brands.ToListAsync());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Brand>> Post(int id, CreateBrandDto createBrandDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound("Category not found!");
            }
            var brand = new Brand
            {
                Name = createBrandDto.Name,
                Created = DateTime.UtcNow,
                Category = category
            };
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = brand.Id }, brand);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> Delete(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return Ok("Brand is deleted!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Brand>> Put(int id, UpdateBrandDto updateBrandDto)
        {
            var brand = await _context.Brands.FindAsync(id);
            brand.Name = updateBrandDto.Name;
            await _context.SaveChangesAsync();
            return Ok("Brand is updated!");
        }
    }
}
