using System;
using System.Collections.Generic;
using System.Text;
using SupplierReg.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SupplierReg.Data.Repositories;
using SupplierReg.Domain.Repositories;
using MediatR;
using Infra.CrossCutting.Bus;
using Domain.Core.Bus;
using Domain.Core.Bus.Behaviour;
using Domain.Core.Bus.Messages.Events;
using Domain.Core.Bus.Messages.Events.Handlers;
using Domain.Core.Persistence;
using Log.Data;
using SupplierReg.Domain.Commands.CompanyContracts;
using Domain.Core.Models;
using SupplierReg.Domain.Commands._Handlers;
using Domain.Core.Identity;
using SupplierReg.Domain.Commands.SupplierContracts;

namespace SupplierReg.CrossCutting.IOC
{
    public class ConfigureDependencies
    {
        public virtual void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurePersistenceAccess(services, configuration);
            ConfigurePersistence(services);
            ConfigureIdentity(services);
            ConfigureDomain(services);
            ConfigureApp(services, configuration);
        }


        protected virtual void ConfigurePersistenceAccess(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SupplierContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SupplierRegConnection")));
        }

        protected virtual void ConfigurePersistence(IServiceCollection services)
        {
            //Infra - Persistence - Repositories
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
        }

        protected virtual void ConfigureIdentity(IServiceCollection services)
        {
            //TODO: UserControl
            //services.AddScopped<ILoggedUser,>();
        }

        protected virtual void ConfigureDomain(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainErrorNotification>, DomainErrorHandler>();

            // Log
            services.AddScoped<IEventStore, EventStore>();

            //Domain - Commands
            services.AddScoped<IRequestHandler<AddCompanyCommand, bool>, CompanyHandler>();
            services.AddScoped<IRequestHandler<AddSupplierCommand, bool>, SupplierHandler>();
        }

        protected virtual void ConfigureApp(IServiceCollection services, IConfiguration configuration)
        {
            
        }
    }
}
