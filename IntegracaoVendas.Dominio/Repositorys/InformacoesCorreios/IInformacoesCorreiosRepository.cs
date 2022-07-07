using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Dominio.Repositorys.Base;

namespace IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios
{
    public interface IInformacoesCorreiosRepository : IBaseRepository<Models.INFORMACOES_CORREIO>
    {
        public bool OrderExists(string objeto);

        public void MarcarComoEnviado(string objeto);
    }
}
