using MediatR;
using WebShop.WebShop.Core.Dto.Categories;
using WebShop.WebShop.Core.IRepositories;
using WebShop.WebShop.Data.Models;

namespace WebShop.WebShop.Core.Commands.Categories
{
    public class UpdateCategoryCommand:IRequest<Category>
    {
        public int Id { get; set; }
        public UpdateCategoryDto updateCategoryDto { get; set; }
    }
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.Put(request.Id, request.updateCategoryDto);
        }
    }
}
