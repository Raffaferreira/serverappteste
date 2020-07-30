using ApiTeste.Core.DTO;
using ApiTeste.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository.Interface
{
    public interface IVendasRepository
    {
        Task<IResponseLayer> GerarIngreso(Ingresso ingresso);
        Task<IResponseLayer> GetIngressos(ListaIngressos listaIngressos);
    }
}
