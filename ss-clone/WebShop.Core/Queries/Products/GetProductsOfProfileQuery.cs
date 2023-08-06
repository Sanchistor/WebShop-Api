using MediatR;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.IRepositories;

namespace WebShop.WebShop.Core.Queries.Products
{
    public class GetProductsOfProfileQuery : IRequest<List<ProductResponse>>
    {
        public int Id { get; set; }
    }
    public class GetProductsOfProfileHandler : IRequestHandler<GetProductsOfProfileQuery, List<ProductResponse>>
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductsOfProfileHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<List<ProductResponse>> Handle(GetProductsOfProfileQuery request, CancellationToken cancellationToken)
        {
            return await _productsRepository.getProductsOfProfile(request.Id);
        }
    }
}
