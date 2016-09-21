using System.Drawing;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using DrawIt;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ImageWithTouch), typeof(ImageWithTouchRenderer))]
namespace DrawIt
{
    public class ImageWithTouchRenderer : ViewRenderer<ImageWithTouch, DrawView> 
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ImageWithTouch> e)
        {
            base.OnElementChanged(e);

            SetNativeControl(new DrawView(RectangleF.Empty));
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ImageWithTouch.CurrentLineColorProperty.PropertyName)
            {
                Control.CurrentLineColor = Element.CurrentLineColor.ToUIColor();
            }

            if (e.PropertyName == ImageWithTouch.CurrentLineColorProperty.PropertyName)
            {
                Control.PenWidth = Element.CurrentLineWidth;
            }
        }
    }
}

