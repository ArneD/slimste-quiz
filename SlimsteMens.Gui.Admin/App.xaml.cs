using System.Windows;
using Autofac;
using SlimsteMens.Services.Interfaces;

namespace SlimsteMens.Gui.Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private ServiceHost _host;
        private IContainer _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            //_host = new ServiceHost(typeof(Service.SlimsteService), new Uri("net.tcp://localhost:7005/SlimsteService/"));
            //_host.AddServiceEndpoint(typeof(Service.ISlimsteService), new NetTcpBinding(), "");
            //_host.OpenTimeout = TimeSpan.FromMinutes(5);
            //_host.CloseTimeout = TimeSpan.FromMinutes(5);
            //_host.Open();

            _container = IoCContainer.Build();
          
            _container.Resolve<MainWindow>().Show();
        
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Resolve<IAdminService>().Dispose();

            //_host.Close();

            base.OnExit(e);
        }
    }
}
