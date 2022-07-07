using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Dominio.Models.PeditosTxt;
using IntegracaoVendas.Dominio.Repositorys.Base;

namespace IntegracaoVendas.Dominio.Repositorys.InfosPedidosTxt
{
    public interface IPedidoEnviadoRepository : IBaseRepository<PEDIDOS_ENVIADOS>
    {
        void SalvarPedidoEnviado(string pedido);
    }
}
