using System;
using System.IO;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Data.Repositorys;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.InformacoesCorreios;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using IntegracaoVendas.Dominio.Services;
using IntegracaoVendas.Dominio.SFTP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Renci.SshNet;

namespace IntegracaoSFTP
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static IServiceProvider _serviceProvider;

        // Enter your host name or IP here
        private static string host = "10.226.107.25";
        // Enter your sftp username here
        private static string username = "linx.amer.test";
        // Enter your sftp password here
        private static string password = ":8q4gy1=#2GU";



        static void Main(string[] args)
        {
            var services = ConfigureServices();
            _serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("Iniciando Integração dos arquivos via SFTP");

            var _sftpIntegration = _serviceProvider.GetService<ISFTPIntegration>();

            _sftpIntegration.InitIntegration();

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
         
            services.AddTransient<ISendFileToServer, SendFileToServer>();
            services.AddTransient<ISFTPIntegration, SFTPIntegration>();
            services.AddTransient<IMoveFileFromServer, MoveFileFromServer>();
            services.AddTransient<IDownloadFileFromServer, DownloadFileFromServer>();
            return services;
        }
    }
}
