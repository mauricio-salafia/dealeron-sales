using Dealeron.SalesTaxes.Application.Common.Interfaces;
using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Dealeron.SalesTaxes.Application.Billing.Commands
{
    public class CreateBillingCommandHandler : IRequestHandler<CreateBillingCommand, OperationResult>
    {
        private readonly IFileService _filService;
        private readonly IBillingService _billingService;
        private readonly ILogger<CreateBillingCommandHandler> _logger;

        public CreateBillingCommandHandler(IFileService filService,
            IBillingService billingService,
            ILogger<CreateBillingCommandHandler> logger)
        {
            _filService = filService;
            _billingService = billingService;
            _logger = logger;
        }

        public async Task<OperationResult> Handle(CreateBillingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling CreateBillingCommand for {request.Name}");
            var result = new OperationResult();
            try
            {
                var billingFile = await _filService.ProcessFileAsync(request.Content, cancellationToken);
                var response = await _billingService.CreateBillAsync(billingFile, cancellationToken);
                result = OperationResult.CreateSucces(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There was an error handling CreateBillingCommand for {request.Name}");
                result = OperationResult.CreateFail(new List<string> { ex.Message });
            }

            return result;

        }
    }
}
