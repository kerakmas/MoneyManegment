using Microsoft.EntityFrameworkCore;
using MoneyManagement.Data.Contexts;
using MoneyManagement.Data.IRepositories;
using MoneyManagement.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Data.Repositories
{
    public class Repository<TEintity> : IRepository<TEintity> where TEintity : Auditable
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<TEintity> dbset;
        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbset = dbContext.Set<TEintity>();
        }

        public async ValueTask<bool> DeleteAsync(TEintity entity)
        {
           var model = await this.dbset.FirstOrDefaultAsync(x => x.Id == entity.Id);
           if(model is null) return false;
            model.IsDeleted = true;
            return true;
        }

        public async ValueTask<TEintity> InsertAsync(TEintity entity)
        {
           var model =  await this.dbset.AddAsync(entity);
            return model.Entity;
        }

        public async ValueTask SaveAsync() => await this.dbContext.SaveChangesAsync();

        public IQueryable<TEintity> SelectAll(Expression<Func<TEintity, bool>> expression = null)
        {
            IQueryable<TEintity> query = this.dbset;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return query;
        }

        public async ValueTask<TEintity> SelectAsync(Expression<Func<TEintity, bool>> expression)
        {
            return await this.SelectAll(expression).FirstOrDefaultAsync();
        }

        public async ValueTask<TEintity> UpdateAsync(TEintity entity) =>
            this.dbset.Update(entity).Entity;
    }
}
