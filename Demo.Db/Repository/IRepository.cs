using Demo.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Db.Repository
{
    public interface IRepository<TEntity> where TEntity : ModelEntity
    {
        public IQueryable<TEntity> Entity { get; }
        public IQueryable<TEntity> NonDeletedEntity { get; }
        void Delete(TEntity entityToDelete);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAsync(Guid? id);
        Task<IEnumerable<TEntity>> GetWithRawSqlAsync(string query, params object[] parameters);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entityToUpdate);
        Task SaveAsync();
    }
}
