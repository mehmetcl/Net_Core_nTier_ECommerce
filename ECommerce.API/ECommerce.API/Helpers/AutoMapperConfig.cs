﻿using AutoMapper;
using ECommerce.API.Helpers;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
namespace ECommerce.API.Helpers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<Category, CategoryWithProductDto>();
            CreateMap<CategoryWithProductDto, Category>();
          
        }
    }
}
