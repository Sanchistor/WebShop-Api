using MediatR;
using WebShop.WebShop.Core.Dto.Products;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.IRepositories;

namespace WebShop.WebShop.Core.Commands.Products
{
    public class FilterProductsByPriceCommand : IRequest<List<ProductResponse>>
    {
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
    }

    public class FilterProductsByPriceHandler : IRequestHandler<FilterProductsByPriceCommand, List<ProductResponse>>
    {
        private readonly IProductsRepository _productsRepository;
        public FilterProductsByPriceHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<List<ProductResponse>> Handle(FilterProductsByPriceCommand request, CancellationToken cancellationToken)
        {
            var dto = new GetProductsByPriceDto
            {
                maxPrice = request.MaxPrice,
                minPrice = request.MinPrice
            };
            return await _productsRepository.getProductsByPrice(dto);
        }
    }
}
