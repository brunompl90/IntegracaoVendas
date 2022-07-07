using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios;
using IntegracaoVendas.Dominio.Repositorys.InformacoesPedidos;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;

namespace IntegracaoVendas.Data.Repositorys
{
    public class InformacoesCorreiosRepository : BaseRepository<INFORMACOES_CORREIO>, IInformacoesCorreiosRepository
    {
        public InformacoesCorreiosRepository(IUnitOfWork unitOfWork, IntegracaoDbContext context) : base(unitOfWork, context)
        {
        }

        public void MarcarComoEnviado(string objeto)
        {
            var order = GetFirstOrDefault(o => o.OBJETO == objeto);
            order.ENVIADO = true;
            Update(order);
        }

        public bool OrderExists(string objeto)
        {
            var order = GetFirstOrDefault(o => o.OBJETO == objeto);
            var hasOrder = order != null;
            return hasOrder;
        }
    }
}
