using ApiTeste.Core.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Commands.Vendas
{
    public class PessoaCommand : AbstractValidator<Pessoa>
    {
        public PessoaCommand()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome da pessoa não pode ser nulo");
        }
    }
}
