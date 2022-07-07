using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using IntegracaoVendas.Dominio.Models;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Dominio.SFTP
{
    public class SFTPIntegration : ISFTPIntegration
    {
        private readonly ISendFileToServer _sendFileToServer;
        private readonly IMoveFileFromServer _moveFileFromServer;
        private readonly IDownloadFileFromServer _downloadFileFromServer;
        private readonly IConfiguration _configuration;
        
        public SFTPIntegration(ISendFileToServer sendFileToServer, 
                               IMoveFileFromServer moveFileFromServer,
                               IDownloadFileFromServer downloadFileFromServer,
                               IConfiguration configuration)
        {
            _sendFileToServer = sendFileToServer;
            _moveFileFromServer = moveFileFromServer;
            _downloadFileFromServer = downloadFileFromServer;
            _configuration = configuration;
        }

        public void InitIntegration()
        {
           

            var caminhoOrder = _configuration.GetSection("Caminho_Download_Order").Value;
            var caminhoOrderSFTP = _configuration.GetSection("Caminho_Download_Order_SFTP").Value;
            var caminhoMoverOrderSFTP = _configuration.GetSection("Caminho_Move_Order_SFTP").Value;

            Console.WriteLine($"Iniciando Processo de Download dos arquivos da pasta {caminhoOrder}");
            DownloadXmlFromServer(caminhoOrder, caminhoOrderSFTP, caminhoMoverOrderSFTP);


            var caminhoEstoque = _configuration.GetSection("Caminho_Envio_Estoque").Value;
            var caminhoEstoqueSFTP = _configuration.GetSection("Caminho_Envio_Estoque_SFTP").Value;
            Console.WriteLine($"Enviando arquivos de Estoque da pasta {caminhoEstoque}");
            SendXmlToServer(caminhoEstoque, caminhoEstoqueSFTP);

            var Caminho_Envio_Preco = _configuration.GetSection("Caminho_Envio_Preco").Value;
            var Caminho_Envio_Preco_SFTP = _configuration.GetSection("Caminho_Envio_Preco_SFTP").Value;

            Console.WriteLine($"Enviando arquivos de Preço da pasta {Caminho_Envio_Preco}");
            SendXmlToServer(Caminho_Envio_Preco, Caminho_Envio_Preco_SFTP);

            var Caminho_Envio_Preco_Liquido = _configuration.GetSection("Caminho_Envio_Preco").Value;
            
            Console.WriteLine($"Enviando arquivos de Preço Liquido da pasta {Caminho_Envio_Preco_Liquido}");
            SendXmlToServer(Caminho_Envio_Preco_Liquido, Caminho_Envio_Preco_SFTP);


            var Caminho_Envio_Envio = _configuration.GetSection("Caminho_Envio_Envio").Value;
            var Caminho_Envio_Envio_SFTP = _configuration.GetSection("Caminho_Envio_Envio_SFTP").Value;

            Console.WriteLine($"Enviando arquivos de Envio da pasta {Caminho_Envio_Envio}");

            SendXmlToServer(Caminho_Envio_Envio, Caminho_Envio_Envio_SFTP);
        }


        private void SendXmlToServer(string caminhoXml, string caminhoServidor)
        {
            foreach (string file in Directory.GetFiles(caminhoXml, "*.xml"))
            {
                var sendStatus = _sendFileToServer.Send(file, caminhoServidor);

                Console.WriteLine($"Excluindo o arquivo {file}");
                File.Delete(file);
            }
        }

        private void DownloadXmlFromServer(string caminhoXml, string caminhoServidor, string caminhoParaMover)
        {
            var downloadStatus = _downloadFileFromServer.DownloadFile(caminhoServidor, caminhoXml);
            var moveStatus = _moveFileFromServer.Move(caminhoServidor, caminhoParaMover);
        }
    }
}
