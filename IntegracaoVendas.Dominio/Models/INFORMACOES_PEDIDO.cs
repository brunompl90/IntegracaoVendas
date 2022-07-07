using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntegracaoVendas.Dominio.Models
{
    public class INFORMACOES_PEDIDO
    {
        [Key]
        public string PEDIDO { get; set; }
       
        public int VOLUME { get; set; }

        public decimal PESO { get; set; }
    }
}
