using ApiTeste.Core.DTO;
using ApiTeste.Repository;
using ApiTeste.Repository.Interface;
using ApiTeste.Service.Validators.Interfaces;
using ApiTeste.Service.Validators.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Service.Validators
{
    public class DeletarPessoaValidator : MessageBase, IDeletarPessoaValidator<DeletarPessoa>, IDisposable
    {
        public readonly IServiceCollection services;
        public DeletarPessoa _objeto { get; set; }
        public bool IsValid
        {
            get
            {
                return !_verifications.Any(item => item.Equals(false));
            }
        }

        public DeletarPessoaValidator(DeletarPessoa deletar)
        {
            _objeto = deletar;
            ExecuteValidate();
        }

        public void ExecuteValidate()
        {
            services.AddTransient<IPessoasRepository, PessoasRepository>();
            var sp = services.BuildServiceProvider();
            var pessoasRepository = sp.GetService<IPessoasRepository>();

            _messages = new List<string>();
            _verifications = new List<bool>();

            var response = pessoasRepository.VerificarPessoaJaDeletada(_objeto.CodigoPessoa).Result;

            if (!response.HasErrors)
            {
                bool deletado = Convert.ChangeType(response.Result, typeof(bool));

                if (deletado)
                    AddMessage("Solicitação de exclusão já foi realizada!", true);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
