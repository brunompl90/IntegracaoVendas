using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.InformacoesPedidos;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Dominio.Services
{
    public class InformacoesPedidosIntegration : IInformacoesPedidosIntegration
    {
        private readonly IConfiguration _configuration;
        private readonly IInformacoesPedidosRepository _informacoesPedidosRepository;
        private readonly IUnitOfWork _uow;
        public InformacoesPedidosIntegration(IInformacoesPedidosRepository informacoesPedidosRepository,
                                IUnitOfWork uow,
                                IConfiguration configuration)
        {
            _informacoesPedidosRepository = informacoesPedidosRepository;
            _configuration = configuration;
            _uow = uow;
        }
        public void InitIntegration()
        {
            var caminhoTxt = _configuration.GetSection("CaminhoPedidos").Value;
            var caminhoTxtMover = _configuration.GetSection("CaminhoMoverPedidos").Value;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Iniciando Integração dos txt de informações de pedidos");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Caminho dos arquivos: {caminhoTxt}");
            Console.WriteLine($"Caminho para mover os arquivos depois de integrados: {caminhoTxtMover}");

            Console.WriteLine($"Total de arquivos localizados no diretorio {caminhoTxt} : {Directory.GetFiles(caminhoTxt, "*.txt").Length}");

            foreach (string file in Directory.GetFiles(caminhoTxt, "*.txt"))
            {
                using (FileStream fileStream = new FileStream(file, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        var infosPedido = reader.ReadToEnd();
                        var pedidoSplit = infosPedido.Split(' ');
                        var idPedido = pedidoSplit[1].Trim();
                        var volumeSplit = infosPedido.Split(new string[] {$"Pedido {idPedido} confirmado com"},
                            StringSplitOptions.None);

                        var volumePedido = int.Parse(volumeSplit[1].Trim().Split(' ')[0]);
                        var pesoPedido = decimal.Parse(pedidoSplit.Last().Trim());

                        if (!_informacoesPedidosRepository.OrderExists(idPedido))
                        {

                            var pedido = new INFORMACOES_PEDIDO
                            {
                                PEDIDO = idPedido,
                                PESO = pesoPedido,
                                VOLUME = volumePedido
                            };

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"Inserindo Pedido {idPedido} na base de dados");
                            _informacoesPedidosRepository.Insert(pedido);
                            _uow.Commit();
                            Console.WriteLine($"Pedido {idPedido} inserida com sucesso");
                        }
                    }

                    
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Movendo arquivo {file} para a pasta {caminhoTxtMover}");
                Console.ForegroundColor = ConsoleColor.White;
                File.Move(file, caminhoTxtMover + $"\\{file.Split('\\').Last()}", true);
            }

        }
    }
}
