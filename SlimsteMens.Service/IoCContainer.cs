using Autofac;
using SlimsteMens.DataAccess;
using SlimsteMens.Model;

namespace SlimsteMens.Service
{
    public static class IoCContainer
    {
        /// <summary>
        /// Gets the base container.
        /// </summary>
        private static IContainer _container;

        /// <summary>
        /// Builds this instance.
        /// </summary>
        public static IContainer Build()
        {
            if (_container == null)
            {
                var builder = new ContainerBuilder();

                new RegisterComponents().Register(builder);
                builder.RegisterType<UnitOfWorkScope>().As<IUnitOfWorkScope>();
                _container = builder.Build();
            }

            return _container;
        }
    }
}
