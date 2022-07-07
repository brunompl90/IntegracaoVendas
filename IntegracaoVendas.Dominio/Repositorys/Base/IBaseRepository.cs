using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace IntegracaoVendas.Dominio.Repositorys.Base
{
    public interface IBaseRepository<TEntity>
    {
        List<TEntity> GetAll();
        List<TEntity> GetWithWhereClause(Expression<Func<TEntity, bool>> predicate);
        TEntity GetFirstOrDefault();
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetQueryable();

        void Insert(TEntity entitie);
        void Insert(List<TEntity> entities);

        void Update(TEntity entitie);
        void Update(List<TEntity> entities);

        void Delete(TEntity entitie);
        void Delete(List<TEntity> entities);

        string ExecuteProcedure(string procedure);
    }
}
