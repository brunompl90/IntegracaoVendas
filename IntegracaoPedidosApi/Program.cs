using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Data.Repositorys;
using IntegracaoVendas.Dominio.Logger;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.InformacoesPedidos;
using IntegracaoVendas.Dominio.Repositorys.PedidosKeeper;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Services;
using IntegracaoVendas.Dominio.Services.PedidosKeeper;
using Microsoft.Extensions.Logging;

namespace IntegracaoPedidosApi
{
    
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            _serviceProvider = services.BuildServiceProvider();
            _serviceProvider.AddColoredConsoleLogger();
            var pedidoIntegration = _serviceProvider.GetService<IInformacoesPedidosIntegrationApi>();
            pedidoIntegration.InitIntegration();
            var logger = _serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation("Execução finalizada, digite uma tecla para fechar o programa.");
            Console.ReadKey();

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
            services.AddTransient<IInformacoesPedidosRepository, InformacoesPedidosRepository>();
            services.AddTransient<IInformacoesPedidosIntegrationApi, InformacoesPedidosIntegrationApi>();
            services.AddTransient<IIntegradorKeepersClient, IntegradorKeepersClient>();
            services.AddTransient<IPedidoKeeperRepository, PedidoKeeperRepository>();
            services.AddTransient<IProdutoKeeperRepository, ProdutoKeeperRepository>();
            services.AddTransient<IPedidosKeeperAdapter, PedidosKeeperAdapter>();
            services.AddTransient<IPedidosKeeperService, PedidosKeeperService>();
           
            services.AddLogging();
            return services;
        }
    }


}
