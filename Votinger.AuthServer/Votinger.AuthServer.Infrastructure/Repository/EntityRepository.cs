using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;
using Votinger.AuthServer.Infrastructure.Data;

namespace Votinger.AuthServer.Infrastructure.Repository
{
    public class EntityRepository<TEntity, TDbContext> : IRepository<TEntity> 
        where TEntity : BaseEntity
        where TDbContext : DbContext
    {
        private TDbContext _context;
        private DbSet<TEntity> _table;

        public EntityRepository(TDbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return _table.FirstOrDefault(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _table;
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _table;
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public virtual void Insert(TEntity entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual void InsertAll(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
            _context.SaveChanges();
        }

        public virtual async Task InsertAllAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public virtual void Update(TEntity entity)
        {
            _table.Update(entity);
            _context.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            _table.Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
