using AutoMapper;
using FH.CatalogService.Application.Dtos;
using FH.CatalogService.Domain.Entities;

namespace FH.CatalogService.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>().
           ForMember(d => d.CategotyName, o => o.MapFrom(s => s.ProductCategory.Name))
           .ForMember(d => d.CategotyId, o => o.MapFrom(s => s.ProductCategory.Id));

        CreateMap<ProductAttributeValues, ProductAttributeValuesDto>()
                .ForMember(dest => dest.AttributeId, act => act.MapFrom(src => src.AttributeValue.ProductAttribute.Id))
                .ForMember(dest => dest.AttributeName, act => act.MapFrom(src => src.AttributeValue.ProductAttribute.Name))
                .ForMember(dest => dest.AttributeValueName, act => act.MapFrom(src => src.AttributeValue.ValueName));

        CreateMap<IReadOnlyList<ProductAttributeValues>, ICollection<ProductAttributeValuesDto>>();


        CreateMap<AttributeValue, AttributeValueDto>();
        CreateMap<ProductAttribute, ProductAttributeDto>();

        CreateMap<ProductAttribute, ProductCategoryAttributeDto>()
                .ForMember(dest => dest.AttributeId, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.AttributeName, act => act.MapFrom(src => src.Name));

        CreateMap<CategoryDto, ProductCategory>();
        CreateMap<ProductCategory, CategoryDto>()
         .ForMember(dto => dto.CategoryAttributes, opt => opt.MapFrom(x => x.ProductCategoryAttributes.Select(y => y.ProductAttribute).ToList()));



    }
}
