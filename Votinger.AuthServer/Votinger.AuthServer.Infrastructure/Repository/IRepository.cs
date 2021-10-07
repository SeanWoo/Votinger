using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Votinger.AuthServer.Core.Entities;

namespace Votinger.AuthServer.Infrastructure.Repository
{
    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="TEntity">BaseEntity</typeparam>
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);
        /// <summary>
        /// Get entity by asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
        /// <summary>
        /// Get all entities asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        /// <summary>
        /// Insert entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity);
        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities"></param>
        void InsertAll(IEnumerable<TEntity> entities);
        /// <summary>
        /// Insert entities asynchronously
        /// </summary>
        /// <param name="entities"></param>
        Task InsertAllAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(TEntity entity);
        /// <summary>
        /// Update entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// Delete entity asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
    }
}
