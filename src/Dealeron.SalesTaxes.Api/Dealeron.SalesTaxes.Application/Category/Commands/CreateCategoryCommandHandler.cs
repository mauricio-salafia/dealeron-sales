using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealeron.SalesTaxes.Domain.Models;
using Dealeron.SalesTaxes.Application.Common.Interfaces;
using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Dealeron.SalesTaxes.Application.Category.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, OperationResult>
    {
        public readonly ISalesTaxesRepository<Domain.Models.Category> _repository;
        public readonly ILogger<CreateCategoryCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(
            ISalesTaxesRepository<Domain.Models.Category> repository,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<CreateCategoryCommandHandler>();
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateCategoryCommand");
            OperationResult operationResult = new OperationResult();
            try
            {
                var categoryEntity = _mapper.Map<Domain.Models.Category>(request);
                
                if (categoryEntity.TaxToApply >= 0)
                {
                    var result = await _repository.AddAsync(categoryEntity, cancellationToken);
                    operationResult = OperationResult.CreateSucces(result.CategoryId);
                }
                else
                {
                    _logger.LogWarning("Tax to apply negative");
                    operationResult = await Task.FromResult(OperationResult.CreateFail(new List<string> { "Tax to apply non positive" }));
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Unexpected errors: {ex}");
                operationResult = OperationResult.CreateFail(new List<string> { ex.Message });
            }

            return operationResult;

        }
    }
}
