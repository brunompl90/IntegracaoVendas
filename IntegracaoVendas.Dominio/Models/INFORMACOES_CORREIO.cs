using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntegracaoVendas.Dominio.Models
{
    public class INFORMACOES_CORREIO
    {
        [Key]
        public string OBJETO { get; set; }

        public string NOTA { get; set; }
       
        public string STATUS { get; set; }

        public DateTime? DATA { get; set; }

        public bool ENVIADO { get; set; }

    }
}
