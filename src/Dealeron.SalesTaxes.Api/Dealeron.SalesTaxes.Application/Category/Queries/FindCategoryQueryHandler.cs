using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dealeron.SalesTaxes.Application.Common.Interfaces;
using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Dealeron.SalesTaxes.Application.Category.Queries
{
    public class FindCategoryQueryHandler : IRequestHandler<FindCategoryQuery, OperationResult>
    {
        private readonly ILogger<FindCategoryQueryHandler> _logger;
        private readonly ISalesTaxesRepository<Domain.Models.Category> _repository;
        private readonly IMapper _mapper;

        public FindCategoryQueryHandler(ILoggerFactory loggerFactory, 
            ISalesTaxesRepository<Domain.Models.Category> repository,
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<FindCategoryQueryHandler>();
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(FindCategoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Looking for category with Id:{request.CategoryId}");
            var response = new OperationResult();
            try
            {
                var result = await _repository.FindByIdAsync(request.CategoryId, cancellationToken: cancellationToken);
                if (result != null)
                {
                    response = OperationResult.CreateSucces(result);
                }
                else
                {
                    response = OperationResult.CreateFail(new List<string> { $"Category with ID:{request.CategoryId} NOT FOUND" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There was an error looking for the category with Id:{request.CategoryId}.");
                response = OperationResult.CreateFail(new List<string> { ex.Message });
            }

            return response;
        }
    }
}
