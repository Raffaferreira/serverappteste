using ApiTeste.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Repository.Base
{
    public class RepositoryBase
    {
        protected const string _msgConnectionInit = "Inicializando conexão com a base de dados.";
        protected const string _msgConnectionOpen = "Abrindo a conexão com a base de dados.";
        protected const string _msgConnectionClose = "Fechando a conexão com a base de dados.";

        protected readonly ILogger _logger;
        protected readonly ApplicationSettings _applicationSettings;
        protected DatabaseSettings _databaseSettings;

        protected RepositoryBase(
            ILogger logger
            , IOptions<ApplicationSettings> applicationSettings)
        {
            _logger = logger;
            _applicationSettings = applicationSettings.Value;
            _databaseSettings = _applicationSettings.DatabaseSettings;
            logger.LogInformation(_databaseSettings.DefaultConnection);
        }

        protected DbConnection GetDefaultConnection()
        {
            var conn = new SqlConnection(_databaseSettings.DefaultConnection);
            return conn;
        }
    }
}
