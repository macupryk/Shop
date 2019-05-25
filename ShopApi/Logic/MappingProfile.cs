using AutoMapper;
using ShopApi.Entities;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApi.Logic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             //CreateMap(typeof(SearchInfo<>), typeof(SearchInfo<>)).MaxDepth(5);
             //CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).MaxDepth(5);
            // Add as many of these lines as you need to map your objects
            CreateMap<Item,ItemModel>();
 

        }
    }
}
