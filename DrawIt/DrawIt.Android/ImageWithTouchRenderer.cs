using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using DrawIt;

[assembly: ExportRenderer(typeof(ImageWithTouch), typeof(ImageWithTouchRenderer))]

namespace DrawIt
{
    public class ImageWithTouchRenderer : ViewRenderer<ImageWithTouch, DrawView> 
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ImageWithTouch> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                SetNativeControl(new DrawView(Context));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
            if (e.PropertyName == ImageWithTouch.InFocusProperty.PropertyName)
            {
                Control.InFocus = Element.InFocus;
            }
        }
    }
}

