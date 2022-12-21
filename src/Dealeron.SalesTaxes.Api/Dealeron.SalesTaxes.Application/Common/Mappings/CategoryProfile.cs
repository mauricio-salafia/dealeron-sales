using AutoMapper;
using Dealeron.SalesTaxes.Application.Category.Commands;
using Dealeron.SalesTaxes.Application.Category.Queries;

namespace Dealeron.SalesTaxes.Application.Common.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Domain.Models.Category>()
                .ForMember(dest => dest.CategoryId, ops => ops.Ignore());

            CreateMap<UpdateCategoryCommand, Domain.Models.Category>();
        }
    }
}
