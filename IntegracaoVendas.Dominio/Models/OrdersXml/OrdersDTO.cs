using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoVendas.Dominio.Models.OrdersXml
{
    public class OrdersDTO
    {
        public string NOTA { get; set; }
        public string NF_SAIDA { get; set; }
        public string SHIPMENTID { get; set; }
        public string VARIANTID { get; set; }
        public int QUANTITY { get; set; }
        public string ORDERNUMBER { get; set; }
        public decimal LineTaxAmount { get; set; }
        public string OBJETO { get; set; }

    }
}
