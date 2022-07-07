using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace IntegracaoVendas.Dominio.Repositorys.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; set; }
        void SetContext(DbContext context);
        void Commit();
    }
}
