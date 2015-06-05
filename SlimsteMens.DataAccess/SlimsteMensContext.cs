using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SlimsteMens.Model;
using SlimsteMens.Model.Entities;

namespace SlimsteMens.DataAccess
{
    public class SlimsteMensContext : DbContext, IUnitOfWork
    {
        #region DbSets (tables)
        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public DbSet<Gallery> Galleries { get; set; }

        /// <summary>
        /// Gets or sets the calendar items.
        /// </summary>
        /// <value>
        /// The calendar items.
        /// </value>
        public DbSet<Puzzle> Puzzles { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public DbSet<FinaleQuestion> FinaleQuestions { get; set; }

        /// <summary>
        /// Gets or sets the cities.
        /// </summary>
        /// <value>
        /// The cities.
        /// </value>
        public DbSet<VideoQuestion> VideoQuestions { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        public DbSet<ThreeSixNineQuestion> ThreeSixNineQuestions { get; set; }

        #endregion DbSets (tables)

        #region IUnitOfWork Members
        public IQueryable<T> AsQueryable<T>(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties) where T : Model.Entities.EntityBase
        {
            var query = CreateIncludeSet(includeProperties).AsQueryable();
            return query;
        }

        private DbSet<T> CreateIncludeSet<T>(IEnumerable<Expression<Func<T, object>>> includeProperties) where T : EntityBase
        {
            var set = Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    set.Include(includeProperty);
                }
            }

            return set;
        }

        public void Commit()
        {
            SaveChanges();
        }

        public void Add<T>(T entity) where T : Model.Entities.EntityBase
        {
            Set<T>().Add(entity);
        }

        public void Attach<T>(T entity) where T : Model.Entities.EntityBase
        {
            Set<T>().Add(entity);
        }

        public T RetrieveById<T>(long id) where T : Model.Entities.EntityBase
        {
            return Set<T>().FirstOrDefault(t => t.Id == id);
        }

        public bool Remove<T>(T entity) where T : Model.Entities.EntityBase
        {
            try
            {
                Set<T>().Remove(entity);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SlimsteMensContext"/> class.
        /// </summary>
        public SlimsteMensContext()
            : base("SlimsteMens")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChequeManagerContext, Migrations.Configuration>());   
            //Database.Initialize(false);
            Configuration.LazyLoadingEnabled = true;
        }

        #region DbContext Override Methods

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");
           
        }
        
        #endregion
    }
}
