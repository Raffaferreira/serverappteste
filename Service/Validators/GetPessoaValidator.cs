using ApiTeste.Core.DTO;
using ApiTeste.Service.Validators.Interfaces;
using ApiTeste.Service.Validators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Service.Validators
{
    public class GetPessoaValidator : MessageBase, IGetPessoaValidator<Pessoa>, IDisposable
    {
        public Pessoa _objeto { get; set; }
        public bool IsValid
        {
            get
            {
                return !_verifications.Any(item => item.Equals(false));
            }
        }

        public GetPessoaValidator(Pessoa listaIngressos)
        {
            _objeto = listaIngressos;
        }

        public void ExecuteValidate()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
