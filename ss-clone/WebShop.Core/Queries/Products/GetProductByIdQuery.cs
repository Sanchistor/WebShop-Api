using MediatR;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.IRepositories;

namespace WebShop.WebShop.Core.Queries.Products
{
    public class GetProductByIdQuery : IRequest<GetProductResponse>
    {
        public int Id { get; set; }
    }
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductResponse>
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductByIdHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<GetProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productsRepository.getProductById(request.Id);
        }
    }
}
