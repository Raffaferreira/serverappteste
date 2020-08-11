using ApiTeste.Commands.Vendas;
using ApiTeste.Core.DTO;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Commands.Startup
{
    public static class FiltersStartup
    {
        public static void AddFluentValidationStartup(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<Pessoa>, PessoaCommand>();
        }
    }
}
