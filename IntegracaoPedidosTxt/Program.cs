using IntegracaoVendas.Data.DbContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using IntegracaoVendas.Data.Repositorys;
using IntegracaoVendas.Dominio.Models.PeditosTxt;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.InfosPedidosTxt;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Services.GeracaoTxt;

namespace IntegracaoPedidosTxt
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            _serviceProvider = services.BuildServiceProvider();
            Console.WriteLine("Iniciando Geração dos arquivos de Pedidos");
            var pedidosAdapter = _serviceProvider.GetService<IInfoPedidosTxtAdapter>();
            var pedidos = pedidosAdapter.GetInfoPedidos();

            Console.WriteLine($"Total de Pedidos Localizados para geração: {pedidos.Count}");

            var geradorPedido = _serviceProvider.GetService<IGeradorPedidoTxt>();

            geradorPedido.GerarTxt(pedidos);

        }

        private static IConfigurationRoot ConfigureConfigurationRoot()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");

            var config = builder.Build();
            return config;
        }

        private static IServiceCollection ConfigureServices()
        {
            _configuration = ConfigureConfigurationRoot();
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(_configuration);
            services.AddSingleton<IntegracaoDbContext>();

            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IInfoPedidosTxtRepository, InfoPedidosTxtRepository>();
            services.AddTransient<IInfoPedidosTxtAdapter, InfoPedidosTxtAdapter>();
            services.AddTransient<IPedidoEnviadoRepository, PedidoEnviadoRepository>();
            services.AddTransient<IGeradorPedidoTxt, GeradorPedidoTxt>();
            
            return services;
        }
    }
}
