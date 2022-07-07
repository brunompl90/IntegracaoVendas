using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios;
using IntegracaoVendas.Dominio.Repositorys.InformacoesPedidos;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.PedidosKeeper;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;

namespace IntegracaoVendas.Data.Repositorys
{
    public class PedidoKeeperRepository : BaseRepository<PedidoKeeper>, IPedidoKeeperRepository
    {
        public PedidoKeeperRepository(IUnitOfWork unitOfWork, IntegracaoDbContext context) : base(unitOfWork, context)
        {
        }
    }
}
