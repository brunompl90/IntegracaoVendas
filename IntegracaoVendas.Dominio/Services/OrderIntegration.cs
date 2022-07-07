using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Dominio.Services
{
    public class OrderIntegration : IOrderIntegration
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;
        public OrderIntegration(IOrderRepository orderRepository,
                                IUnitOfWork uow,
                                IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            _uow = uow;
        }
        public void InitIntegration()
        {
            var caminhoXml = _configuration.GetSection("CaminhoPedidos").Value;
            var caminhoXmlMover = _configuration.GetSection("CaminhoMoverPedidos").Value;

            Console.WriteLine("Iniciando Integração dos Xmls de vendas");
            Console.WriteLine($"Caminho dos arquivos: {caminhoXml}");
            Console.WriteLine($"Caminho para mover os arquivos depois de integrados: {caminhoXmlMover}");

            Console.WriteLine($"Total de arquivos localizados no diretorio {caminhoXml} : {Directory.GetFiles(caminhoXml, "*.xml").Length}");

            foreach (string file in Directory.GetFiles(caminhoXml, "*.xml"))
            {
                using (FileStream fileStream = new FileStream(file, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Order));
                    var order = ((Order)serializer.Deserialize(fileStream));

                    Console.WriteLine($"Integrando Order: {order.OrderNumber}");
                    Console.WriteLine($"Verificando se o pedido já se encontra na base de dados");

                    if (!_orderRepository.OrderExists(order.OrderNumber))
                    {
                        Console.WriteLine($"Inserindo Order {order.OrderNumber} na base de dados");
                        _orderRepository.Insert(order);
                        _uow.Commit();
                        Console.WriteLine($"Order {order.OrderNumber} inserida com sucesso");
                    }
                }
                Console.WriteLine($"Movendo arquivo {file} para a pasta {caminhoXmlMover}");
                File.Move(file, caminhoXmlMover + $"\\{file.Split('\\').Last()}", true);
            }

        }
    }
}
