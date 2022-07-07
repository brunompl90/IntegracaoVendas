using System;
using System.IO;
using Hangfire;
using Hangfire.MemoryStorage;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Data.Repositorys;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios;
using IntegracaoVendas.Dominio.Repositorys.Order;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Repositorys.VendasCanceladas;
using IntegracaoVendas.Dominio.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegracaoXmls
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            _serviceProvider = services.BuildServiceProvider();


            ConfigureHangFire();

            var executionIntervalEstoque = int.Parse(_configuration.GetSection("XmlEstoqueIntervalMinutes").Value);
            var executionIntervalPreco = int.Parse(_configuration.GetSection("XmlPrecoIntervalMinutes").Value);
            var executionIntervalPrecoLiquido = int.Parse(_configuration.GetSection("XmlPrecoLiquidoIntervalMinutes").Value);
            var executionIntervalOrders = int.Parse(_configuration.GetSection("XmlOrdersIntervalHours").Value);

            using (var server = new BackgroundJobServer())
            {
                IntegrationEstoque();
                IntegrationPreco();
                IntegrationPrecoLiquido();
                IntegrationOrdem();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Execuções Iniciais Finalizadas.");
                Console.WriteLine("As Proximas execuções ocorrerão nos períodos agendados.");

                RecurringJob.AddOrUpdate(
                    () => IntegrationEstoque(),
                    Cron.MinuteInterval(executionIntervalEstoque));

                RecurringJob.AddOrUpdate(
                    () => IntegrationPreco(),
                    Cron.DayInterval(executionIntervalPreco));

                RecurringJob.AddOrUpdate(
                    () => IntegrationPrecoLiquido(),
                    Cron.DayInterval(executionIntervalPrecoLiquido));

                RecurringJob.AddOrUpdate(
                () => IntegrationOrdem(),
                Cron.HourInterval(executionIntervalOrders));

                Console.ReadKey();
            }
        }

        public static void IntegrationEstoque()
        {
            var pedidoIntegration = _serviceProvider.GetService<IXmlEstoqueIntegration>();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} Iniciando Integração do XML de Estoque.");
            pedidoIntegration.InitIntegration("Estoque");
        }

        public static void IntegrationPreco()
        {
            var pedidoIntegration = _serviceProvider.GetService<IXmlEstoqueIntegration>();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} Iniciando Integração do XML de Preço.");
            pedidoIntegration.InitIntegration("Preço");
        }

        public static void IntegrationPrecoLiquido()
        {
            var pedidoIntegration = _serviceProvider.GetService<IXmlEstoqueIntegration>();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} Iniciando Integração do XML de Preço Liquido.");
            pedidoIntegration.InitIntegration("PreçoLiquido");
        }

        public static void IntegrationOrdem()
        {
            var pedidoIntegration = _serviceProvider.GetService<IXmlEstoqueIntegration>();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss} Iniciando Integração do XML de Orders.");
            pedidoIntegration.InitIntegration("Order");
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
            services.AddTransient<IXmlEstoqueIntegration, XmlEstoqueIntegration>();
            services.AddTransient<IXmlRepository, XmlRepository>();
            services.AddTransient<IInformacoesCorreiosRepository, InformacoesCorreiosRepository>();
            services.AddTransient<IVendasCanceladasRepository, VendasCanceladasRepository>();
            services.AddTransient<IBaseDapperRepository,BaseDapperRepository>();
            return services;
        }

        private static void ConfigureHangFire()
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();
        }
    }
}
