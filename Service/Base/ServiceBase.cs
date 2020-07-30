using Microsoft.Extensions.Logging;

namespace ApiTeste.Service.Base
{
    public class ServiceBase
    {
        protected readonly ILogger _logger;
        public ServiceBase(ILogger logger)
        {
            _logger = logger;
        }
    }
}
