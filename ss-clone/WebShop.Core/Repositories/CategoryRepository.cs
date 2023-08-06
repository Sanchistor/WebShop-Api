using MediatR;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.Categories;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApiDbContext _context;
        public CategoryRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null) {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            
            return Unit.Value;
        }

        public async Task<List<Category>> Get()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories
               .Where(c => c.Id == id)
               .Include(c => c.Brands)
               .FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> Post(int id, CreateCategoryDto createCategoryDto)
        {
            var section = await _context.Sections.FindAsync(id);
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Created = DateTime.UtcNow,
                Section = section
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Put(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            category.Name = updateCategoryDto.Name;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
