using Autofac;
using SlimsteMens.Services.Interfaces;
using SlimsteMens.Services.Services;
using SlimsteMens.Services.SlimsteAdminService;

namespace SlimsteMens.Services
{
    public class RegisterComponents
    {
        /// <summary>
        /// Registers components in the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public void Register(ContainerBuilder container)
        {
            container.RegisterType<AdminServiceCallback>().As<IAdminServiceCallback>().SingleInstance();
            container.RegisterType<AdminServiceCallback>().As<ISlimsteServiceCallback>().SingleInstance();
            container.RegisterType<AdminService>().As<IAdminService>().SingleInstance();
        }
    }
}
