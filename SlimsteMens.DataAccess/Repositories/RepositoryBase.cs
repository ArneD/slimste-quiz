using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SlimsteMens.Model;
using SlimsteMens.Model.Entities;
using SlimsteMens.Model.Repositories;

namespace SlimsteMens.DataAccess.Repositories
{
    /// <summary>
    /// Generic repository base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        public IUnitOfWork Context { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="dbContext">The unit of work context.</param>
        public RepositoryBase(IUnitOfWork dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            Context = dbContext;
        }

        /// <summary>
        /// Expose the entity set as queryable including the included properties.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public IQueryable<T> AsQueryable(params Expression<Func<T, object>>[] includeProperties)
        {
            return Context.AsQueryable(includeProperties);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Insert(T entity)
        {
            Context.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            Context.Attach(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public T RetrieveById(long id)
        {
            return Context.RetrieveById<T>(id);
        }

        /// <summary>
        /// Finds the specified Entity.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(Func<T, bool> func)
        {
            return AsQueryable().Where(func);
        }
    }
}
