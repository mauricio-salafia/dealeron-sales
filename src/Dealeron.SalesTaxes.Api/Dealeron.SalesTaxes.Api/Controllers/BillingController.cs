using Dealeron.SalesTaxes.Api.Models;
using Dealeron.SalesTaxes.Application.Billing.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dealeron.SalesTaxes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly ILogger<BillingController> _logger;
        private readonly IMediator _mediator;

        public BillingController(ILoggerFactory loggerFactory,
            IMediator mediator)
        {
            _logger = loggerFactory.CreateLogger<BillingController>();
            _mediator = mediator;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult> UploadFile([FromForm] FileUploadModel fileDetails, CancellationToken cancellationToken)
        {
            string content = string.Empty;
            string filename = fileDetails.FileDetails.FileName;
            using (var stream = fileDetails.FileDetails.OpenReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    content = await reader.ReadToEndAsync();
                }
            }

            var command = new CreateBillingCommand
            {
                Content = content,
                Name = filename
            };

            var response = await _mediator.Send(command, cancellationToken);
            if(response.Status != Application.Common.Models.OperationResultEnum.Succes)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = string.Join(".", response.Errors) });
            }

            var path = Path.Combine(Environment.CurrentDirectory, "output" + filename);
            var dataBytes = System.Text.Encoding.UTF8.GetBytes(response.ObjectData?.ToString());
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await fileStream.WriteAsync(dataBytes, 0, dataBytes.Length, cancellationToken);
            }

            return Ok($"Ouput path: {path}");
        }
    }
}
