using Application.Commands;
using Application.Handlers;
using Application.Queries;
using AutoMapper;
using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Contracts.UnitofWork;
using Infrastructure;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.UnitOfWork;
using MediatR;
using WebApi.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMyDependencyGroup(
                 this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderDetailsRepo, OrderDetailsRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerDetailsRepo, CustomerDetailsRepo>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR((config) =>
            {
                config.RegisterServicesFromAssemblies(typeof(AddProductCommandHandler).Assembly, 
                    typeof(GetProductByIdQuery).Assembly, 
                    typeof(AddProductCommand).Assembly, 
                    typeof(GetProductByIdQueryHandler).Assembly, 
                    typeof(DeleteProductCommandHandler).Assembly, 
                    typeof(DeleteProductCommand).Assembly, 
                    typeof(UpdateProductCommand).Assembly,
                    typeof(UpdateProductCommandHandler).Assembly
                    );
            });


            return services;
        }

        //public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
        //{
        //    var interfaceType = assembly.GetType("IGenericRepo");
        //    var types = assembly.GetTypes().Where(t => t.Name.EndsWith("Repo") && t.IsClass && !t.IsAbstract);

        //    foreach (var type in types)
        //    {                
        //        if (interfaceType != null)
        //        {
        //            services.AddScoped(interfaceType, type);
        //        }
        //    }

        //    return services;
        //}
    }
 }
