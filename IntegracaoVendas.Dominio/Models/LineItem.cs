using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace IntegracaoVendas.Dominio.Models
{
    [XmlRoot(ElementName = "LineItem")]
    public class LineItem
    {
        [Key]
        public int LineItemId { get; set; }
        public string OrderNumber { get; set; }
        [XmlElement(ElementName = "LineNumber")]
        public string LineNumber { get; set; }
        [XmlElement(ElementName = "VariantId")]
        public string VariantId { get; set; }
        [XmlElement(ElementName = "DisplayName")]
        public string DisplayName { get; set; }
        [XmlElement(ElementName = "Quantity")]
        public string Quantity { get; set; }
        [XmlElement(ElementName = "UnitListPrice")]
        public string UnitListPrice { get; set; }
        [XmlElement(ElementName = "UnitCustomerPrice")]
        public string UnitCustomerPrice { get; set; }
        [XmlElement(ElementName = "LineListPrice")]
        public string LineListPrice { get; set; }
        [XmlElement(ElementName = "LineCustomerPrice")]
        public string LineCustomerPrice { get; set; }
        [XmlElement(ElementName = "LineTaxAmount")]
        public string LineTaxAmount { get; set; }
        [XmlElement(ElementName = "TaxPercentage")]
        public string TaxPercentage { get; set; }

        public Order Order { get; set; }
    }
}
