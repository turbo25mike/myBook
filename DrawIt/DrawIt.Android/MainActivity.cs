using Android.App;
using Android.OS;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin.Forms.Platform.Android;

namespace DrawIt
{
	[Activity (Label = "DrawIt.Android.Android", MainLauncher = true)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App(new AndroidInitializer()));
        }
    }
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

