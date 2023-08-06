using MediatR;
using WebShop.WebShop.Core.Dto.Categories;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Commands.Categories
{
    public class CreateCategoryCommand: IRequest<Category>
    {
        public int Id { get; set; }
        public CreateCategoryDto createCategoryDto { get; set; }
    }
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.Post(request.Id, request.createCategoryDto);
        }
    }
}
