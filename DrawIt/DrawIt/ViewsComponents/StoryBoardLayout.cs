using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace DrawIt
{
    public class StoryBoardLayout : AbsoluteLayout
    {
        public int PageNumber;
        
        public IEnumerable Layers
        {
            get
            {
                return (IEnumerable)GetValue(LayersProperty);
            }
            set
            {
                SetValue(LayersProperty, value);
            }
        }

        public static readonly BindableProperty LayersProperty = 
        BindableProperty.Create(nameof(Layers), typeof(IEnumerable), typeof(StoryBoardLayout), null, propertyChanged: OnLayersChanged);
        
        public static void OnLayersChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                var layout = bindable as StoryBoardLayout;
                foreach (Layer layer in layout.Layers)
                {
                    bool found = false;
                    foreach (var child in layout.Children)
                    {
                        var childLayer = child as ImageLayer;
                        if (childLayer.ID == layer.ID)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        layout.DisableLayers();
                        ImageLayer newLayer = new ImageLayer(layer.ID);
                        layout.Children.Add(newLayer);
                        newLayer.SetInFocus(true);
                    }
                }
            }
        }

        public static readonly BindableProperty SelectedLayerProperty =
        BindableProperty.Create(nameof(SelectedLayer), typeof(Layer), typeof(StoryBoardLayout), null, propertyChanged: OnSelectedLayerChanged);

        public static void OnSelectedLayerChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                var layout = bindable as StoryBoardLayout;
                ImageLayer imgLayer = null;
                foreach (var child in layout.Children)
                {
                    var childLayer = child as ImageLayer;
                    if (childLayer.ID == layout.SelectedLayer.ID)
                    {
                        imgLayer = childLayer;
                        break;
                    }
                }

                if (imgLayer != null)
                {
                    layout.SetLayerInFocus(imgLayer);
                }
            }
        }

        public Layer SelectedLayer
        {
            get
            {
                return (Layer)GetValue(SelectedLayerProperty);
            }
            set
            {
                SetValue(SelectedLayerProperty, value);
            }
        }
        

        public StoryBoardLayout()
        {
            BackgroundColor = Color.White;
            PageNumber = 1;

            AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);
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
    }
}
