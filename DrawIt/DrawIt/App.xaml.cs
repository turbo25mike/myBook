using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms;

namespace DrawIt
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            //NavigationService.NavigateAsync("StoryManagerView");

            NavigationService.NavigateAsync("TestView?title=Hello%20from%20Xamarin.Forms");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<TestView>();
            Container.RegisterTypeForNavigation<StoryManagerView>();
        }
    }
}
