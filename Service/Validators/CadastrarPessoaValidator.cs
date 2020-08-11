using ApiTeste.Core.DTO;
using ApiTeste.Service.Validators.Interfaces;
using ApiTeste.Service.Validators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Service.Validators
{
    public class CadastrarPessoaValidator : MessageBase, ICadastrarPessoaValidator<Pessoa>, IDisposable
    {
        public bool disposed { get; set; }
        public Pessoa _objeto { get; set; }
        public bool IsValid
        {
            get
            {
                return !_verifications.Any(item => item.Equals(false));
            }
        }

        public CadastrarPessoaValidator(Pessoa ingresso)
        {
            _objeto = ingresso;
            ExecuteValidate();
        }

        public void ExecuteValidate()
        {
            _messages = new List<string>();
            _verifications = new List<bool>();

            if (_objeto.Idade < 18)
            {
                AddMessage("Só é permitido cadastrar para maior de 18 anos", true);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
