using ApiTeste.Core.DTO;
using ApiTeste.Handlers;
using ApiTeste.Repository.Interface;
using ApiTeste.Repository.Models;
using ApiTeste.Service.Base;
using ApiTeste.Service.Interface;
using ApiTeste.Service.Model;
using ApiTeste.Service.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApiTeste.Service
{
    public class PessoasService : ServiceBase, IPessoasService
    {
        public readonly ILogger<PessoasService> _logger;
        public readonly IResponseLayer _responseLayers;
        public readonly IPessoasRepository _pessoasRepository;

        public PessoasService(ILogger<PessoasService> logger,
            IResponseLayer responseLayer,
            IPessoasRepository pessoasRepository) : base(logger)
        {
            _pessoasRepository = pessoasRepository;
            _responseLayers = responseLayer;
            _logger = logger;
        }

        public async Task<IResponseLayer> GetListaDePessoas()
        {
            Message response = null;
            IResponseLayer responseService = null;
            string mensagem = string.Empty;

            try
            {
                IResponseLayer responseRepository = await _pessoasRepository.GetListaPessoas();

                if (responseRepository.HasErrors)
                {
                    mensagem = "Falha na solicitação, desculpe-nos! Tente após alguns minutos!";
                    response = new Message(HttpStatusCode.BadRequest, mensagem);
                }
                else
                {
                    List<PessoaModel> pessoasRetorno = Convert.ChangeType(responseRepository.Result, typeof(List<PessoaModel>));
                    response = new Message(HttpStatusCode.OK, "", pessoasRetorno);
                }

                responseService = _responseLayers.AddMessage(response.Executed, response);
            }
            catch (Exception ex)
            {
                mensagem = "Houve uma falha na solicitação!";
                response = new Message(HttpStatusCode.InternalServerError, mensagem);
                responseService = _responseLayers.AddMessage(response.Executed, response);
            }

            return responseService;
        }

        public async Task<IResponseLayer> CadastrarPessoa(Pessoa pessoa)
        {
            Message response = null;
            IResponseLayer responseService = null;
            string mensagem = string.Empty;

            try
            {
                using (var validate = new CadastrarPessoaValidator(pessoa))
                {
                    if (validate.IsValid)
                    {
                        IResponseLayer cadastro = await _pessoasRepository.CadastrarPessoa(pessoa);

                        if (cadastro.HasErrors && cadastro.Result == 0)
                        {
                            mensagem = "Falha ao cadastrar pessoa, desculpe-nos! Tente mais tarde.";
                            response = new Message(HttpStatusCode.InternalServerError, mensagem);
                        }
                        else
                        {
                            int IdPessoa = Convert.ChangeType(cadastro.Result, typeof(int));

                            var pessoaEncontrada = await _pessoasRepository.GetPessoaById(IdPessoa);

                            if (!pessoaEncontrada.HasErrors)
                            {
                                PessoaModel pessoaResponse = Convert.ChangeType(pessoaEncontrada.Result, typeof(PessoaModel));

                                response = new Message(HttpStatusCode.OK, "", pessoaResponse);
                            }
                        }
                    }
                    else
                    {
                        mensagem = "Houverão erros de validação!";
                        response = new Message(HttpStatusCode.InternalServerError, mensagem, validate._messages);
                    }
                }

                responseService = _responseLayers.AddMessage(response.Executed, response);
            }
            catch (Exception ex)
            {
                mensagem = "Houve uma falha na solicitação!";
                response = new Message(HttpStatusCode.InternalServerError, mensagem);
                responseService = _responseLayers.AddMessage(response.Executed, response);
            }

            return responseService;
        }

        public async Task<IResponseLayer> DeletarPessoaPorCodigo(DeletarPessoa deletarPessoa)
        {
            Message response = null;
            IResponseLayer responseService = null;
            string mensagem = string.Empty;

            try
            {
                using (var deletar = new DeletarPessoaValidator(deletarPessoa))
                {
                    if (deletar.IsValid)
                    {
                        var deletarResponse = await _pessoasRepository.DeletarPessoa(deletarPessoa);

                        if (deletarResponse.HasErrors)
                        {
                            mensagem = "Falha ao cadastrar pessoa, desculpe-nos! Tente mais tarde.";
                            response = new Message(HttpStatusCode.InternalServerError, mensagem);
                        }
                        else
                        {
                            bool deleted = Convert.ChangeType(deletarResponse.Result, typeof(bool));
                            response = new Message(HttpStatusCode.OK, "", deleted);
                        }
                    }
                    else
                    {
                        mensagem = "Houverão erros de validação!";
                        response = new Message(HttpStatusCode.BadRequest, mensagem, deletar._messages);
                    }
                }

                responseService = _responseLayers.AddMessage(response.Executed, response);
            }
            catch (Exception ex)
            {
                mensagem = "Houve uma falha na solicitação!";
                response = new Message(HttpStatusCode.InternalServerError, mensagem);
                responseService = _responseLayers.AddMessage(response.Executed, response);
            }

            return responseService;
        }

        public async Task<IResponseLayer> AtualizarDadosPessoa(AtualizarPessoa atualizar)
        {
            Message response = null;
            IResponseLayer responseService = null;
            string mensagem = string.Empty;

            try
            {
                var atualizarResponse = await _pessoasRepository.AtualizarPessoa(atualizar);

                if (atualizarResponse.HasErrors && atualizarResponse.Result > 0)
                {
                    mensagem = "Falha ao atualizar pessoa, desculpe! Tente mais tarde.";
                    response = new Message(HttpStatusCode.InternalServerError, mensagem);
                }
                else
                {
                    bool updated = Convert.ChangeType(atualizarResponse.Result, typeof(bool));
                    response = new Message(HttpStatusCode.OK, "", updated);
                }

                responseService = _responseLayers.AddMessage(response.Executed, response);
            }
            catch (Exception ex)
            {
                mensagem = "Houve uma falha na solicitação!";
                response = new Message(HttpStatusCode.InternalServerError, mensagem);
                responseService = _responseLayers.AddMessage(response.Executed, response);
            }

            return responseService;
        }
    }
}
