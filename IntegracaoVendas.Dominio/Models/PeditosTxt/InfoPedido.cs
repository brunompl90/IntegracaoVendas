using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoVendas.Dominio.Models.PeditosTxt
{
    public class InfosPedidosDTO
    {
       
        public string PEDIDO { get; set; }
        public string CGC_CPF { get; set; }
        public string NOME_CLIFOR { get; set; }
        public string ENDERECO { get; set; }
        public string ENTREGA_COMPLEMENTO { get; set; }
        public string ENTREGA_CIDADE { get; set; }
        public string ENTREGA_BAIRRO { get; set; }
        public string ENTREGA_UF { get; set; }
        public string ENTREGA_CEP { get; set; }
        public string TRANSPORTADORA_CNPJ { get; set; }
        public DateTime? LIMITE_ENTREGA { get; set; }
        public DateTime EMISSAO { get; set; }
        public string OBS { get; set; }
        public string CODIGO_OPERACAO { get; set; }
        public string ITEM_PEDIDO { get; set; }
        public string PRODUTO { get; set; }
        public string UNIDADE { get; set; }
        public string DESC_PRODUTO { get; set; }
        public int QTDE_ENTREGAR { get; set; }
        public decimal PRECO1 { get; set; }

    }

    public class PosicaoArquivoAttribute : Attribute
    {
        public int PosicaoInicial { get; set; }
        public int Tamanho { get; set; }
       
        public PosicaoArquivoAttribute(int posicaoInicial, int tamanho)
        {
            PosicaoInicial = posicaoInicial;
            Tamanho = tamanho;

        }
    }
    public class InfoPedido
    {
        public string PedidoKey { get; set; }
        [PosicaoArquivo(1, 1)]
        public string Fixo => "1";
        [PosicaoArquivo(2, 10)]
        public string Pedido { get; set; }
        [PosicaoArquivo(12, 15)]
        public string Solicitante { get; set; }
        [PosicaoArquivo(27, 35)]
        public string Nome { get; set; }
        [PosicaoArquivo(62, 30)]
        public string Endereco { get; set; }
        [PosicaoArquivo(92, 20)]
        public string Complemento { get; set; }
        [PosicaoArquivo(121, 15)]
        public string Bairro { get; set; }
        [PosicaoArquivo(127, 20)]
        public string Cidade { get; set; }
        [PosicaoArquivo(147, 2)]
        public string Estado { get; set; }
        [PosicaoArquivo(149, 8)]
        public string Cep { get; set; }
        [PosicaoArquivo(157, 14)]
        public string CPF_CNPJ { get; set; }
        [PosicaoArquivo(171, 8)]
        public string DataLimite { get; set; }
        [PosicaoArquivo(179, 8)]
        public string DataEmissao { get; set; }
        [PosicaoArquivo(187, 3)]
        public string CodigoOperacao { get; set; }
        [PosicaoArquivo(190, 200)]
        public string Observacao { get; set; }
        [PosicaoArquivo(390, 14)]
        public string CNPJ_Transportadora { get; set; }

        public List<DetalhePedido> DetalhesPedido { get; set; }
    }

    public class DetalhePedido
    {
        [PosicaoArquivo(1, 1)]
        public string Fixo => "9";
        [PosicaoArquivo(2, 10)]
        public string Pedido { get; set; }
        [PosicaoArquivo(12, 3)]
        public string ItemPedido { get; set; }
        [PosicaoArquivo(15, 25)]
        public string Produto { get; set; }
        [PosicaoArquivo(40, 20)]
        public string Descricao { get; set; }
        [PosicaoArquivo(60, 2)]
        public string UnidadeMedida { get; set; }
        [PosicaoArquivo(62, 11)]
        public string Quantidade { get; set; }
        [PosicaoArquivo(73, 16)]
        public string ValorUnitario { get; set; }
    }
}
