using System;
using System.Collections.Generic;
using System.Text;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;

namespace IntegracaoVendas.Data.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        public Microsoft.EntityFrameworkCore.DbContext Context { get; set; }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();

        }

        public void SetContext(Microsoft.EntityFrameworkCore.DbContext context)
        {
            this.Context = context;
        }
    }
}
