using AutoMapper;
using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.BLL.Dtos
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Source --->  Target
            CreateMap<Product, GetProductDto>();
            CreateMap<PostProductDto, Product>();

        }
    }
}
