using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.Categories;
using WebShop.WebShop.Data.Models;

namespace ss_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public CategoryController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Include(c => c.Brands)
                .FirstOrDefaultAsync();
            if(category == null)
            {
                return NotFound("Category not found!");
            }
            return category;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Category>> Post(int id, CreateCategoryDto createCategoryDto)
        {
            var section = await _context.Sections.FindAsync(id);
            if(section == null)
            {
                return NotFound("Section not found!");
            }

            var category = new Category
            {
                Name = createCategoryDto.Name,
                Created = DateTime.UtcNow,
                Section = section
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null) {
                return NotFound("Category not found!");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok("Category successfully deleted!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Put(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            category.Name = updateCategoryDto.Name;
            await _context.SaveChangesAsync();
            return Ok("Category is updated");
        }
    }
}
