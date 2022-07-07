using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using IntegracaoVendas.Dominio.Utils.Xmls;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Dominio.Services.MoverNFs
{
    public class MoveNfsXmlsService : IMoveNfsXmlsService
    {
        public string CaminhoArquivo { get; set; }

        private readonly IConfiguration _configuration;
        public MoveNfsXmlsService(IConfiguration configuration)
        {
            _configuration = configuration;
            CaminhoArquivo = _configuration.GetSection("CaminhoNFs").Value;
        }
        public void MoverXmlsTransportadora()
        {
            var caminhos = CaminhoArquivo.Split(";");

            foreach (var caminho in caminhos)
            {
                var arquivos = System.IO.Directory.GetFiles(caminho, "*.xml");
                Console.WriteLine($"Movendo xmls do caminho: {caminho}");
                Console.WriteLine($"Total de xmls para mover: {arquivos.Length}");
                foreach (var file in arquivos)
                {
                    var nomeXml = file.Split('\\').Last();
                    dynamic xml = new DynamicXml(file);
                    var nomeTransportadora = xml.NFe.infNFe.transp.transporta.xNome.XmlValue.ToString().Trim();
                    Console.WriteLine($"Criando subpasta: {caminho}\\{nomeTransportadora} caso não exista");
                    CreateDirectory(caminho, nomeTransportadora);
                    Console.WriteLine($"Movento xml: {nomeXml} para a subpasta: {nomeTransportadora}");
                    File.Move(file, @$"{caminho}\{nomeTransportadora}\{nomeXml}");
                }
            }
            Console.WriteLine($"Xmls Movidos com sucesso!");
        }

        private void CreateDirectory(string caminho, string transportadora)
        {
            System.IO.Directory.CreateDirectory(@$"{caminho}/{transportadora}");
        }
    }
}
