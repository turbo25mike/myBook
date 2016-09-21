using Xamarin.Forms;

namespace DrawIt
{
	public class ImageWithTouch : Image 
	{
		public static readonly BindableProperty InFocusProperty =
            BindableProperty.Create((ImageWithTouch w) => w.InFocus, false);

        public bool InFocus
        {
            get
            {
                return (bool)GetValue(InFocusProperty);
            }
            set
            {
                SetValue(InFocusProperty, value);
            }
        }
    }
}

