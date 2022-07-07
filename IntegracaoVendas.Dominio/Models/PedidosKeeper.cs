using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntegracaoVendas.Dominio.Models
{
    public class PedidoKeeper
    {
        [Key]
        public string Pedido { get; set; }
        public string PedidoMagento { get; set; }
        public string EntregaRazaoSocial { get; set; }
        public string CidadeUF { get; set; }
        public string Transportadora { get; set; }
        public decimal Peso { get; set; }
        public decimal Volume { get; set; }
        public DateTime Envio { get; set; }
        public DateTime Retorno { get; set; }
        public DateTime Expedicao { get; set; }
        public string VERSAO { get; set; }
        public DateTime Emissao { get; set; }
        public int Status { get; set; }
        public int TIPO { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
    }

    public class ProdutoKeeper
    {
        public string Pedido { get; set; }
        public string NumeroItem { get; set; }
        [Key]
        public string IdProduto { get; set; }
        public string CorProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string UnidadeMedida { get; set; }
        public string Quantidade { get; set; }
        public string ValorUnitario { get; set; }

    }
}
