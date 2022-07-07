using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoVendas.Dominio.SFTP
{
    public interface ISendFileToServer
    {
        int Send(string fileName, string serverFileLocation);
    }
}
