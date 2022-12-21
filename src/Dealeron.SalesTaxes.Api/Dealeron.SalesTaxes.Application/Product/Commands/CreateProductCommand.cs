using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;

namespace Dealeron.SalesTaxes.Application.Product.Commands
{
    public class CreateProductCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
