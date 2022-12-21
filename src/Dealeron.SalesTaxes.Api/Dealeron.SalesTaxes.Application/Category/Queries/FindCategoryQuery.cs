using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;

namespace Dealeron.SalesTaxes.Application.Category.Queries
{
    public class FindCategoryQuery : IRequest<OperationResult>
    {
        public int CategoryId { get; set; }
    }
}
