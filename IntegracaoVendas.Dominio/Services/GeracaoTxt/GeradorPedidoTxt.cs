using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using IntegracaoVendas.Dominio.Models.PeditosTxt;
using IntegracaoVendas.Dominio.Repositorys.InfosPedidosTxt;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Dominio.Services.GeracaoTxt
{
    public interface IGeradorPedidoTxt
    {
        void GerarTxt(List<InfoPedido> infosPedido);
    }
    public class GeradorPedidoTxt : IGeradorPedidoTxt
    {
        public string CaminhoArquivo { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IPedidoEnviadoRepository _pedidoEnviadoRepository;
        private readonly IUnitOfWork _uow;
        public GeradorPedidoTxt(IConfiguration configuration,
                                IPedidoEnviadoRepository pedidoEnviadoRepository,
                                IUnitOfWork uow)
        {
            _configuration = configuration;
            _pedidoEnviadoRepository = pedidoEnviadoRepository;
            CaminhoArquivo = _configuration.GetSection("CaminhoPedidos").Value;
            _uow = uow;
        }

        public void GerarTxt(List<InfoPedido> infosPedido)
        {
            Console.WriteLine($"Os arquivos gerados serão salvos  no diretorio: {CaminhoArquivo}");
            foreach (var infoPedido in infosPedido)
            {
                Console.WriteLine($"Gerando Arquivo para o Pedido: {infoPedido.PedidoKey}");
                var txtArquivoBuilder = new StringBuilder();

                var propriedadesInfo = GetProPriedadesTxt(infoPedido);
                var propriedadesOrdenadas = propriedadesInfo.OrderBy(p => p.Posicao).ToList();
                txtArquivoBuilder.AppendLine(string.Join("", propriedadesOrdenadas.Select(p => p.Valor.ToString())));

                foreach (var detalhePedido in infoPedido.DetalhesPedido)
                {
                    var propriedadesInfoDetalhes = GetProPriedadesTxt(detalhePedido);
                    var propriedadesOrdenadasDetalhes = propriedadesInfoDetalhes.OrderBy(p => p.Posicao).ToList();
                    txtArquivoBuilder.AppendLine(string.Join("", propriedadesOrdenadasDetalhes.Select(p => p.Valor.ToString())));
                }

                txtArquivoBuilder.AppendLine("FIM");
                var arquivoFinal = txtArquivoBuilder.ToString();
                File.WriteAllText($"{CaminhoArquivo}\\{infoPedido.Pedido.TrimStart('0')}.txt", arquivoFinal);
                Console.WriteLine($"Arquivo para o Pedido: {infoPedido.PedidoKey} gerado com sucesso!");
                _pedidoEnviadoRepository.SalvarPedidoEnviado(infoPedido.PedidoKey);
                _uow.Commit();
            }


            Console.WriteLine($"Geração de arquivos de pedido Finalizada");
            Console.WriteLine($"Total de Arquivos Gerados: {infosPedido.Count}");
        }

        public List<(string Propriedade, object Valor, int Posicao, int Tamanho)> GetProPriedadesTxt<T>(T infoPedido)
        {
            var tuplesInfosPropriedade = new List<(string Propriedade, object Valor, int Posicao, int Tamanho)>();
            Type myType = infoPedido.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                var propAttribute = prop.GetCustomAttribute<PosicaoArquivoAttribute>();

                if (propAttribute != null)
                {
                    var valor = prop.GetValue(infoPedido)?.ToString();

                    var valorFormatado = valor == null ? string.Empty.PadRight(propAttribute.Tamanho, ' ') : valor.Substring(0, valor.Length < propAttribute.Tamanho ? valor.Length : propAttribute.Tamanho).PadRight(propAttribute.Tamanho, ' ');

                    var propriTupple = (Propriedade: prop.Name, Valor: valorFormatado, Posicao: propAttribute.PosicaoInicial, Tamanho: propAttribute.Tamanho);
                    tuplesInfosPropriedade.Add(propriTupple);
                }
            }

            return tuplesInfosPropriedade;
        }
    }
}
