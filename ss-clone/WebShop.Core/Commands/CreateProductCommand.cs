using MediatR;
using ss_clone.Data;
using WebShop.WebShop.Core.Dto.Products;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Commands
{
    public class CreateProductCommand :IRequest<Product>
    {
        public CreateProductDto CreateProductDto { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductsRepository _productsRepository;
        public CreateProductCommandHandler(IProductsRepository productsRepository) {  _productsRepository = productsRepository; }
        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productsRepository.Post(request.CreateProductDto);
        }
    }
}
