using ApiTeste.Infrastructure.Model;
using MediatR;
using System;

namespace ApiTeste.Core.DTO
{
    public class Ingresso : IRequest<Response>
    {
        public int codigoPessoa { get; set; }
        public DateTime? DataGeracaoIngresso { get; set; }
        public int TipoPessoa { get; set; }
        public string MeioDePagamento { get; set; }
    }
}
