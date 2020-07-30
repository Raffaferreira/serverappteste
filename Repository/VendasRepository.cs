using ApiTeste.Core.DTO;
using ApiTeste.Handlers;
using ApiTeste.Infrastructure;
using ApiTeste.Repository.Base;
using ApiTeste.Repository.Interface;
using ApiTeste.Repository.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository
{
    public class VendasRepository : RepositoryBase, IVendasRepository
    {
        public readonly ILogger<VendasRepository> _logger;
        public readonly IResponseLayer _response;

        public VendasRepository(IOptions<ApplicationSettings> applicationSettings,
            ILogger<VendasRepository> logger,
            IResponseLayer typedResponse) : base(logger, applicationSettings)
        {
            _logger = logger;
            _response = typedResponse;
        }

        public async Task<IResponseLayer> GerarIngreso(Ingresso ingresso)
        {
            IEnumerable<Ingresso> result = null;

            _logger.LogDebug(_msgConnectionInit);

            try
            {
                using (var conn = GetDefaultConnection())
                {
                    _logger.LogDebug(_msgConnectionOpen);

                    var sql = @"SP_GERAR_INGRESSO";

                    var parametros = new DynamicParameters();

                    parametros.Add("@CODIGOPESSOA", ingresso.codigoPessoa);
                    parametros.Add("@DATAGERACAOINGRESSO", ingresso.DataGeracaoIngresso);
                    parametros.Add("@TIPOPESSOA", ingresso.TipoPessoa);
                    parametros.Add("@MEIODEPAGAMENTO", ingresso.MeioDePagamento);

                    result = await conn.QueryAsync<Ingresso>(sql, parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                return _response.AddMessage(false, ex.Message);
            }

            return _response.AddMessage(true, result);
        }

        public async Task<IResponseLayer> GetIngressos(ListaIngressos listaIngressos)
        {
           IEnumerable<ListaIngressosModel> result = null;

            _logger.LogDebug(_msgConnectionInit);

            try
            {
                using (var conn = GetDefaultConnection())
                {
                    _logger.LogDebug(_msgConnectionOpen);

                    var sql = @"SELECT CODIGO_INGRESSO AS CODIGOEMAIL,
                                       QUANT_INGRESSOS AS QUANTIDADE,
                                       VALOR_INGRESSO VALOR,
                                       STATUS_INGRESSO STATUS,
                                       ENDERECO_LOCAL LOCAL
                                FROM   TB_INGRESSOS 
                                WHERE  LOTE_DO_INGRESSO = @LOTE";

                    var param = new
                    {
                        LOTE = listaIngressos.Lote
                    };

                    result = await conn.QueryAsync<ListaIngressosModel>(sql, param);
                }

                _logger.LogDebug(_msgConnectionClose);
            }
            catch (Exception ex)
            {
                return _response.AddMessage(false, ex.Message);
            }

            return _response.AddMessage(true, result);
        }
    }
}
