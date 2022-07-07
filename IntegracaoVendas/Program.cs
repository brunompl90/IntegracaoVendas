using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Data.Repositorys;
using IntegracaoVendas.Dominio.Models;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegracaoVendas
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            _serviceProvider = services.BuildServiceProvider();

            var _orderIntegration = _serviceProvider.GetService<IOrderIntegration>();
            _orderIntegration.InitIntegration();

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
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderIntegration, OrderIntegration>();
            return services;
        }
    }
}
