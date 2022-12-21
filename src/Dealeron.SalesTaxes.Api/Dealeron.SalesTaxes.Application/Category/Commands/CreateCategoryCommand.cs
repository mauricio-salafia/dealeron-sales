using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;

namespace Dealeron.SalesTaxes.Application.Category.Commands
{
    public class CreateCategoryCommand : IRequest<OperationResult>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal TaxToApply { get; set; }
    }
}
