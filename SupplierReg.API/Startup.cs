using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SupplierReg.API.Filters;
using SupplierReg.API.Mappings;
using SupplierReg.CrossCutting.IOC;

namespace SupplierReg.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Model binding error handling
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    var returnJson = actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => new Models.DTOs.ApiErrorDTO
                    {
                        Key = "invalid format",
                        Message = x.ErrorMessage,
                        Detail = x.Exception?.ToString()
                    }).ToArray();

                    return new BadRequestObjectResult(returnJson);
                };
                    
            });

            //CORS
            services.AddCors(o => o.AddPolicy("AnyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //Mediatr
            services.AddMediatR(typeof(Startup));

            //Automapper - Profiles
            services.AddAutoMapper(typeof(CompanyProfile), typeof(SupplierProfile));

            //TODO: change for production
            //ConfigureDependencies configureDependencies = new ConfigureDependencies();
            ConfigureDependenciesTest configureDependencies = new ConfigureDependenciesTest();

            configureDependencies.ConfigureService(services, Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AnyPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
