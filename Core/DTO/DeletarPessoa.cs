using ApiTeste.Infrastructure.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Core.DTO
{
    public class DeletarPessoa : IRequest<Response>
    {
        public int CodigoPessoa { get; set; }
    }
}
