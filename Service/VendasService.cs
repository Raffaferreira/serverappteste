using ApiTeste.Core.DTO;
using ApiTeste.Handlers;
using ApiTeste.Service.Base;
using ApiTeste.Service.Interface;
using ApiTeste.Service.Model;
using ApiTeste.Service.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ApiTeste.Service
{
    public class VendasService : ServiceBase, IVendasService
    {
        public readonly ILogger<VendasService> _logger;
        public readonly IResponseLayer _responseLayers;

        public VendasService(ILogger<VendasService> logger,
            IResponseLayer responseLayer) : base(logger)
        {
            _responseLayers = responseLayer;
            _logger = logger;
        }

        public async Task<IResponseLayer> GetListaIngressosPorLote(ListaIngressos getingressos)
        {
            Message response;
            IResponseLayer responseService = null;
            string mensagem = string.Empty;

            try
            {
                using (var validate = new GetIngressosValidator(getingressos))
                {
                    if (validate.IsValid)
                    {

                    }
                    else
                    {

                    }
                }

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                mensagem = "Houve uma falha na solicitação do ingresso!";
                response = new Message(HttpStatusCode.InternalServerError, mensagem);
                responseService = _responseLayers.AddMessage(response.Executed, response);
            }

            return responseService;
        }

        public async Task<IResponseLayer> VenderIngresso(Ingresso ingresso)
        {
            Message response;
            IResponseLayer responseService = null;
            string mensagem = string.Empty;

            try
            {
                using (var validate = new IngressoValidator(ingresso))
                {
                    if (validate.IsValid)
                    {

                    }
                    else
                    {

                    }
                }

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                mensagem = "Houve uma falha na solicitação do ingresso!";
                response = new Message(HttpStatusCode.InternalServerError, mensagem);
                responseService = _responseLayers.AddMessage(response.Executed, response);
            }

            return responseService;
        }

    }
}
