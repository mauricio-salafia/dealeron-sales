using AutoMapper;
using Dealeron.SalesTaxes.Application;
using Dealeron.SalesTaxes.Application.Category.Commands;
using Dealeron.SalesTaxes.Contract;
using Dealeron.SalesTaxes.Contract.Category;

namespace Dealeron.SalesTaxes.Api.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
            CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
            CreateMap<Domain.Models.Category, FindCategoryResponse>();
        }
    }
}
