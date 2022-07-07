using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using IntegracaoVendas.Dominio.Models.PedidosApi;
using IntegracaoVendas.Dominio.Services.PedidosKeeper;
using Microsoft.Extensions.Logging;

namespace IntegracaoVendas.Dominio.Services
{
    public class InformacoesPedidosIntegrationApi : IInformacoesPedidosIntegrationApi
    {
        private readonly IIntegradorKeepersClient _integradorKeepersClient;
        private readonly IPedidosKeeperService _pedidosKeeperService;
        private readonly ILogger _logger;
        public InformacoesPedidosIntegrationApi(IIntegradorKeepersClient integradorKeepersClient,
                                                IPedidosKeeperService pedidosKeeperService,
                                                ILogger<InformacoesPedidosIntegrationApi> logger)
        {
            _integradorKeepersClient = integradorKeepersClient;
            _pedidosKeeperService = pedidosKeeperService;
            _logger = logger;
        }
        public void InitIntegration()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            _logger.LogInformation("Iniciando Integração dos pedidos via api");

            var pedidos = _pedidosKeeperService.GetPedidoApiKeepers();
            foreach (var pedido in pedidos)
            {
                _integradorKeepersClient.PostOrder(pedido);
            }
            
        }

        public PedidoApiKeeper GetPedidoTest()
        {
            var json = @"{
                          ""order_number"": 12345678,
                                    ""operation_code"": ""M3S"",
                                    ""observation"": ""Integração de pedidos"",
                                    ""expected_date_shipment"": ""2020-09-20"",
                                    ""customer"": {
                                        ""name"": ""NOME DO CLIENTE"",
                                        ""fantasy_name"": ""NOME FANTASIA DO CLIENTE"",
                                        ""cnpj_cpf"": ""999.999.999-99"",
                                        ""address"": ""PRAÇA DA SÉ"",
                                        ""number"": ""221"",
                                        ""complement"": ""SALA 1"",
                                        ""neighbourhood"": ""SÉ"",
                                        ""city"": ""SÃO PAULO"",
                                        ""state"": ""SP"",
                                        ""postal_code"": ""01001-000"",
                                        ""state_registration"": ""999.999.999.999"",
                                        ""is_foreign"": false,
                                        ""country_code"": 1058
                                    },
                                    ""transport"": {
                                        ""name"": ""AGENCIA CANHEMA POSTAGEM EXPRESSA LTDA"",
                                        ""fantasy_name"": ""AGF DOM JOAO VI"",
                                        ""cnpj_cpf"": ""99.999.999/9999-99"",
                                        ""address"": ""AV DOM JOAO VI"",
                                        ""number"": ""183"",
                                        ""complement"": null,
                                        ""neighbourhood"": ""TABOÃO"",
                                        ""city"": ""DIADEMA"",
                                        ""state"": ""SP"",
                                        ""postal_code"": ""09940-150"",
                                        ""state_registration"": ""999.999.999.999""
                                    },
                                    ""items"": [
                                    {
                                        ""product"": ""TESTE0201"",
                                        ""description"": ""DESCRIÇÃO DO PRODUTO TESTE0201"",
                                        ""unit_of_measurement"": ""UN"",
                                        ""quantity"": 1,
                                        ""unit_price"": 12.1,
                                        ""total_price"": 12.1,
                                        ""lot"": ""string""
                                    }
                                    ]
                                }";


            var pedido = JsonSerializer.Deserialize<PedidoApiKeeper>(json);

            return pedido;
        }
    }
}
