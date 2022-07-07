using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Models.PedidosApi;
using IntegracaoVendas.Dominio.Repositorys.PedidosKeeper;

namespace IntegracaoVendas.Dominio.Services.PedidosKeeper
{
    public interface IPedidosKeeperService
    {
        List<PedidoApiKeeper> GetPedidoApiKeepers();
    }
    public class PedidosKeeperService : IPedidosKeeperService
    {
        private readonly IPedidoKeeperRepository _pedidoKeeperRepository;
        private readonly IProdutoKeeperRepository _produtoKeeperRepository;
        private readonly IPedidosKeeperAdapter _pedidosKeeperAdapter;
        public PedidosKeeperService(IPedidoKeeperRepository pedidoKeeperRepository,
                                    IProdutoKeeperRepository produtoKeeperRepository,
                                    IPedidosKeeperAdapter pedidosKeeperAdapter)
        {
            _pedidoKeeperRepository = pedidoKeeperRepository;
            _produtoKeeperRepository = produtoKeeperRepository;
            _pedidosKeeperAdapter = pedidosKeeperAdapter;
        }
        public List<PedidoApiKeeper> GetPedidoApiKeepers()
        {
            var pedidosParaEnviar = new List<PedidoApiKeeper>();
            
            var pedidos = _pedidoKeeperRepository.GetAll();

            foreach (var pedido in pedidos)
            {
                var produtos = _produtoKeeperRepository.GetWithWhereClause(p => p.Pedido == pedido.Pedido);
                var pedidoParaEnvio = _pedidosKeeperAdapter.GePedidoApiKeeper(pedido, produtos);
                pedidosParaEnviar.Add(pedidoParaEnvio);
            }

            return pedidosParaEnviar;
        }
    }

    public interface IPedidosKeeperAdapter
    {
        PedidoApiKeeper GePedidoApiKeeper(PedidoKeeper pedido, List<ProdutoKeeper> produtos);
    }

    public class PedidosKeeperAdapter : IPedidosKeeperAdapter
    {
        public PedidoApiKeeper GePedidoApiKeeper(PedidoKeeper pedido, List<ProdutoKeeper> produtos)
        {

            var pedidoApiKeeper = new PedidoApiKeeper();
            pedidoApiKeeper.order_number = int.Parse(pedido.Pedido);
            pedidoApiKeeper.customer = new Customer
            {
                fantasy_name = pedido.EntregaRazaoSocial,
                cnpj_cpf = pedido.Cpf,
                name = pedido.EntregaRazaoSocial,
                address = pedido.Endereco,
                city = pedido.CidadeUF.Split('/')[0],
                state = pedido.CidadeUF.Split('/')[1],
                postal_code = pedido.Cep
            };

            pedidoApiKeeper.transport = new Transport
            {
                name = pedido.Transportadora
            };

            pedidoApiKeeper.items = new List<Item>();


            foreach (var produto in produtos)
            {
               var itemPedido = new Item();
               itemPedido.description = produto.DescricaoProduto;
               itemPedido.product = produto.IdProduto;
               itemPedido.unit_of_measurement = produto.UnidadeMedida;
               itemPedido.quantity = int.Parse(produto.Quantidade);
               itemPedido.total_price = decimal.Parse(produto.ValorUnitario) * itemPedido.quantity;
               itemPedido.unit_price = decimal.Parse(produto.ValorUnitario);
               pedidoApiKeeper.items.Add(itemPedido);
            }

            return pedidoApiKeeper;
        }
    }
}
