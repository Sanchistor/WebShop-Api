using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShop.WebShop.Core.Dto.Products;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.Dto.Sort;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.IRepositories
{
    public interface IProductsRepository
    {
        public Task<List<ProductResponse>> getProductsOfProfile(int id);
        public Task<GetProductResponse> getProductById(int id);
        public Task<List<ProductResponse>> getProductsByPrice(GetProductsByPriceDto getProductsByPriceDto);
        public Task<List<ProductResponse>> getProductsOfBrand(int id);
        public Task<Product> Post(CreateProductDto createProductDto);
        public Task<List<ProductResponse>> SortByPhraseInDescription(SortByPhraseDto sortByPhraseDto);
        public Task<Product> Put(int id, UpdateProductDto updateProductDto);
        public Task<Unit> Delete(int id);
    }
}   