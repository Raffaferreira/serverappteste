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
    public class PessoasRepository : RepositoryBase, IPessoasRepository
    {
        public readonly ILogger<PessoasRepository> _logger;
        public readonly IResponseLayer _response;

        public PessoasRepository(IOptions<ApplicationSettings> applicationSettings,
            ILogger<PessoasRepository> logger,
            IResponseLayer typedResponse) : base(logger, applicationSettings)
        {
            _logger = logger;
            _response = typedResponse;
        }

        public async Task<IResponseLayer> AtualizarPessoa(AtualizarPessoa atualizar)
        {
            int result = 0;

            _logger.LogDebug(_msgConnectionInit);

            try
            {
                using (var conn = GetDefaultConnection())
                {
                    _logger.LogDebug(_msgConnectionOpen);

                    var sql = @"UPDATE TB_PESSOA SET NOME = @NOME,
                                                 DATA_NASCIMENTO = @DATA,
                                                 IDADE = @IDADE,
                                                 EMAIL = @EMAIL,
                                                 SEXO = @SEXO,
                                                 HABILIDADES = @HABILIDADES,
                                                 ATIVO = @STATUS
                                                 WHERE CODIGO_PESSOA = @CODIGOPESSOA";

                    var parametros = new
                    {
                        NOME = atualizar.Nome,
                        DATA = atualizar.Data.Value,
                        IDADE = atualizar.Idade,
                        EMAIL = atualizar.Email,
                        SEXO = atualizar.Sexo == "Masculino" ? 1 : 2,
                        HABILIDADES = atualizar.Habilidades,
                        STATUS = atualizar.Ativo ? 1 : 0,
                        CODIGOPESSOA = atualizar.Codigo
                    };

                    result = await conn.ExecuteAsync(sql, parametros, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                return _response.AddMessage(false, ex.Message);
            }

            return _response.AddMessage(true, Convert.ToBoolean(result));
        }

        public async Task<IResponseLayer> CadastrarPessoa(Pessoa pessoa)
        {
            int result = 0;

            _logger.LogDebug(_msgConnectionInit);

            try
            {
                using (var conn = GetDefaultConnection())
                {
                    _logger.LogDebug(_msgConnectionOpen);

                    var sql = @"INSERT INTO TB_PESSOA 
                                (NOME, 
                                DATA_NASCIMENTO, 
                                IDADE, 
                                EMAIL, 
                                SEXO, 
                                HABILIDADES, 
                                ATIVO)
                                OUTPUT INSERTED.[CODIGO_PESSOA]
                                VALUES 
                                (@NOME, 
                                @DATA, 
                                @IDADE, 
                                @EMAIL, 
                                @SEXO,
                                @HABILIDADES, 
                                @ATIVO)";

                    var parametros = new
                    {
                        NOME = pessoa.Nome,
                        DATA = pessoa.Data.Value,
                        IDADE = pessoa.Idade,
                        EMAIL = pessoa.Email,
                        SEXO = pessoa.Sexo == "Masculino" ? 1 : 2,
                        HABILIDADES = pessoa.Habilidades,
                        ATIVO = pessoa.Ativo ? 1 : 0
                    };

                    result = await conn.QuerySingleAsync<int>(sql, parametros, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                return _response.AddMessage(false, ex.Message);
            }

            return _response.AddMessage(true, result);
        }

        public async Task<IResponseLayer> DeletarPessoa(DeletarPessoa deletar)
        {
            int result = 0;

            _logger.LogDebug(_msgConnectionInit);

            try
            {
                using (var conn = GetDefaultConnection())
                {
                    _logger.LogDebug(_msgConnectionOpen);

                    var sql = @"DELETE FROM TB_PESSOA WHERE CODIGO_PESSOA = @CODIGOPESSOA";

                    var parametros = new
                    {
                        CODIGOPESSOA = deletar.CodigoPessoa
                    };

                    result = await conn.ExecuteAsync(sql, parametros, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                return _response.AddMessage(false, ex.Message);
            }

            return _response.AddMessage(true, Convert.ToBoolean(result));
        }

        public async Task<IResponseLayer> GetListaPessoas()
        {
            IEnumerable<PessoaModel> result = null;

            _logger.LogDebug(_msgConnectionInit);

            try
            {
                using (var conn = GetDefaultConnection())
                {
                    _logger.LogDebug(_msgConnectionOpen);

                    var sql = @"SELECT PESSOA.CODIGO_PESSOA CODIGO,
                                       PESSOA.NOME, 
                                       PESSOA.DATA_NASCIMENTO AS DATA, 
                                       PESSOA.IDADE,
                                       PESSOA.EMAIL, 
                                       GENERO.DESCRICAO AS SEXO, 
                                       PESSOA.HABILIDADES,
                                       PESSOA.ATIVO 
                                       FROM TB_PESSOA PESSOA
									   INNER JOIN TB_GENERO_PESSOA GENERO ON GENERO.CODIGO_GENERO = PESSOA.SEXO";

                    result = await conn.QueryAsync<PessoaModel>(sql);
                }

                _logger.LogDebug(_msgConnectionClose);
            }
            catch (Exception ex)
            {
                return _response.AddMessage(false, ex.Message);
            }

            return _response.AddMessage(true, result);
        }

        public async Task<IResponseLayer> GetPessoaById(int codigoPessoa)
        {
            PessoaModel result = null;

            _logger.LogDebug(_msgConnectionInit);

            try
            {
                using (var conn = GetDefaultConnection())
                {
                    _logger.LogDebug(_msgConnectionOpen);

                    var sql = @"SELECT PESSOA.CODIGO_PESSOA CODIGO,
                                       PESSOA.NOME, 
                                       PESSOA.DATA_NASCIMENTO AS DATA, 
                                       PESSOA.IDADE,
                                       PESSOA.EMAIL, 
                                       GENERO.DESCRICAO AS SEXO, 
                                       PESSOA.HABILIDADES,
                                       PESSOA.ATIVO 
                                       FROM TB_PESSOA PESSOA
									   INNER JOIN TB_GENERO_PESSOA GENERO ON GENERO.CODIGO_GENERO = PESSOA.SEXO
									   WHERE PESSOA.CODIGO_PESSOA = @CODIGOPESSOA";

                    object param = new
                    {
                        CODIGOPESSOA = codigoPessoa
                    };

                    result = await conn.QuerySingleAsync<PessoaModel>(sql, param);
                }

                _logger.LogDebug(_msgConnectionClose);
            }
            catch (Exception ex)
            {
                return _response.AddMessage(false, ex.Message);
            }

            return _response.AddMessage(true, result);
        }

        public async Task<IResponseLayer> VerificarPessoaJaDeletada(int codigoPessoa)
        {
            bool result = false;

            _logger.LogDebug(_msgConnectionInit);

            try
            {
                using (var conn = GetDefaultConnection())
                {
                    _logger.LogDebug(_msgConnectionOpen);

                    var sql = @"SELECT COUNT(*) WHERE PESSOA.CODIGO_PESSOA = @CODIGOPESSOA";

                    object param = new
                    {
                        CODIGOPESSOA = codigoPessoa
                    };

                    result = await conn.ExecuteScalarAsync<bool>(sql, param);
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
