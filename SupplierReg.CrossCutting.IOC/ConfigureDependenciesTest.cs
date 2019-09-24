using System;
using System.Collections.Generic;
using System.Text;
using SupplierReg.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SupplierReg.CrossCutting.IOC
{
    public class ConfigureDependenciesTest : ConfigureDependencies
    {
        protected override void ConfigurePersistenceAccess(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SupplierContext>(options =>
                options.UseInMemoryDatabase("SupplierRegDB"));
        }
    }
}
