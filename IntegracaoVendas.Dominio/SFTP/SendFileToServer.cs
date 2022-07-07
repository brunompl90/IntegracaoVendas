using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Dominio.SFTP
{
    public class SendFileToServer : ISendFileToServer
    {

        public SendFileToServer(IConfiguration configuration)
        {
            this.host = configuration.GetSection("SFTP_HOST").Value;
            this.username = configuration.GetSection("SFTP_USER").Value;
            this.password = configuration.GetSection("SFTP_PASSWORD").Value;
        }

        // Enter your host name or IP here
        private string host;

        // Enter your sftp username here
        private string username;

        // Enter your sftp password here
        private string password;


        public int Send(string fileName, string serverFileLocation)
        {
            var connectionInfo = new ConnectionInfo(host, username, new PasswordAuthenticationMethod(username, password));
            // Upload File
            using (var sftp = new SftpClient(connectionInfo))
            {

                sftp.Connect();
                sftp.ChangeDirectory(serverFileLocation);
                using (var uplfileStream = System.IO.File.OpenRead(fileName))
                {
                    Console.WriteLine($"Enviando o arquivo {fileName} para o diretorio {serverFileLocation}");
                    sftp.UploadFile(uplfileStream, fileName.Split('\\').Last(), true);
                }

                sftp.Disconnect();
            }

            return 0;
        }
    }
}
