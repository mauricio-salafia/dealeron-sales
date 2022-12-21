using AutoMapper;
using Dealeron.SalesTaxes.Application.Product.Commands;
using Dealeron.SalesTaxes.Contract.Product;

namespace Dealeron.SalesTaxes.Api.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
        }
    }
}
