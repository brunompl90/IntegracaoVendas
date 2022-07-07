using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.InformacoesPedidos;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;

namespace IntegracaoVendas.Data.Repositorys
{
    public class InformacoesPedidosRepository : BaseRepository<INFORMACOES_PEDIDO>, IInformacoesPedidosRepository
    {
        public InformacoesPedidosRepository(IUnitOfWork unitOfWork, IntegracaoDbContext context) : base(unitOfWork, context)
        {
        }

        public bool OrderExists(string orderNumber)
        {
            var order = GetFirstOrDefault(o => o.PEDIDO == orderNumber);
            var hasOrder = order != null;
            return hasOrder;
        }
    }
}
