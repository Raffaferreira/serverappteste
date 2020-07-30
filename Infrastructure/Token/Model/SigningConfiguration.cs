using ApiTeste.Infrastructure.Model;
using ApiTeste.Infrastructure.Token.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTeste.Infrastructure.Token.Model
{
    public class SigningConfiguration : ISigningConfiguration
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        protected readonly ApplicationSettings _applicationSettings;
        protected TokenConfigurations _tokenSettings;

        public SigningConfiguration(IOptions<ApplicationSettings> applicationSettings)
        {
            _applicationSettings = applicationSettings.Value;
            _tokenSettings = _applicationSettings.TokenConfigurations;

            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
