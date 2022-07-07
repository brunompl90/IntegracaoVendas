using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Dominio.Models.PeditosTxt;
using IntegracaoVendas.Dominio.Repositorys.InfosPedidosTxt;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;

namespace IntegracaoVendas.Data.Repositorys
{
    public class PedidoEnviadoRepository : BaseRepository<PEDIDOS_ENVIADOS>, IPedidoEnviadoRepository
    {
        public PedidoEnviadoRepository(IUnitOfWork unitOfWork, IntegracaoDbContext context) : base(unitOfWork, context)
        {
        }

        public void SalvarPedidoEnviado(string pedido)
        {
            
            var pedidoEnviado = new PEDIDOS_ENVIADOS
            {
                DT_GERACAO = DateTime.Now,
                Pedido = pedido
            };

            Insert(pedidoEnviado);
        }
    }
}
