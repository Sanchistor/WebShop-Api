using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.WebShop.Core.Dto.Categories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.IRepositories
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> Get();
        public Task<Category> GetById(int id);
        public Task<Category> Post(int id, CreateCategoryDto createCategoryDto);
        public Task<Unit> Delete(int id);
        public Task<Category> Put(int id, UpdateCategoryDto updateCategoryDto);

    }
}
