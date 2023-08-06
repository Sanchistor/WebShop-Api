using MediatR;
using WebShop.WebShop.Core.Dto.Products;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Commands.Products
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public UpdateProductDto updateProductDto { get; set; }
    }
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductsRepository _productsRepository;
        public UpdateProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productsRepository.Put(request.Id, request.updateProductDto);
        }
    }
}
