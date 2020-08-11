using ApiTeste.Infrastructure.Model;
using MediatR;
using System;

namespace ApiTeste.Core.DTO
{
    public class Pessoa : IRequest<Response>
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string Habilidades { get; set; }
        public bool Ativo { get; set; }
    }
}
