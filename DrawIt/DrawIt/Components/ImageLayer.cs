using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrawIt
{
    public class ImageLayer : Frame
    {
        private ImageWithTouch DrawingImage;
        public string Name;

        public static readonly BindableProperty StatusProperty =
            BindableProperty.Create((ImageLayer w) => w.Status, "");

        public string Status
        {
            get
            {
                return (string)GetValue(StatusProperty);
            }
            set
            {
                SetValue(StatusProperty, value);
            }
        }

        public ImageLayer(string name)
        {
            Name = name;
            Status = Name + ": Active";
            DrawingImage = new ImageWithTouch
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent
            };

            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0f, 0f, 1f, 1f));

            Padding = 5;
            HasShadow = false;
            OutlineColor = Color.Black;
            Content = DrawingImage;
        }

        public void SetInFocus(bool focused)
        {
            Status = (focused) ? "A" : "";
            DrawingImage.InFocus = focused;
        }
    }
}
