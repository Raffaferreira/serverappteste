using ApiTeste.Core.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Commands.Vendas
{
    public class IngressoCommand : AbstractValidator<Ingresso>
    {
        public IngressoCommand()
        {
            RuleFor(c => c.codigoPessoa)
                .NotEmpty()
                .WithMessage("Código da pessoa, não pode ser nulo!");

            RuleFor(c => c.DataGeracaoIngresso)
                .NotEmpty()
                .WithMessage("Código da pessoa, não pode ser nulo!");

            RuleFor(c => c.codigoPessoa)
                .NotEmpty()
                .WithMessage("Código da pessoa, não pode ser nulo!");

            RuleFor(c => c.codigoPessoa)
                .NotEmpty()
                .WithMessage("Código da pessoa, não pode ser nulo!");

        }
    }
}
