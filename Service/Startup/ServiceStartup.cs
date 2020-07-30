using ApiTeste.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Service.Startup
{
    public static class ServiceStartup
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IVendasService, VendasService>();
        }
    }
}
