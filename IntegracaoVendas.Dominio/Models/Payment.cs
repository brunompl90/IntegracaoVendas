using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace IntegracaoVendas.Dominio.Models
{
	[XmlRoot(ElementName = "Payment")]
    public class Payment
    {
        [XmlElement(ElementName = "PaymentId")]
        [Key]
        public string PaymentId { get; set; }
        public string OrderNumber { get; set; }
        [XmlElement(ElementName = "PaymentType")]
        public string PaymentType { get; set; }
        [XmlElement(ElementName = "PaymentCurrency")]
        public string PaymentCurrency { get; set; }
        [XmlElement(ElementName = "PaymentValue")]
        public string PaymentValue { get; set; }
        [XmlElement(ElementName = "AdyenInstallmentsNo")]
        public string AdyenInstallmentsNo { get; set; }

        public Order Order { get; set; }
    }
}
