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
        public Guid ID;
        
        public ImageLayer(Guid id)
        {
            ID = id;
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
            DrawingImage.InFocus = focused;
        }
    }
}
