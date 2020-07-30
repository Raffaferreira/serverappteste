using ApiTeste.Core.DTO;
using ApiTeste.Infrastructure.Model;
using ApiTeste.Service.Interface;
using ApiTeste.Service.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTeste.Handlers.Controllers.Vendas
{
    public class GetIngressosHandler : ResponseError, IRequestHandler<ListaIngressos, Response>
    {
        public readonly IVendasService _vendasService;

        public GetIngressosHandler(IVendasService vendasService)
        {
            _vendasService = vendasService;
        }

        public async Task<Response> Handle(ListaIngressos request, CancellationToken cancellationToken)
        {
            List<Message> validations;
            Message customMessage;
            dynamic message = null;

            try
            {
                IResponseLayer response = await _vendasService.GetListaIngressosPorLote(request);
                customMessage = Convert.ChangeType(response.Result, typeof(Message));

                if (response.HasErrors)
                {
                    message = customMessage.Result;
                }
                else
                {
                    validations = new List<Message>();

                    if (response.Result is List<Message>)
                    {
                        validations.AddRange(response.Result);
                    }
                    else
                    {
                        validations.Add(response.Result);
                    }

                    return await Errors(validations);
                }
            }
            catch (Exception ex)
            {
                return await Errors(new Message(HttpStatusCode.InternalServerError, ex.Message));
            }

            return new Response(customMessage.Result, customMessage.StatusCode);
        }
    }
}
