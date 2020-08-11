using ApiTeste.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository.Startup
{
    public static class RepositoryStartup
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPessoasRepository, PessoasRepository>();
        }
    }
}
