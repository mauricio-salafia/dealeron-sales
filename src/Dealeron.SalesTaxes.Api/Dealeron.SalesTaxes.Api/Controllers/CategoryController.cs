using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dealeron.SalesTaxes.Contract.Category;
using Dealeron.SalesTaxes.Application.Category.Commands;
using MediatR;
using AutoMapper;
using Dealeron.SalesTaxes.Application.Category.Queries;

namespace Dealeron.SalesTaxes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(ILoggerFactory loggerFactory,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<CategoryController>();
            _mediator = mediator;
            _mapper = mapper;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory(CreateCategoryRequest request)
        {
            _logger.LogInformation("Creating a category");
            var command = _mapper.Map<CreateCategoryCommand>(request);
            var result = await _mediator.Send(command);
            if (result.Status == Application.Common.Models.OperationResultEnum.Succes)
            {
                var categoryId = (int)result.ObjectData;
                _logger.LogInformation($"Category created with Id:{categoryId}");
                return Ok(categoryId);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = string.Join(".", result.Errors) });

        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPut]
        public async Task<ActionResult> UpdateCategory(UpdateCategoryRequest request)
        {
            _logger.LogInformation("Updating a category");
            var command = _mapper.Map<UpdateCategoryCommand>(request);
            var result = await _mediator.Send(command);
            if (result.Status == Application.Common.Models.OperationResultEnum.Succes)
            {
                _logger.LogInformation($"Category updated with Id:{request.CategoryId}");
                return NoContent();
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = string.Join(".", result.Errors) });
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public async Task<ActionResult<FindCategoryResponse>> GetCategory([FromRoute] int id)
        {
            _logger.LogInformation($"Looking for the category with ID: {id}");
            if (id <= 0)
                return BadRequest();

            var query = new FindCategoryQuery { CategoryId = id };
            var result = await _mediator.Send(query);
            if (result.Status == Application.Common.Models.OperationResultEnum.Succes)
            {
                var category = (Domain.Models.Category)result.ObjectData;
                var response = _mapper.Map<FindCategoryResponse>(category);
                _logger.LogInformation($"Category found with Id:{id}");
                return Ok(response);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = string.Join(".", result.Errors) });
        }
    }
}
