using MediatR;
using WebShop.WebShop.Core.IRepositories;

namespace WebShop.WebShop.Core.Queries.Categories
{
    public class DeleteCategoryQuery:IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryQuery>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Unit> Handle(DeleteCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.Delete(request.Id);
        }
    }
}
