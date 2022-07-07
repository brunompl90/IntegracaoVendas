using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IntegracaoVendas.Dominio.Models.OrdersXml
{


    [XmlRoot(ElementName = "lc-header", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Lcheader
    {
        [XmlElement(ElementName = "source", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Source { get; set; }
        [XmlElement(ElementName = "channel", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Channel { get; set; }
        [XmlElement(ElementName = "subsidiary", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Subsidiary { get; set; }
        [XmlElement(ElementName = "timestamp", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Timestamp { get; set; }


        private XmlSerializerNamespaces _Xmlns;


        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }

    }

    [XmlRoot(ElementName = "status", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Status
    {
        [XmlElement(ElementName = "order-status", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Orderstatus { get; set; }
        [XmlElement(ElementName = "shipping-status", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Shippingstatus { get; set; }
        [XmlElement(ElementName = "confirmation-status", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Confirmationstatus { get; set; }
        [XmlElement(ElementName = "payment-status", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Paymentstatus { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }


    [XmlRoot(ElementName = "status", Namespace = "http://www.lecreuset.com/xml/order")]
    public class ShippingStatus
    {
       
        [XmlElement(ElementName = "shipping-status", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Shippingstatus { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "quantity", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Quantity
    {
        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }
        [XmlText]
        public int Text { get; set; }
        

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "product-lineitem", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Productlineitem
    {
        [XmlElement(ElementName = "product-id", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Productid { get; set; }
        [XmlElement(ElementName = "quantity", Namespace = "http://www.lecreuset.com/xml/order")]
        public Quantity Quantity { get; set; }
        [XmlElement(ElementName = "tax-rate", Namespace = "http://www.lecreuset.com/xml/order")]
        public decimal Taxrate { get; set; }
        [XmlElement(ElementName = "shipment-id", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Shipmentid { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "product-lineitems", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Productlineitems
    {
        [XmlElement(ElementName = "product-lineitem", Namespace = "http://www.lecreuset.com/xml/order")]
        public List<Productlineitem> Productlineitem { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "custom-attribute", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Customattribute
    {
        [XmlAttribute(AttributeName = "attribute-id")]
        public string Attributeid { get; set; }
        [XmlText]
        public string Text { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "custom-attributes", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Customattributes
    {
        [XmlElement(ElementName = "custom-attribute", Namespace = "http://www.lecreuset.com/xml/order")]
        public Customattribute Customattribute { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "shipment", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Shipment
    {
        [XmlElement(ElementName = "status", Namespace = "http://www.lecreuset.com/xml/order")]
        public ShippingStatus Status { get; set; }
        [XmlElement(ElementName = "shipping-method", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Shippingmethod { get; set; }
        [XmlElement(ElementName = "tracking-number", Namespace = "http://www.lecreuset.com/xml/order")]
        public string Trackingnumber { get; set; }
        [XmlElement(ElementName = "custom-attributes", Namespace = "http://www.lecreuset.com/xml/order")]
        public Customattributes Customattributes { get; set; }
        [XmlAttribute(AttributeName = "shipment-id")]
        public string Shipmentid { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "shipments", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Shipments
    {
        [XmlElement(ElementName = "shipment", Namespace = "http://www.lecreuset.com/xml/order")]
        public Shipment Shipment { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "order", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Order
    {
        [XmlElement(ElementName = "status", Namespace = "http://www.lecreuset.com/xml/order")]
        public Status Status { get; set; }
        [XmlElement(ElementName = "product-lineitems", Namespace = "http://www.lecreuset.com/xml/order")]
        public Productlineitems Productlineitems { get; set; }
        [XmlElement(ElementName = "shipments", Namespace = "http://www.lecreuset.com/xml/order")]
        public Shipments Shipments { get; set; }
        [XmlAttribute(AttributeName = "order-no")]
        public string Orderno { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }
    }

    [XmlRoot(ElementName = "orders", Namespace = "http://www.lecreuset.com/xml/order")]
    public class Orders
    {
        [XmlElement(ElementName = "lc-header", Namespace = "http://www.lecreuset.com/xml/order")]
        public Lcheader Lcheader { get; set; }
        [XmlElement(ElementName = "order", Namespace = "http://www.lecreuset.com/xml/order")]
        public List<Order> Order { get; set; }
        [XmlAttribute(AttributeName = "n2", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string N2 { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }

        private XmlSerializerNamespaces _Xmlns;
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (_Xmlns == null)
                {
                    _Xmlns = new XmlSerializerNamespaces();
                    _Xmlns.Add("n2", "http://www.lecreuset.com/xml/order");
                }

                return _Xmlns;
            }

            set
            {
                _Xmlns = value;
            }
        }

         //[XmlAttribute("schemaLocation", Namespace = "http://www.lecreuset.com/xml/order")]
         //public string xsiSchemaLocation = "http://www.lecreuset.com/xml/order lc_order.xsd";
    }

}


