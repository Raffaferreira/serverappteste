﻿using ApiTeste.Core.DTO;
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

namespace ApiTeste.Handlers.Controllers.Pessoas
{
    public class EditarPessoaHandler : ResponseError, IRequestHandler<AtualizarPessoa, Response>
    {
        public readonly IPessoasService _pessoasService;

        public EditarPessoaHandler(IPessoasService pessoaService)
        {
            _pessoasService = pessoaService;
        }

        public async Task<Response> Handle(AtualizarPessoa request, CancellationToken cancellationToken)
        {
            List<Message> validations;
            Message customMessage;
            dynamic message = null;

            try
            {
                IResponseLayer response = await _pessoasService.AtualizarDadosPessoa(request);
                customMessage = Convert.ChangeType(response.Result, typeof(Message));

                if (!response.HasErrors)
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

            return new Response(message, customMessage.StatusCode);
        }
    }
}
