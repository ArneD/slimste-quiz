using Autofac;
using SlimsteMens.DataAccess.Repositories;
using SlimsteMens.Model;
using SlimsteMens.Model.Repositories;

namespace SlimsteMens.DataAccess
{
    public class RegisterComponents
    {
        /// <summary>
        /// Registers components in the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public void Register(ContainerBuilder container)
        {
            container.RegisterType<SlimsteMensContext>().As<IUnitOfWork>().InstancePerLifetimeScope();
            container.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>));
        }
    }
}
