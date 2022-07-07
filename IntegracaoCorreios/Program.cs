using System;
using System.IO;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Data.Repositorys;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios;
using IntegracaoVendas.Dominio.Repositorys.InformacoesPedidos;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegracaoCorreios
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            _serviceProvider = services.BuildServiceProvider();

            var pedidoIntegration = _serviceProvider.GetService<ICorreiosIntegration>();
            pedidoIntegration.InitIntegration();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Execução finalizada, digite uma tecla para fechar o programa.");
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
            services.AddTransient<IInformacoesCorreiosRepository, InformacoesCorreiosRepository>();
            services.AddTransient<ICorreiosIntegration, CorreiosIntegration>();
            return services;
        }
    }
}
