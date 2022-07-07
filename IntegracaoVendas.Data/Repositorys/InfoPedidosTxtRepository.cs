using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Dominio.Models.PeditosTxt;
using IntegracaoVendas.Dominio.Repositorys.InfosPedidosTxt;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;

namespace IntegracaoVendas.Data.Repositorys
{
    public class InfoPedidosTxtRepository : BaseRepository<InfosPedidosDTO>, IInfoPedidosTxtRepository
    {
        public InfoPedidosTxtRepository(IUnitOfWork unitOfWork, IntegracaoDbContext context) : base(unitOfWork, context)
        {
        }

        public List<InfosPedidosDTO> GetInfoPedidosNaoEnviados()
        {
            var query = @"SELECT A.PEDIDO, 
	                           CGC_CPF, 
	                           NOME_CLIFOR, 
                               ENTREGA_ENDERECO + ',' + ENTREGA_NUMERO AS ENDERECO,
	                           ENTREGA_COMPLEMENTO, 
	                           ENTREGA_BAIRRO, 
	                           ENTREGA_CIDADE, 
	                           ENTREGA_UF, 
	                           ENTREGA_CEP, 
	                           D.CGC AS TRANSPORTADORA_CNPJ, 
	                           B.LIMITE_ENTREGA, 
	                           A.EMISSAO, 
	                           'M3X' as CODIGO_OPERACAO, 
	                           A.OBS, 
	                           D.CGC, --PRIMEIRA PARTE
                               ITEM_PEDIDO, 
	                           RTRIM(LTRIM(B.PRODUTO)) + RTRIM(LTRIM(B.COR_PRODUTO)) AS PRODUTO, 
	                           DESC_PRODUTO, 
	                           B.QTDE_ENTREGAR,   
	                           E.UNIDADE,
	                           PRECO1
                        FROM VENDAS A JOIN VENDAS_PRODUTO B 
                          ON A.PEDIDO = B.PEDIDO
                         JOIN CADASTRO_CLI_FOR C 
                          ON A.CLIENTE_ATACADO = C.NOME_CLIFOR
                        JOIN TRANSPORTADORAS D 
                          ON A.TRANSPORTADORA = D.TRANSPORTADORA
                        JOIN PRODUTOS E 
                          ON E.PRODUTO = B.PRODUTO
                        where QTDE_ENTREGAR > 0
                        AND FILIAL = 'E-COMMERCE NOVA'
                        AND EMISSAO >=  CAST( GETDATE() AS Date )
                        AND not exists(select 1 from PEDIDOS_ENVIADOS P where P.PEDIDO = A.PEDIDO)
                        AND A.TIPO = 'ECOMMERCE'            
                         ";

            var pedidos = GetFromQuery(query);
            return pedidos;
        }
    }
}
