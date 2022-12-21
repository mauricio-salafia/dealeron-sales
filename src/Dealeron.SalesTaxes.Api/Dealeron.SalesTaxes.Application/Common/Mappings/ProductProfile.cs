using AutoMapper;
using Dealeron.SalesTaxes.Application.Product.Commands;

namespace Dealeron.SalesTaxes.Application.Common.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Domain.Models.Product>()
                .ForMember(dest => dest.ProductId, opts => opts.Ignore());
        }
    }
}
