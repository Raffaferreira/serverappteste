using ApiTeste.Infrastructure.Token.Interface;
using ApiTeste.Infrastructure.Token.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Infrastructure.Token
{
    public static class TokenConfiguration
    {
        public static void AddTokenConfiguration(this IServiceCollection services)
        {
            services.AddTransient<ISigningConfiguration, SigningConfiguration>();
            var sp = services.BuildServiceProvider();
            var TokenKey = sp.GetService<ISigningConfiguration>();

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = TokenKey.Key;
                paramsValidation.ValidAudience = "valid";
                paramsValidation.ValidIssuer = "issuer";
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build());
            });
        }
    }
}
