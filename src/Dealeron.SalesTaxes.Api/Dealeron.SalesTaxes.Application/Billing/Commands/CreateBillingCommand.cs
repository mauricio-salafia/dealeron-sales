using Dealeron.SalesTaxes.Application.Common.Models;
using MediatR;

namespace Dealeron.SalesTaxes.Application.Billing.Commands
{
    public class CreateBillingCommand : IRequest<OperationResult>
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
