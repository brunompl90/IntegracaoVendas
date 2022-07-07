using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoVendas.Dominio.SFTP
{
    public interface IDownloadFileFromServer
    {
        int DownloadFile(string serverFileLocation, string fileDestination);
    }
}
