using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_clone.Data;
using WebShop.WebShop.Core.Commands.Categories;
using WebShop.WebShop.Core.Dto.Categories;
using WebShop.WebShop.Core.Queries.Categories;
using WebShop.WebShop.Data.Models;

namespace ss_clone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
           return await _mediator.Send(new GetAllCategoriesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });
            if(category == null)
            {
                return NotFound("Category not found!");
            }
            return category;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Category>> Post(int id, CreateCategoryDto createCategoryDto)
        {
            var command = new CreateCategoryCommand { Id = id, createCategoryDto = createCategoryDto };
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            await _mediator.Send(new DeleteCategoryQuery() { Id = id });
            return Ok("Category successfully deleted!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Put(int id, UpdateCategoryDto updateCategoryDto)
        {
            await _mediator.Send(new UpdateCategoryCommand() { Id = id, updateCategoryDto = updateCategoryDto });
            return Ok("Category is updated");
        }
    }
}
