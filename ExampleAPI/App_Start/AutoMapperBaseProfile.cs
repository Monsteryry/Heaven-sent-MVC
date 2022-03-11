using AutoMapper;
using Entities.Models;
using ExampleApi.Models.InputModels.Categories;
using ExampleApi.Models.OutputModels.Categories;
using API.Models.InputModels.Products;
using API.Models.OutputModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.App_Start
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            CreateMap<ProductInputModel, Product>()
                 .ForMember(x => x.Id, d => d.Ignore())
                 .ForMember(x => x.Category, d => d.Ignore())
                 .ForMember(x => x.Orders, d => d.Ignore());

            CreateMap<Product, ProductOutputModel>()
                .ForMember(x => x.CategoryName, d => d.MapFrom(s => s.Category.Name));

            CreateMap<CategoryInputModel, Category>()
                .ForMember(x => x.Id, d => d.Ignore())
                .ForMember(x => x.Products, d => d.Ignore());

            CreateMap<Category, CategoryOutputModel>();
        }   

    }
}