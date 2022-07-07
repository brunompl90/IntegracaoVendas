using System;
using System.IO;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Data.Repositorys;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.InfosPedidosTxt;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Services.GeracaoTxt;
using IntegracaoVendas.Dominio.Services.MoverNFs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegracaoMoveNFs
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            _serviceProvider = services.BuildServiceProvider();
            var moverXmlService = _serviceProvider.GetService<IMoveNfsXmlsService>();
            Console.WriteLine("Movendo xmls para as subpastas");
            moverXmlService.MoverXmlsTransportadora();
            Console.Write("Pressione <Enter> para fechar... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
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
            services.AddTransient<IMoveNfsXmlsService, MoveNfsXmlsService>();

            return services;
        }
    }
}
