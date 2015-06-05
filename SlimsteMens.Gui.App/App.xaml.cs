using System.Windows;
using Autofac;

namespace SlimsteMens.Gui.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="App" /> class.
        /// </summary>
        public App()
        {
            _container = IoCContainer.Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _container.Resolve<MainWindow>().Show();
            base.OnStartup(e);
        }
    }
}
