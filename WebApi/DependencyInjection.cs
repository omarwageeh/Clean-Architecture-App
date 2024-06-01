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
using Application;
using Domain;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMyDependencyGroup(
                 this IServiceCollection services)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddScoped<IMediator, Mediator>();
            services.AddMediatR((config) =>
            {
                config.RegisterServicesFromAssemblies(ApplicationAssembly.Instance);
            });


            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var interfaceType = typeof(DomainAssembly).Assembly.GetTypes().Where(type => type.Name.EndsWith("Repo") && type.IsInterface);
            var types = typeof(InfrastructureAssembly).Assembly.GetTypes().Where(t => t.Name.EndsWith("Repo") && t.IsClass && !t.IsAbstract);

            foreach (var type in types)
            {
                var matchingInterface = interfaceType.FirstOrDefault(interfaceType => interfaceType.Name == $"I{type.Name}");
                if (matchingInterface != null)
                {
                    services.AddScoped(matchingInterface, type);
                }
            }
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
 }
