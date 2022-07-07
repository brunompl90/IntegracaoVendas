using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Renci.SshNet.Sftp;

namespace IntegracaoVendas.Dominio.SFTP
{
    public class MoveFileFromServer : IMoveFileFromServer
    {

        public MoveFileFromServer(IConfiguration configuration)
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


        public int Move(string serverFileLocation, string destinationFileLocation)
        {
            var connectionInfo = new ConnectionInfo(host, username, new PasswordAuthenticationMethod(username, password));
            // Upload File
            using (var sftp = new SftpClient(connectionInfo))
            {

                sftp.Connect();
                sftp.ChangeDirectory(serverFileLocation);

                var files = sftp.ListDirectory(serverFileLocation);
                foreach (SftpFile file in files.Where(f => f.Name.Contains(".xml")))
                {
                    Console.WriteLine($"Movento o arquivo {file.Name} para o diretorio {destinationFileLocation}");
                    file.MoveTo(destinationFileLocation + file.Name);
                }

                sftp.Disconnect();
            }

            return 0;
        }
    }
}
