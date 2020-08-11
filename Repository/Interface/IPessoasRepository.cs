using ApiTeste.Core.DTO;
using ApiTeste.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository.Interface
{
    public interface IPessoasRepository
    {
        Task<IResponseLayer> CadastrarPessoa(Pessoa pessoa);
        Task<IResponseLayer> VerificarPessoaJaDeletada(int codigoPessoa);
        Task<IResponseLayer> GetListaPessoas();
        Task<IResponseLayer> GetPessoaById(int codigoPessoa);
        Task<IResponseLayer> DeletarPessoa(DeletarPessoa deletar);
        Task<IResponseLayer> AtualizarPessoa(AtualizarPessoa atualizar);
    }
}
