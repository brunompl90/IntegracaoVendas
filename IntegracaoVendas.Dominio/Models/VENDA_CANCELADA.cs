using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntegracaoVendas.Dominio.Models
{
    public class VENDA_CANCELADA
    {
        [Key]
        public string ORDERNUMBER { get; set; }

        public bool CANCELADO { get; set; }

    }
}
