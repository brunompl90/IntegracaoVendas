using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using IntegracaoVendas.Dominio.Models.PedidosApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace IntegracaoVendas.Dominio.Services
{
    public interface IIntegradorKeepersClient
    {
        public void PostOrder(PedidoApiKeeper pedido);

    }

    public class IntegradorKeepersClient : IIntegradorKeepersClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public IntegradorKeepersClient(IConfiguration configuration,
                                       ILogger<IntegradorKeepersClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public void PostOrder(PedidoApiKeeper pedido)
        {
            _logger.LogCritical($"Integrando Pedido: {pedido.order_number}");
            var apiKey = _configuration.GetSection("ApiKey").Value;
            var client = new RestClient("https://integradorhml.keepers.com.br");
            var request = new RestRequest("api/v1/orders", Method.POST);
            client.AddDefaultHeader("Authorization", $"Token {apiKey}");
            request.AddJsonBody(pedido);

            var response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.Created)
            {
                _logger.LogInformation($"Pedido: {pedido.order_number} criado com sucesso!");
            }

            if (statusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogError($"Falha ao adicionar o pedido: {pedido.order_number} erro: {response.Content}");
            }

            if (statusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogError($"Falha na autorização para adicionar o pedido: {pedido.order_number} error: {response.Content}");
            }
            
        }
    }
}
