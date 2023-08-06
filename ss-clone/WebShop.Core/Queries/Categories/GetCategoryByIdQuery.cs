using MediatR;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Queries.Categories
{
    public class GetCategoryByIdQuery:IRequest<Category>
    {
        public int Id { get; set; }
    }
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetById(request.Id);
        }
    }
}
