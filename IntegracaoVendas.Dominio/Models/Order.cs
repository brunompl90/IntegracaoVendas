using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace IntegracaoVendas.Dominio.Models
{
	[XmlRoot(ElementName = "Order")]
    public class Order
    {
        [Key]
        [XmlElement(ElementName = "OrderNumber")]
        public string OrderNumber { get; set; }
        [XmlElement(ElementName = "CustomerCode")]
        public string CustomerCode { get; set; }
     
        [XmlElement(ElementName = "LineTotal")]
        public string LineTotal { get; set; }
        [XmlElement(ElementName = "CustomerTotal")]
        public string CustomerTotal { get; set; }
        [XmlElement(ElementName = "CustomerDiscountTotal")]
        public string CustomerDiscountTotal { get; set; }
        [XmlElement(ElementName = "DiscountCode")]
        public string DiscountCode { get; set; }
        [XmlElement(ElementName = "CustomerTaxTotal")]
        public string CustomerTaxTotal { get; set; }
        [XmlElement(ElementName = "VatCode")]
        public string VatCode { get; set; }
        [XmlElement(ElementName = "Culture")]
        public string Culture { get; set; }
        [XmlElement(ElementName = "Currency")]
        public string Currency { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "LineItem")]
        public List<LineItem> LineItem { get; set; }
        [XmlElement(ElementName = "Payment")]
        public Payment Payment { get; set; }
        [XmlElement(ElementName = "BillingAddress")]
        public BillingAddress BillingAddress { get; set; }
        [XmlElement(ElementName = "Shipment")]
        public Shipment Shipment { get; set; }
    }
}
