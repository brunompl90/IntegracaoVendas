using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace IntegracaoVendas.Dominio.SFTP
{
    public class DownloadFileFromServer : IDownloadFileFromServer
    {

        public DownloadFileFromServer(IConfiguration configuration)
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


        public int DownloadFile(string serverFileLocation, string fileDestination)
        {
            var connectionInfo = new ConnectionInfo(host, username, new PasswordAuthenticationMethod(username, password));
            // Upload File
            using (var sftp = new SftpClient(connectionInfo))
            {

                sftp.Connect();
                sftp.ChangeDirectory(serverFileLocation);

                var files = sftp.ListDirectory(serverFileLocation);

                foreach (var file in files.Where(f => f.Name.Contains(".xml")))
                {
                    using (Stream fileStream = File.Create( $"{fileDestination}//{file.Name}"))
                    {
                        sftp.DownloadFile(file.FullName, fileStream);
                    }
                }



                sftp.Disconnect();
            }

            return 0;
        }
    }
}
