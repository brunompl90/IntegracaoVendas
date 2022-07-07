using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Models.OrdersXml;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Repositorys.VendasCanceladas;
using IntegracaoVendas.Dominio.Utils.FullXmlWriter;
using Microsoft.Extensions.Configuration;
using Order = IntegracaoVendas.Dominio.Models.OrdersXml.Order;
using Shipment = IntegracaoVendas.Dominio.Models.OrdersXml.Shipment;

namespace IntegracaoVendas.Dominio.Services
{
    public class XmlEstoqueIntegration : IXmlEstoqueIntegration
    {
        private readonly IConfiguration _configuration;
        private readonly IXmlRepository _xmlRepository;
        private readonly IInformacoesCorreiosRepository _informacoesCorreiosRepository;
        private readonly IBaseDapperRepository _baseDapperRepository;
        private readonly IVendasCanceladasRepository _vendasCanceladasRepository;
        private readonly IUnitOfWork _uow;
        public XmlEstoqueIntegration(IXmlRepository xmlRepository,
                                IBaseDapperRepository baseDapperRepository,
                                IVendasCanceladasRepository vendasCanceladasRepository,
                                IInformacoesCorreiosRepository informacoesCorreiosRepository,
                                IUnitOfWork uow,
                                IConfiguration configuration)
        {
            _xmlRepository = xmlRepository;
            _configuration = configuration;
            _baseDapperRepository = baseDapperRepository;
            _informacoesCorreiosRepository = informacoesCorreiosRepository;
            _vendasCanceladasRepository = vendasCanceladasRepository;
            _uow = uow;
        }
        public void InitIntegration(string tipoXml)
        {

            if (tipoXml == "Order")
            {
                var caminho = _configuration.GetSection("CaminhoXmlOrder").Value;
                ExtractXmlOrders(caminho, "lcbr_shipping");
                ExtractXmlOrdersCancel(caminho, "lcbr_shipping_cancel");
            }

            if (tipoXml == "Estoque")
            {
                var procedure = _configuration.GetSection("ProcegureXmlEstoque").Value;
                var caminho = _configuration.GetSection("CaminhoXmlEstoque").Value;
                ExtractXml(procedure, caminho, "lcbr_stock");
            }

            if (tipoXml == "Preço")
            {
                var procedure = _configuration.GetSection("ProcegureXmlPreco").Value;
                var caminho = _configuration.GetSection("CaminhoXmlPreco").Value;
                ExtractXml(procedure, caminho, "lcbr_price");
            }

            if (tipoXml == "PreçoLiquido")
            {
                var procedure = _configuration.GetSection("ProcegureXmlPrecoLiquido").Value;
                var caminho = _configuration.GetSection("CaminhoXmlPrecoLiquido").Value;
                ExtractXml(procedure, caminho, "lcbr_price_sale");
            }
        }


        private void ExtractXmlOrders(string caminho, string namePrefix)
        {
            DeleteFiles(caminho);
            var query = @"SELECT 
                         NOTA, --CHAVE PRIMARIA
                         B.NF_SAIDA, --CHAVE PRIMARIA
                         C.PEDIDO, --CHAVE PRIMARIA
                         E.SHIPMENTID, 
                         VARIANTID, 
                         QUANTITY, 
                         E.ORDERNUMBER, 
                         F.LineTaxAmount ,
						 A.OBJETO
                        FROM [INFORMACOES_CORREIOS] A JOIN FATURAMENTO B 
                        ON A.NOTA = SUBSTRING(B.NF_SAIDA, PATINDEX('%[^0]%',B.NF_SAIDA), 10)
                        JOIN FATURAMENTO_PROD C 
                        ON B.NF_SAIDA = C.NF_SAIDA
                        AND B.FILIAL = C.FILIAL
                        AND B.SERIE_NF = C.SERIE_NF
                        JOIN VENDAS D 
                        ON D.PEDIDO = C.PEDIDO
                        JOIN SHIPMENTS E 
                        ON E.ORDERNUMBER = D.PEDIDO_CLIENTE
                        JOIN LINEITENS F 
                        ON F.ORDERNUMBER = E.ORDERNUMBER
                        WHERE B.FILIAL = 'E-COMMERCE NOVA'
                        and A.ENVIADO = 0";

            var result = _baseDapperRepository.Get<OrdersDTO>(query, null);

            if(result == null || result.Count() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Sem informações para gerar xml de orders");
                return;
            }

            var orders = result.ToLookup(g => g.ORDERNUMBER);
            var xmlOrders = new Orders();
            xmlOrders.Lcheader = new Lcheader
            {
                Source = "LINK",
                Channel = "WEB",
                Subsidiary = "BR",
                Timestamp = $"{DateTime.Now:yyyy-MM-ddThh:mm:ssZ}"
            };

            xmlOrders.Order = new List<Order>();

            foreach (var order in orders)
            {
                var orderTag = new Order();
                orderTag.Orderno = order.Key;

                orderTag.Status = new Status
                {
                    Orderstatus = "COMPLETED",
                    Shippingstatus = "SHIPPED",
                    Confirmationstatus = "CONFIRMED",
                    Paymentstatus = "PAID"
                };

               
                orderTag.Productlineitems = new Productlineitems();
                orderTag.Productlineitems.Productlineitem = new List<Productlineitem>();
                foreach (var orderProductLines in order.GroupBy(x => new { x.ORDERNUMBER, x.VARIANTID }))
                {
                    orderTag.Productlineitems.Productlineitem.Add(new Productlineitem
                    {
                        Productid = orderProductLines.FirstOrDefault()?.VARIANTID,
                        Quantity = new Quantity
                        {
                            Text = orderProductLines.FirstOrDefault()?.QUANTITY ?? 0,
                            Unit = "1"
                        },
                        Taxrate = orderProductLines.FirstOrDefault()?.LineTaxAmount ?? 0.00M,
                        Shipmentid = orderProductLines.FirstOrDefault()?.SHIPMENTID
                    });
                }

                orderTag.Shipments = new Shipments();
                orderTag.Shipments.Shipment = new Shipment
                {
                    Status = new ShippingStatus
                    {
                        Shippingstatus = "SHIPPED"
                    },
                    Shippingmethod = "CORREIOS",
                    Trackingnumber = order.FirstOrDefault().OBJETO,
                    Shipmentid = order.FirstOrDefault().SHIPMENTID,
                    Customattributes = new Customattributes
                    {
                        Customattribute = new Customattribute
                        {
                            Attributeid = "carrier",
                            Text = "CORREIOS"
                        }
                    }

                };

                _informacoesCorreiosRepository.MarcarComoEnviado(order.FirstOrDefault().OBJETO);

                xmlOrders.Order.Add(orderTag);

            }

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Orders));
                    serializer.Serialize(writer, xmlOrders);
                    var xml = sww.ToString();

                    var xmlDoc = new XmlDocument();

                    xmlDoc.LoadXml(xml);

                    var declarantion = xmlDoc.FirstChild as XmlDeclaration;
                    declarantion.Encoding = "UTF-8";
                    

                    XmlAttribute attr = xmlDoc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
                    attr.Value = "http://www.lecreuset.com/xml/order lc_order.xsd";
                    var childOrders =  xmlDoc.ChildNodes.Item(1);
                    childOrders.Attributes.Append(attr);
                    childOrders.Attributes.RemoveNamedItem("xmlns:xsd");




                    xmlDoc.Save($"{caminho}/{namePrefix}_{DateTime.Now:yyyyMMdd-HHmm}.xml");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} Xml {namePrefix}_{DateTime.Now:yyyyMMdd-HHmm}.xml gerado com sucesso");

                    _uow.Commit();

                }
            }


        }

        private void ExtractXmlOrdersCancel(string caminho, string namePrefix)
        {
            
            var query = @"SELECT 
                          --CHAVE PRIMARIA
                          --CHAVE PRIMARIA
                         K.ORDERNUMBER, --CHAVE PRIMARIA
                         E.SHIPMENTID, 
                         VARIANTID, 
                         QTDE_CANCELADA as QUANTITY,
						 SHIPPINGMETHODID,
                         E.ORDERNUMBER 
                        FROM VENDAS D
						JOIN VENDAS_PRODUTO C 
						ON C.PEDIDO = D.PEDIDO
                        JOIN SHIPMENTS E 
						ON E.ORDERNUMBER = D.PEDIDO_CLIENTE
						JOIN PRODUTOS_BARRA F
						ON F.PRODUTO = C.PRODUTO 
						AND F.COR_PRODUTO = C.COR_PRODUTO
						JOIN PRODUTOS_REF_FORNECEDOR G
						ON G.CODIGO_BARRA = F.CODIGO_BARRA
                        JOIN LINEITENS K 
                        ON K.ORDERNUMBER = E.ORDERNUMBER
						AND K.VARIANTID = G.CODIGO_ITEM_FORNECEDOR
                        WHERE FILIAL = 'E-COMMERCE NOVA'
						AND QTDE_CANCELADA <> 0
						and not exists( select 1 from  VENDAS_CANCELADAS vc where vc.ORDERNUMBER = K.ORDERNUMBER and vc.CANCELADO = 1)";

            var result = _baseDapperRepository.Get<OrdersDTO>(query, null);

            if (result == null || result.Count() == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Sem informações para gerar xml de orders cancelados");
                return;
            }

            var orders = result.ToLookup(g => g.ORDERNUMBER);
            var xmlOrders = new Orders();
            xmlOrders.Lcheader = new Lcheader
            {
                Source = "LINK",
                Channel = "WEB",
                Subsidiary = "BR",
                Timestamp = $"{DateTime.Now:yyyy-MM-ddThh:mm:ssZ}"
            };

            xmlOrders.Order = new List<Order>();

            foreach (var order in orders)
            {
                var orderTag = new Order();
                orderTag.Orderno = order.Key;

                orderTag.Status = new Status
                {
                    Orderstatus = "CANCELLED",
                    Shippingstatus = "NOT_SHIPPED",
                    Confirmationstatus = "NOT_CONFIRMED",
                    Paymentstatus = "NOT_PAID"
                };


                orderTag.Productlineitems = new Productlineitems();
                orderTag.Productlineitems.Productlineitem = new List<Productlineitem>();
                foreach (var orderProductLines in order.GroupBy(x => new { x.ORDERNUMBER, x.VARIANTID }))
                {
                    orderTag.Productlineitems.Productlineitem.Add(new Productlineitem
                    {
                        Productid = orderProductLines.FirstOrDefault()?.VARIANTID,
                        Quantity = new Quantity
                        {
                            Text = orderProductLines.FirstOrDefault()?.QUANTITY ?? 0,
                            Unit = "1"
                        },
                        Taxrate = 0.00M,
                        Shipmentid = orderProductLines.FirstOrDefault()?.SHIPMENTID
                    });
                }

                orderTag.Shipments = new Shipments();
                orderTag.Shipments.Shipment = new Shipment
                {
                    Status = new ShippingStatus
                    {
                        Shippingstatus = "NOT_SHIPPED"
                    },
                    Shippingmethod = string.Empty,
                    Trackingnumber = string.Empty,
                    Shipmentid = order.FirstOrDefault().SHIPMENTID,
                    Customattributes = new Customattributes
                    {
                        Customattribute = new Customattribute
                        {
                            Attributeid = "carrier",
                            Text = "CORREIOS"
                        }
                    }

                };

                var vendaCancelada = new VENDA_CANCELADA { CANCELADO = true, ORDERNUMBER = order.Select(o => o.ORDERNUMBER).FirstOrDefault() };
                _vendasCanceladasRepository.Insert(vendaCancelada);

                xmlOrders.Order.Add(orderTag);

            }

            using (var sww = new StringWriter())
            {
                using (FullElementXmlTextWriter writer = new FullElementXmlTextWriter(sww))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Orders));
                    serializer.Serialize(writer, xmlOrders);
                    //a tag nao estava sendo criada vazia na mesma linha
                    //utilizado para deixa as tag com esse valor vazias e abertas e fechadas na mesma linha
                    var xml = sww.ToString();
                    
                    var xmlDoc = new XmlDocument();


                    xmlDoc.LoadXml(xml);

                    var declarantion = xmlDoc.FirstChild as XmlDeclaration;
                    declarantion.Encoding = "UTF-8";

                    XmlAttribute attr = xmlDoc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
                    attr.Value = "http://www.lecreuset.com/xml/order lc_order.xsd";
                    var childOrders = xmlDoc.ChildNodes.Item(1);
                    childOrders.Attributes.Append(attr);
                    childOrders.Attributes.RemoveNamedItem("xmlns:xsd");



                    xmlDoc.Save($"{caminho}/{namePrefix}_{DateTime.Now:yyyyMMdd-HHmm}.xml");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} Xml {namePrefix}_{DateTime.Now:yyyyMMdd-HHmm}.xml gerado com sucesso");
                    _uow.Commit();

                }
            }


        }


        private void ExtractXml(string procedure, string caminho, string namePrefix)
        {
            var xmlDeclaration = "<?xml version='1.0' encoding='UTF-8'?>";
            var xmlRetornado = xmlDeclaration;
            xmlRetornado += _xmlRepository.ExecuteProcedure($"EXEC {procedure}");
            DeleteFiles(caminho);

            if (!string.IsNullOrEmpty(xmlRetornado))
            {

                var xmlDoc = new XmlDocument();

                xmlDoc.LoadXml(xmlRetornado);
                xmlDoc.Save($"{caminho}/{namePrefix}_{DateTime.Now:yyyyMMdd-HHmm}.xml");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} Xml {namePrefix}_{DateTime.Now:yyyyMMdd-HHmm}.xml gerado com sucesso");
                var di = new DirectoryInfo(caminho);
            }
        }

        private void DeleteFiles(string caminho)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} Limpando arquivos do diretorio {caminho}");
            var di = new DirectoryInfo(caminho);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }


    }
}
