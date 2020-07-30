using ApiTeste.Core.DTO;
using ApiTeste.Service.Validators.Interfaces;
using ApiTeste.Service.Validators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Service.Validators
{
    public class IngressoValidator : MessageBase, IIngressoValidator<Ingresso>, IDisposable
    {
        public Ingresso _objeto { get; set; }
        public bool IsValid
        {
            get
            {
                return !_verifications.Any(item => item.Equals(false));
            }
        }

        public IngressoValidator(Ingresso ingresso)
        {
            _objeto = ingresso;
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
