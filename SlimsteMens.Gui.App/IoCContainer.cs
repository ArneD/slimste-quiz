using Autofac;
using SlimsteMens.Gui.App.ViewModels;

namespace SlimsteMens.Gui.App
{
    /// <summary>
    /// IoC Container
    /// </summary>
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

                new SlimsteMens.Services.RegisterComponents().Register(builder);

                builder.RegisterType<MainWindowViewModel>().As<MainWindowViewModel>().SingleInstance();
                builder.RegisterType<StartViewModel>().As<StartViewModel>().SingleInstance();
                builder.RegisterType<ThreeSixNineViewModel>().As<ThreeSixNineViewModel>().SingleInstance();
                builder.RegisterType<PuzzleViewModel>().As<PuzzleViewModel>().SingleInstance();
                builder.RegisterType<ImageViewModel>().As<ImageViewModel>().SingleInstance();
                builder.RegisterType<GalleryViewModel>().As<GalleryViewModel>().SingleInstance();
                builder.RegisterType<VideoViewModel>().As<VideoViewModel>().SingleInstance();
                builder.RegisterType<FinaleViewModel>().As<FinaleViewModel>().SingleInstance();

                builder.RegisterType<MainWindow>().As<MainWindow>().SingleInstance();
                _container = builder.Build();
            }

            return _container;
        }
    }
}
