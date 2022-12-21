using AutoMapper;
using Dealeron.SalesTaxes.Application.Common.Interfaces;
using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Dealeron.SalesTaxes.Application.Product.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly ISalesTaxesRepository<Domain.Models.Product> _repository;

        public CreateProductCommandHandler(IMapper mapper,
            ILoggerFactory loggerFactory,
            ISalesTaxesRepository<Domain.Models.Product> repository)
        {
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<CreateProductCommandHandler>();
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a product");
            var response = new OperationResult();
            try
            {
                var product = _mapper.Map<Domain.Models.Product>(request);
                var result = await _repository.AddAsync(product, cancellationToken);
                response = OperationResult.CreateSucces(result.ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error.");
                response = OperationResult.CreateFail(new List<string> { ex.ToString() });
            }

            return response;
        }
    }
}
