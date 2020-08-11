using ApiTeste.Core.DTO;
using ApiTeste.Handlers;
using System.Threading.Tasks;

namespace ApiTeste.Service.Interface
{
    public interface IPessoasService
    {
        Task<IResponseLayer> CadastrarPessoa(Pessoa pessoa);
        Task<IResponseLayer> GetListaDePessoas();
        Task<IResponseLayer> DeletarPessoaPorCodigo(DeletarPessoa deletarPessoa);
        Task<IResponseLayer> AtualizarDadosPessoa(AtualizarPessoa atualizar);
    }
}
