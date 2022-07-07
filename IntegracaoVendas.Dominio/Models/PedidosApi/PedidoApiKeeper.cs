using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoVendas.Dominio.Models.PedidosApi
{
    public class Customer
    {
        public string name { get; set; }
        public string fantasy_name { get; set; }
        public string cnpj_cpf { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string neighbourhood { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string state_registration { get; set; }
        public bool is_foreign { get; set; }
        public int country_code { get; set; }
    }

    public class Transport
    {
        public string name { get; set; }
        public string fantasy_name { get; set; }
        public string cnpj_cpf { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public object complement { get; set; }
        public string neighbourhood { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string state_registration { get; set; }
    }

    public class Item
    {
        public string product { get; set; }
        public string description { get; set; }
        public string unit_of_measurement { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_price { get; set; }
        public string lot { get; set; }
    }

    public class PedidoApiKeeper
    {
        public int order_number { get; set; }
        public string operation_code { get; set; }
        public string observation { get; set; }
        public string expected_date_shipment { get; set; }
        public Customer customer { get; set; }
        public Transport transport { get; set; }
        public List<Item> items { get; set; }
    }
}
