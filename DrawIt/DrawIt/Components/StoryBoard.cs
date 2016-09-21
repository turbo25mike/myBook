using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace DrawIt
{
    public class StoryBoard : AbsoluteLayout
    {
        public int PageNumber;

        public StoryBoard(int pageNumber = 1)
        {
            BackgroundColor = Color.White;
            


            PageNumber = pageNumber;
            AddLayer();
        }

        private void DisableLayers()
        {
            foreach (ImageLayer layer in Children)
            {
                layer.SetInFocus(false);
            }
        }

        public void SetLayerInFocus(ImageLayer layer)
        {
            DisableLayers();
            layer.SetInFocus(true);
        }

        public ImageLayer AddLayer()
        {
            DisableLayers();
            ImageLayer newLayer = new ImageLayer("Layer " + (Children.Count + 1));
            Children.Add(newLayer);
            newLayer.SetInFocus(true);
            return newLayer;

        }
    }
}
