using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace IntegracaoVendas.Dominio.Models
{
	[XmlRoot(ElementName = "BillingAddress")]
    public class BillingAddress
    {
        [Key]
        public int BillingAddressId { get; set; }
        public string OrderNumber { get; set; }
        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "AddressLine1")]
        public string AddressLine1 { get; set; }
        [XmlElement(ElementName = "AddressLine2")]
        public string AddressLine2 { get; set; }
        [XmlElement(ElementName = "AddressLine3")]
        public string AddressLine3 { get; set; }
        [XmlElement(ElementName = "AddressLine4")]
        public string AddressLine4 { get; set; }
        [XmlElement(ElementName = "City")]
        public string City { get; set; }
        [XmlElement(ElementName = "CountryCode")]
        public string CountryCode { get; set; }
        [XmlElement(ElementName = "CountryName")]
        public string CountryName { get; set; }
        [XmlElement(ElementName = "PostalCode")]
        public string PostalCode { get; set; }
        [XmlElement(ElementName = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "RegionName")]
        public string RegionName { get; set; }

        public Order Order { get; set; }
    }
}
