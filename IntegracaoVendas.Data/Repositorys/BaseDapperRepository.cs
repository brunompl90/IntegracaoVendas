using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using IntegracaoVendas.Dominio.Repositorys.Base;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Data.Repositorys
{
    public class BaseDapperRepository : IBaseDapperRepository
    {
        private readonly IConfiguration _configuration;
        public BaseDapperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<T> Get<T>(string query, Dictionary<string, object> parameters)
        {
            using (SqlConnection conexao = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                return conexao.Query<T>(
                    query, parameters);
            }
        }
    }
}
