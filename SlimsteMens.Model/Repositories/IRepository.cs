using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model.Repositories
{
    /// <summary>
    /// Interface for the repositories
    /// </summary>
    /// <typeparam name="T">Type of the repository</typeparam>
    public interface IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Expose the entity set as queryable including the included properties.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IQueryable<T> AsQueryable(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Gets the entity specified by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T RetrieveById(long id);

        /// <summary>
        /// Finds the specified function.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        IEnumerable<T> Query(Func<T, bool> func);
    }
}
