using MediatR;
using WebShop.WebShop.Core.IRepositories;

namespace WebShop.WebShop.Core.Queries.Products
{
    public class DeleteProductQuery : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteProductHandler : IRequestHandler<DeleteProductQuery>
    {
        private readonly IProductsRepository _productsRepository;
        public DeleteProductHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Unit> Handle(DeleteProductQuery request, CancellationToken cancellationToken)
        {
            return await _productsRepository.Delete(request.Id);
        }
    }
}
