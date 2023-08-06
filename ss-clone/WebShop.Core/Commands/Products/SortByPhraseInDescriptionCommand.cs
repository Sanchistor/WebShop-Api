using MediatR;
using WebShop.WebShop.Core.Dto.Response;
using WebShop.WebShop.Core.Dto.Sort;
using WebShop.WebShop.Core.IRepositories;

namespace WebShop.WebShop.Core.Commands.Products
{
    public class SortByPhraseInDescriptionCommand : IRequest<List<ProductResponse>>
    {
        public SortByPhraseDto sortByPhraseDto { get; set; }
    }

    public class SortByPhraseInDescriptionHandler : IRequestHandler<SortByPhraseInDescriptionCommand, List<ProductResponse>>
    {
        private readonly IProductsRepository _productsRepository;
        public SortByPhraseInDescriptionHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<List<ProductResponse>> Handle(SortByPhraseInDescriptionCommand request, CancellationToken cancellationToken)
        {
            return await _productsRepository.SortByPhraseInDescription(request.sortByPhraseDto);
        }
    }
}
