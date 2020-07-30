using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTeste.Core.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/vendas/")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("Bearer")]
        [HttpPost("geraringresso")]
        public async Task<IActionResult> GerarIngreso([FromBody] Ingresso request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }

        [Authorize("Bearer")]
        [HttpGet("ingressos")]
        public async Task<IActionResult> GetIngressos([FromQuery] ListaIngressos request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }

        [Authorize("Bearer", Roles = "usr_sys, usr_normal, usr_admin")]
        [HttpPost("validatar/ingresso")]
        public async Task<IActionResult> ValidarIngressoUsuario([FromBody] Ingresso request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }

        [Authorize("Bearer", Roles = "usr_sys, usr_normal, usr_admin")]
        [HttpPut("atualizar/ingresso")]
        public async Task<IActionResult> AtualizarInformacoesDoIngresso([FromBody] Ingresso request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }

        [Authorize("Bearer", Roles = "usr_sys, usr_normal, usr_admin")]
        [HttpGet("locais")]
        public async Task<IActionResult> PostosDeVendasFisico([FromBody] Ingresso request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.CustomErrors);
            }

            return Ok(response.Result);
        }
    }
}
