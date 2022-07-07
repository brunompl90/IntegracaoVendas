using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using IntegracaoVendas.Data.DbContext;
using IntegracaoVendas.Dominio.Repositorys.Base;
using IntegracaoVendas.Dominio.Repositorys.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IntegracaoVendas.Data.Repositorys
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IntegracaoDbContext _context;
        public BaseRepository(IUnitOfWork unitOfWork, IntegracaoDbContext context)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.SetContext(context);
            _context = context;
        }

        public string ExecuteProcedure(string procedure)
        {

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procedure;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows && result.Read())
                    {

                        return result.GetString(0);
                    }
                }
            }

            return string.Empty;
        }

        public void Delete(TEntity entitie)
        {
            _unitOfWork.Context.Set<TEntity>().Remove(entitie);
        }

        public void Delete(List<TEntity> entities)
        {
            foreach (var entitie in entities)
            {
                _unitOfWork.Context.Set<TEntity>().Remove(entitie);
            }
        }

        public List<TEntity> GetAll()
        {
            var list = _unitOfWork.Context.Set<TEntity>().ToList();
            return list;
        }

        public List<TEntity> GetWithWhereClause(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _unitOfWork.Context.Set<TEntity>().Where(predicate).ToList();
            return list;
        }

        public void Insert(TEntity entitie)
        {
            _unitOfWork.Context.Add<TEntity>(entitie);
        }

        public void Insert(List<TEntity> entities)
        {
            foreach (var entitie in entities)
            {
                _unitOfWork.Context.Add<TEntity>(entitie);
            }
        }

        public void Update(TEntity entitie)
        {
            _unitOfWork.Context.Set<TEntity>().Update(entitie);
        }

        public void Update(List<TEntity> entities)
        {
            foreach (var entitie in entities)
            {
                _unitOfWork.Context.Set<TEntity>().Update(entitie);
            }
        }

        public TEntity GetFirstOrDefault()
        {
            var entitie = _unitOfWork.Context.Set<TEntity>().FirstOrDefault();
            return entitie;
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var entitie = _unitOfWork.Context.Set<TEntity>().FirstOrDefault(predicate);
            return entitie;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _unitOfWork.Context.Set<TEntity>().AsQueryable();
        }

        public List<TEntity> GetFromQuery(string query, params object[]paramters)
        {
            return _unitOfWork.Context.Set<TEntity>().FromSqlRaw(query, paramters).ToList();
        }
    }
}
