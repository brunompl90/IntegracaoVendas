using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;

namespace IntegracaoVendas.Data.Repositorys
{
    public class XmlRepository : BaseRepository<Order>, IXmlRepository
    {
        public XmlRepository(IUnitOfWork unitOfWork, IntegracaoDbContext context) : base(unitOfWork, context)
        {
        }

        public bool OrderExists(string orderNumber)
        {
            var order = GetFirstOrDefault(o => o.OrderNumber == orderNumber);
            var hasOrder = order != null;
            return hasOrder;
        }
    }
}
