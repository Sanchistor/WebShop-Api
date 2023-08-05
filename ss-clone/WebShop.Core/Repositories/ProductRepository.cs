using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.Products;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Repositories
{
    public class ProductRepository : IProductsRepository
    {
        private readonly ApiDbContext _context;
        public ProductRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<GetProductResponse> getProductById(int id)
        {
            var product = await _context.Products
            .Where(c => c.Id == id)
            .Include(c => c.Profile)
            .Include(c => c.Section)
            .Include(c => c.Category)
            .Include(c => c.Brand)
            .Select(c => new GetProductResponse
            {
                Id = c.Id,
                Year = c.Year,
                ShortBio = c.ShortBio,
                Description = c.Description,
                Photo = c.Photo,
                Price = c.Price,
                Created = c.Created,
                Updated = c.Updated,
                Profile = new ProfileDTO
                {
                    Id = c.Profile.Id,
                    NickName = c.Profile.NickName,
                    Created = c.Profile.Created
                },
                Section = new SectionDto
                {
                    Id = c.Section.Id,
                    Name = c.Section.Name,
                    Created = c.Section.Created
                },
                Category = new CategoryDto
                {
                    Id = c.Category.Id,
                    Name = c.Category.Name,
                    Created = c.Category.Created
                },
                Brand = new BrandDto
                {
                    Id = c.Brand.Id,
                    Name = c.Brand.Name,
                    Created = c.Brand.Created
                }
            })
            .FirstOrDefaultAsync();
            return product;
        }

        public async Task<List<ProductResponse>> getProductsByPrice(GetProductsByPriceDto getProductsByPriceDto)
        {
            var filteredProducts = await _context.Products
                .Where(c => c.Price >= getProductsByPriceDto.minPrice && c.Price <= getProductsByPriceDto.maxPrice)
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
            return filteredProducts;
        }

        public async Task<List<ProductResponse>> getProductsOfBrand(int id)
        {
            var productsOfBrand = await _context.Products
                .Include(c => c.Brand)
                .Where(c => c.BrandId == id)
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
            return productsOfBrand;
        }

        public async Task<List<ProductResponse>> getProductsOfProfile(int id)
        {
            var products = await _context.Products
            .Where(c => c.ProfileId == id)
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
            
            return products;
        }

        public async Task<Product> Post(CreateProductDto createProductDto)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(c => c.Id == createProductDto.ProfileId);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == createProductDto.CategoryId);
            var section = await _context.Sections.FirstOrDefaultAsync(c => c.Id == createProductDto.SectionId);
            var brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == createProductDto.BrandId);
            

            var product = new Product
            {
                Year = createProductDto.Year,
                Photo = createProductDto.Photo,
                Price = createProductDto.Price,
                ShortBio = createProductDto.ShortBio,
                Description = createProductDto.Description,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Profile = profile,
                Section = section,
                Brand = brand,
                Category = category
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
