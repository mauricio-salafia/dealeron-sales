using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;

namespace Dealeron.SalesTaxes.Application.Category.Commands
{
    public sealed class UpdateCategoryCommand : IRequest<OperationResult>
    {
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal TaxToApply { get; set; }
    }
}
