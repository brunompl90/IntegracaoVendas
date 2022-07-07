using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntegracaoVendas.Dominio.Models.PeditosTxt
{
    public class PEDIDOS_ENVIADOS
    {
        [Key]
        public string Pedido { get; set; }
        public DateTime DT_GERACAO { get; set; }
    }
}
