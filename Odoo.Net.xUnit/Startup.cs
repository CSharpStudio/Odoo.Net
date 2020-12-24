using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Odoo.Net.Core;
using Odoo.Net.Web;
using Odoo.Net.xUnit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odoo.Net.xUnit
{
    public class Startup
    {
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureAppConfiguration(builder => builder.AddJsonFile("appsettings.json"));
        }

        public void Configure(IServiceProvider serviceProvider)
        {
            var registry = serviceProvider.GetRequiredService<Registry>();
            registry.Register<Odoo.Net.Web.Base>();
            registry.Register<Bank>();
        }

        public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
        {
            var registry = new Registry();

            services.AddSingleton(registry);
        }
    }
}
