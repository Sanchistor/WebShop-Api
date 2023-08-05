using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.Dto.Sections;
using WebShop.WebShop.Data.Models;

namespace ss_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public SectionController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SelectSectionDto>>> Get()
        {
            var sectionDTO = await _context.Sections
                .Select(c => new SelectSectionDto { Id = c.Id, Created = c.Created, Name = c.Name })
                .ToListAsync();

            if (sectionDTO == null)
            {
                return NotFound("Section is not found!");
            }

            return sectionDTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetById(int id)
        {
            var section = await _context.Sections
                .Where(c => c.Id == id)
                .Include(c => c.Categories)
                .FirstOrDefaultAsync();

            if (section == null)
            {
                return NotFound("Section is not found!");
            }

            return section;
        }

        [HttpGet("SortBySection/{id}")]
        public async Task<ActionResult<ProductResponse>> SortBySection(int id)
        {
            var sortedBySection = await _context.Products
                .Include(c => c.Section)
                .Where(c => c.SectionId == id)
                .OrderByDescending(c => c.Created)
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
            if (sortedBySection.Count == 0)
            {
                return NotFound("This Section does not exist!");
            }
            return Ok(sortedBySection);
        }

        [HttpPost]
        public async Task<ActionResult<Section>> Post(CreateSectionDto createSectionDto)
        {
            var section = new Section
            {
                Name = createSectionDto.Name,
                Created = DateTime.UtcNow
            };

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = section.Id }, section);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Section>> Delete(int id)
        {
            var section = _context.Sections.FirstOrDefault(c => c.Id == id);
            if (section == null) {
                return NotFound("Section is not found!");
            }
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return Ok("Section deleted successfully!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Section>> Put(int id, UpdateSectionDto updateSectionDto)
        {
            var section = _context.Sections.FirstOrDefault(c => c.Id == id);
            if (section == null)
            {
                return NotFound("Section is not found!");
            }
            section.Name = updateSectionDto.Name;
            await _context.SaveChangesAsync();
            return Ok("Section updated successfully");
        }
    }
}
