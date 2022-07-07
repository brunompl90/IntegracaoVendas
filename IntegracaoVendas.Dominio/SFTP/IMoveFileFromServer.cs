using System;
using System.Collections.Generic;
using System.Text;

namespace IntegracaoVendas.Dominio.SFTP
{
    public interface IMoveFileFromServer
    {
        int Move(string serverFileLocation, string destinationFileLocation);
    }
}
