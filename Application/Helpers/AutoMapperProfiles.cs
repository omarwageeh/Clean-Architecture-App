using Application.Commands;
using Application.Dtos;
using AutoMapper;
using Domain.Entitties;

namespace WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<CreateOrderCommand, Order>().ForMember(coc=>coc.OrderDetails, opt=>opt.MapFrom(_=>Guid.Empty));

            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(des => des.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(des => des.CustomerDetails, opt => opt.MapFrom(src => src.CustomerDetails));

            CreateMap<OrderDetails, OrderDetailsDto>().ForMember(des => des.ProductId, opt => opt.MapFrom(src=>src.ProductId)).ReverseMap();
            CreateMap<CustomerDetails, CustomerDetailsDto>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.CustomerDetails, opt => opt.MapFrom(src => src.CustomerDetails));

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.CustomerDetails, opt => opt.MapFrom(src => src.CustomerDetails));

        }
    }
}
