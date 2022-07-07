using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Dominio.Repositorys.Base;

namespace IntegracaoVendas.Dominio.Repositorys.Order
{
    public interface IXmlRepository : IBaseRepository<Models.Order>
    {
        public bool OrderExists(string orderNumber);
    }
}
