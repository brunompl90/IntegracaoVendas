using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoVendas.Dominio.Repositorys.Base
{
    public interface IBaseDapperRepository
    {
        IEnumerable<T> Get<T>(string query, Dictionary<string, object> parameters);
    }
}
