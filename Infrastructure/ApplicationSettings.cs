using ApiTeste.Infrastructure.Model;

namespace ApiTeste.Infrastructure
{
    public class ApplicationSettings
    {
        public DatabaseSettings DatabaseSettings { get; set; }
        public TokenConfigurations TokenConfigurations { get; set; }
    }
}
