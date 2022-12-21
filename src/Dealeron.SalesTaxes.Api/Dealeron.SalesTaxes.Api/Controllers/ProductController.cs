using AutoMapper;
using Dealeron.SalesTaxes.Application.Product.Commands;
using Dealeron.SalesTaxes.Contract.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dealeron.SalesTaxes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(ILoggerFactory loggerFactory, 
            IMediator mediator, 
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<ProductController>();
            _mediator = mediator;
            _mapper = mapper;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateProductRequest request)
        {
            _logger.LogInformation("Creating a product");
            var command = _mapper.Map<CreateProductCommand>(request);
            var result = await _mediator.Send(command);
            if (result.Status == Application.Common.Models.OperationResultEnum.Succes)
            {
                var productId = (int)result.ObjectData;
                _logger.LogInformation($"Product created with Id:{productId}");
                return Ok(productId);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = string.Join(".", result.Errors) });
        }
    }
}
