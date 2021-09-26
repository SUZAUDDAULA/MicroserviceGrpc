using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using ProductGrpc.Data.Entities.Product;
using ProductGrpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpc.Mapper
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.CreatedTime,opt => opt.MapFrom(src => Timestamp.FromDateTime(Convert.ToDateTime(src.createdAt))));

            CreateMap<ProductModel, Product>()
                .ForMember(dest => dest.createdAt,opt => opt.MapFrom(src => src.CreatedTime.ToDateTime()));

            //CreateMap<Models.Product, ProductModel>()
            //    .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => Timestamp.FromDateTime(src.CreatedTime)));

            //CreateMap<ProductModel, Models.Product>()
            //    .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.ToDateTime()));
        }
    }
}
