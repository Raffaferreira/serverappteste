using ApiTeste.Core.DTO;
using ApiTeste.Handlers;
using System.Threading.Tasks;

namespace ApiTeste.Service.Interface
{
    public interface IVendasService
    {
        Task<IResponseLayer> VenderIngresso(Ingresso ingresso);
        Task<IResponseLayer> GetListaIngressosPorLote(ListaIngressos getingressos);
    }
}
