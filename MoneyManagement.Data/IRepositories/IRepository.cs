using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Data.IRepositories
{
    public interface IRepository<TEntity>
    {
        ValueTask<TEntity> InsertAsync(TEntity entity);
        ValueTask<TEntity> UpdateAsync(TEntity entity);
        ValueTask<bool> DeleteAsync(TEntity entity);
        ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> SelectAll(
          Expression<Func<TEntity, bool>> expression = null);
        ValueTask SaveAsync();
    }
}
