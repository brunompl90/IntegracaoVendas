using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Dominio.Models.PeditosTxt;

namespace IntegracaoVendas.Dominio.Repositorys.InfosPedidosTxt
{
   public interface IInfoPedidosTxtAdapter
   {
       List<InfoPedido> GetInfoPedidos();

   }
}
