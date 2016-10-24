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
            NavigationService.NavigateAsync("StoryManagerView");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<StoryManagerView>();
            Container.RegisterTypeForNavigation<StoryBoardEditorView>();
        }
    }
}
