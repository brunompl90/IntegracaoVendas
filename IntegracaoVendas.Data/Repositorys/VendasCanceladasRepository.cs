using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios;
using IntegracaoVendas.Dominio.Repositorys.InformacoesPedidos;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Repositorys.VendasCanceladas;

namespace IntegracaoVendas.Data.Repositorys
{
    public class VendasCanceladasRepository : BaseRepository<VENDA_CANCELADA>, IVendasCanceladasRepository
    {
        public VendasCanceladasRepository(IUnitOfWork unitOfWork, IntegracaoDbContext context) : base(unitOfWork, context)
        {
        }
        
    }
}
