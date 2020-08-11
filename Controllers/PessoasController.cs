using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTeste.Core.DTO;
using ApiTeste.Repository.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/pessoas/")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PessoasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("cadastrar")]
        [ProducesResponseType(typeof(PessoaModel), 200)]
        public async Task<IActionResult> CadastrarPessoa([FromBody] Pessoa request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }

        [HttpDelete("deletar/{codigoPessoa:int}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeletarPessoa(int codigoPessoa)
        {
            DeletarPessoa request = new DeletarPessoa();
            request.CodigoPessoa = codigoPessoa;

            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }

        [HttpPut("editar")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> EditarDadosPessoa([FromBody] AtualizarPessoa request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }

        [HttpGet("getpessoas")]
        [ProducesResponseType(typeof(List<PessoaModel>), 200)]
        public async Task<IActionResult> ListaDePessoas()
        {
            GetPessoas request = new GetPessoas();

            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }
    }
}
