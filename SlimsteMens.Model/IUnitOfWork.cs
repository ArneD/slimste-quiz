using System;
using System.Linq;
using System.Linq.Expressions;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.Model
{
    /// <summary>
    /// Interface of unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Expose the entity set as queryable including the included properties.
        /// </summary>
        /// <typeparam name="T">Type of the entity</typeparam>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        IQueryable<T> AsQueryable<T>(params Expression<Func<T, object>>[] includeProperties) where T : EntityBase;

        /// <summary>
        /// Commits changes in this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Add<T>(T entity) where T : EntityBase;

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        void Attach<T>(T entity) where T : EntityBase;

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T RetrieveById<T>(long id) where T : EntityBase;

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Remove<T>(T entity) where T : EntityBase;
    }
}
