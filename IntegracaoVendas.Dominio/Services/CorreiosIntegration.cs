using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Dominio.Services
{
    public class CorreiosIntegration : ICorreiosIntegration
    {
        private readonly IConfiguration _configuration;
        private readonly IInformacoesCorreiosRepository _informacoesCorreiosRepository;
        private readonly IUnitOfWork _uow;
        public CorreiosIntegration(IInformacoesCorreiosRepository informacoesCorreiosRepository,
                                IUnitOfWork uow,
                                IConfiguration configuration)
        {
            _informacoesCorreiosRepository = informacoesCorreiosRepository;
            _configuration = configuration;
            _uow = uow;
        }
        public void InitIntegration()
        {
            var urlWebService = _configuration.GetSection("UrlWebServiceCorreios").Value;
            var usuario = _configuration.GetSection("UsuarioWebCorreios").Value;
            var senha = _configuration.GetSection("SenhaoWebCorreios").Value;
            var dataInicio = _configuration.GetSection("DataInicio").Value;
            var dataFim = _configuration.GetSection("DataFim").Value;
            var fullUrl = $"{urlWebService}";


            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Iniciando Integração com o WebService dos correios");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Url do WebService: {fullUrl}");

            var notasStream = DownloadFile(fullUrl, usuario, senha, dataInicio, dataFim);
            string linha;
            using (var ms = new MemoryStream(notasStream.Result))
            using (var sw = new StreamReader(ms, Encoding.UTF8))
                while ((linha = sw.ReadLine()) != null)
                {
                    var line = linha?.Split(';');

                    if (line == null || line.Length == 1)
                    {
                        continue;
                    }

                    var nota = line[0];
                    var objeto = line[2];
                    var status = line[1];
                    var data = line[3] == "-" ? DateTime.MinValue : DateTime.Parse(line[3]);

                    Console.WriteLine($"Integrando a nota: {nota} Objeto: {objeto}");
                    Console.WriteLine("Verificando se o objeto já se encontra na base de dados");

                    if (!_informacoesCorreiosRepository.OrderExists(objeto))
                    {

                        var infosCorrerios = new INFORMACOES_CORREIO
                        {
                            OBJETO = objeto,
                            STATUS = status,
                            NOTA = nota,
                            DATA = data
                        };

                        if (infosCorrerios.DATA != null && infosCorrerios.DATA == DateTime.MinValue)
                        {
                            infosCorrerios.DATA = null;
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Inserindo Objeto {objeto} na base de dados");
                        _informacoesCorreiosRepository.Insert(infosCorrerios);
                        _uow.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Objeto {objeto} inserida com sucesso");
                    }
                }
        }

        public static async Task<byte[]> DownloadFile(string url, string usuario, string senha, string dataInicio, string dataFim)
        {
            var parms = $"?codigoAGF=303&usuario={usuario}&senha={senha}&tipoArquivo=FILE&formatoArquivo=CSV&separadorArquivo=;&cabecalhoArquivo=N&camposArquivo=NF|SITUACAO|SRO|DATA_SITUACAO&tamanhoArquivo=45|11|59|80&cep=&data_inicio={dataInicio}&data_fim={dataFim}";
            using (var client = new HttpClient())
            {

                using (var result = await client.GetAsync(url + parms))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsByteArrayAsync();
                    }

                }
            }
            return null;
        }
    }
}
