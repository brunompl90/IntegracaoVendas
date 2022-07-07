using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using IntegracaoVendas.Dominio.Extensions;
using IntegracaoVendas.Dominio.Models.PeditosTxt;
using IntegracaoVendas.Dominio.Repositorys.InfosPedidosTxt;

namespace IntegracaoVendas.Dominio.Services.GeracaoTxt
{
    public class InfoPedidosTxtAdapter : IInfoPedidosTxtAdapter
    {
        private readonly IInfoPedidosTxtRepository _infoPedidosTxtRepository;
        public InfoPedidosTxtAdapter(IInfoPedidosTxtRepository infoPedidosTxtRepository)
        {
            _infoPedidosTxtRepository = infoPedidosTxtRepository;
        }
        public List<InfoPedido> GetInfoPedidos()
        {
            var infosPedidos = new List<InfoPedido>();
            var pedidosDtos = _infoPedidosTxtRepository.GetInfoPedidosNaoEnviados();

            var pedidosAgrupados = pedidosDtos.GroupBy(p => p.PEDIDO);

            foreach (var pedidos in pedidosAgrupados)
            {
                infosPedidos.Add(CreateInfoPedido(pedidos));
            }

            return infosPedidos;
        }

        public InfoPedido CreateInfoPedido(IGrouping<string, InfosPedidosDTO> infosPedidosGroup)
        {
            var pedido = infosPedidosGroup.FirstOrDefault();
            var infoPedido = new InfoPedido();
            infoPedido.PedidoKey = pedido?.PEDIDO;
            infoPedido.Bairro = pedido?.ENTREGA_BAIRRO?.Trim();
            infoPedido.CNPJ_Transportadora = pedido?.TRANSPORTADORA_CNPJ?.Trim();
            infoPedido.CPF_CNPJ = pedido?.CGC_CPF?.Trim();
            infoPedido.Cep = pedido?.ENTREGA_CEP?.Trim();
            infoPedido.Cidade = pedido?.ENTREGA_CIDADE?.Trim();
            infoPedido.CodigoOperacao = pedido?.CODIGO_OPERACAO?.Trim();
            infoPedido.Complemento = pedido?.ENTREGA_COMPLEMENTO?.Trim();
            infoPedido.DataEmissao = pedido?.EMISSAO.ToString("ddmmyyyy")?.Trim();
            infoPedido.DataLimite = pedido?.LIMITE_ENTREGA == null ? string.Empty : pedido?.LIMITE_ENTREGA?.ToString("ddmmyyyy").Trim();
            infoPedido.Endereco = pedido?.ENDERECO?.Trim();
            infoPedido.Estado = pedido?.ENTREGA_UF?.Trim();
            infoPedido.Nome = pedido?.NOME_CLIFOR?.Trim();
            infoPedido.Observacao = pedido?.OBS?.Trim();
            infoPedido.Pedido = pedido?.PEDIDO.Trim().PadLeft(10, '0');
            infoPedido.Solicitante = pedido?.CGC_CPF?.Trim();
            infoPedido.DetalhesPedido = new List<DetalhePedido>();

            foreach (var infosPedidos in infosPedidosGroup)
            {
                var detalheProduto = new DetalhePedido
                {
                    Descricao = infosPedidos.DESC_PRODUTO?.Trim().ObterStringSemAcentosECaracteresEspeciais(),
                    ItemPedido = infosPedidos.ITEM_PEDIDO?.Trim(),
                    Pedido = infosPedidos.PEDIDO.Trim().PadLeft(10, '0'),
                    Produto = infosPedidos.PRODUTO?.Trim(),
                    Quantidade = infosPedidos.QTDE_ENTREGAR.ToString().Trim().PadLeft(11, '0'),
                    UnidadeMedida = infosPedidos.UNIDADE?.Trim(),
                    ValorUnitario = infosPedidos.PRECO1.ToString().Trim().Replace(",", "").Replace(".", "").PadLeft(16, '0').Trim()
                };

                infoPedido.DetalhesPedido.Add(detalheProduto);
            }

            return infoPedido;
        }
    }
}
