using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Dominio.Repositorys.Base;

namespace IntegracaoVendas.Dominio.Repositorys.InformacoesPedidos
{
    public interface IInformacoesPedidosRepository : IBaseRepository<Models.INFORMACOES_PEDIDO>
    {
        public bool OrderExists(string orderNumber);
    }
}
