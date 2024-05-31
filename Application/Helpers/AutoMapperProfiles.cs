using Application.Commands;
using Application.Dtos.Common;
using Application.Dtos.Create;
using Application.Dtos.Get;
using Application.Dtos.Update;
using AutoMapper;
using Domain.Entitties;

namespace WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<GetOrderDto, Order>().ReverseMap();
            CreateMap<CreateOrderDto,Order >()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.CustomerDetails, opt => opt.MapFrom(src => src.CustomerDetails)).ReverseMap();


            CreateMap<OrderDetails, CreateOrderDetailsDto>().ForMember(des => des.ProductId, opt => opt.MapFrom(src => src.ProductId)).ReverseMap();
            CreateMap<OrderDetails, GetOrderDetailsDto>();
            CreateMap<OrderDetails, UpdateOrderDetailsDto>().ReverseMap();
            CreateMap<CustomerDetailsDto, CustomerDetails>().ReverseMap();


            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();


            CreateMap<CreateOrderCommand, Order>().ForMember(o=>o.OrderDetails, opt=>opt.MapFrom(coc=>coc.OrderDetails)).ReverseMap();

            CreateMap<Order, UpdateOrderCommand>()
                .ForMember(des => des.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(des => des.CustomerDetails, opt => opt.MapFrom(src => src.CustomerDetails)).ReverseMap();

        }
    }
}
