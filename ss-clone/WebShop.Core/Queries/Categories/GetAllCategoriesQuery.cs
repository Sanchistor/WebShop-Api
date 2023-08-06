using MediatR;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Queries.Categories
{
    public class GetAllCategoriesQuery:IRequest<List<Category>>
    {
    }
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.Get();
        }
    }
}
