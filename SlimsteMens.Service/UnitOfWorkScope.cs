using Autofac;
using SlimsteMens.Model;
using System;

namespace SlimsteMens.Service
{
    /// <summary>
    /// Unit of work scope
    /// </summary>
    public sealed class UnitOfWorkScope : IUnitOfWorkScope
    {
        private readonly ILifetimeScope _lifetimeScope;
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether to [automatic commit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic commit]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoCommit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkScope" /> class.
        /// </summary>
        /// <param name="lifetimeScope">The lifetime scope.</param>
        /// <param name="autoCommit">if set to <c>true</c> [automatic commit].</param>
        /// <exception cref="System.ArgumentNullException">container</exception>
        public UnitOfWorkScope(ILifetimeScope lifetimeScope, bool autoCommit = true)
        {
            if (lifetimeScope == null) 
                throw new ArgumentNullException("lifetimeScope");

            _lifetimeScope = lifetimeScope;
            UnitOfWork = Resolve<IUnitOfWork>();
            AutoCommit = autoCommit;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return _lifetimeScope.Resolve<T>();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (AutoCommit)
            {
                UnitOfWork.Commit();
            }

            _lifetimeScope.Dispose();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="UnitOfWorkScope"/> is reclaimed by garbage collection.
        /// </summary>
        ~UnitOfWorkScope()
        {
            Dispose(false);
        }
    }
}
