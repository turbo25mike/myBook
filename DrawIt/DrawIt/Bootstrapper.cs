using Xamarin.Forms;
using Autofac;

namespace DrawIt
{
    public class Bootstrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AppModule>();
            var container = builder.Build();

            var page = container.Resolve<StoryManagerView>();
            App.Current.MainPage = new NavigationPage(page);
        }
    }
}
