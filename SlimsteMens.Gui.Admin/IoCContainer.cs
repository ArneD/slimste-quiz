using Autofac;
using SlimsteMens.Gui.Admin.ViewModels;
using SlimsteMens.Gui.Admin.Views;
using SlimsteMens.Gui.Controls.ViewModels;

namespace SlimsteMens.Gui.Admin
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

                new Services.RegisterComponents().Register(builder);

                builder.RegisterType<MainWindowViewModel>().As<MainWindowViewModel>().SingleInstance();
                builder.RegisterType<ConfigTimersViewModel>().As<ConfigTimersViewModel>().SingleInstance();
                builder.RegisterType<StartViewModel>().As<StartViewModel>().SingleInstance();
                builder.RegisterType<ThreeSixNineRoundViewModel>().As<ThreeSixNineRoundViewModel>().SingleInstance();
                builder.RegisterType<PuzzleRoundViewModel>().As<PuzzleRoundViewModel>().SingleInstance();
                builder.RegisterType<GalleryRoundViewModel>().As<GalleryRoundViewModel>().SingleInstance();
                builder.RegisterType<VideoRoundViewModel>().As<VideoRoundViewModel>().SingleInstance();
                builder.RegisterType<TieBreakerQuestionViewModel>().As<TieBreakerQuestionViewModel>().SingleInstance();
                builder.RegisterType<FinaleRoundViewModel>().As<FinaleRoundViewModel>().SingleInstance();
                builder.RegisterType<ScoreControlViewModel>().As<ScoreControlViewModel>().SingleInstance();

                builder.RegisterType<MainWindow>().As<MainWindow>().SingleInstance();
                builder.RegisterType<ConfigTimersWindow>().As<ConfigTimersWindow>().SingleInstance();
                _container = builder.Build();
            }

            return _container;
        }
    }
}
