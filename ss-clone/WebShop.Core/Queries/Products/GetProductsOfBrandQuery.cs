using MediatR;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.IRepositories;

namespace WebShop.WebShop.Core.Queries.Products
{
    public class GetProductsOfBrandQuery : IRequest<List<ProductResponse>>
    {
        public int Id { get; set; }
    }
    public class GetProductsOfBrandHandler : IRequestHandler<GetProductsOfBrandQuery, List<ProductResponse>>
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductsOfBrandHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<List<ProductResponse>> Handle(GetProductsOfBrandQuery request, CancellationToken cancellationToken)
        {
            return await _productsRepository.getProductsOfBrand(request.Id);
        }
    }
}
