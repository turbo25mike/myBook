using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace DrawIt
{
   public class StoryBoard : BindableBase
    {
        public int PageNumber { get; set; }

        private ObservableCollection<Layer> _Layers;
        public ObservableCollection<Layer> Layers
        {
            get
            {
                return _Layers;
            }
            set
            {
                if (_Layers == null || !_Layers.Equals(value))
                {
                    _Layers = value;
                    OnPropertyChanged(nameof(Layers));
                }
            }
        }

        private Layer _SelectedLayer;
        public Layer SelectedLayer
        {
            get
            {
                return _SelectedLayer;
            }
            set
            {
                if (_SelectedLayer == null || !_SelectedLayer.Equals(value))
                {
                    _SelectedLayer = value;
                    OnPropertyChanged(nameof(SelectedLayer));
                }
            }
        }

        public StoryBoard()
        {
            Layers = new ObservableCollection<Layer> {new Layer()};
        }
    }
}
