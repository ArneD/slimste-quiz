using System;

namespace SlimsteMens.Model
{
    /// <summary>
    /// Interface of a unit of work scope
    /// </summary>
    public interface IUnitOfWorkScope : IDisposable
    {
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Gets or sets a value indicating whether [automatic commit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic commit]; otherwise, <c>false</c>.
        /// </value>
        bool AutoCommit { get; set; }
    }
}
