using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace DrawIt
{
    public class StoryBoardEditorViewModel : ViewModelBase
    {
        private KarokeMachine AudioPlayer;
        private StoryBoard CurrentStoryBoard;

        private KeyValuePair<string, string> _SelectedColor;
        public KeyValuePair<string, string> SelectedColor
        {
            get
            {
                return _SelectedColor;
            }
            set
            {
                if (!_SelectedColor.Equals(value))
                {
                    _SelectedColor = value;
                    OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }

        private string _PlayStopBtnText;
        public string PlayStopBtnText
        {
            get
            {
                return _PlayStopBtnText;
            }
            set
            {
                if (!_PlayStopBtnText.Equals(value))
                {
                    _PlayStopBtnText = value;
                    OnPropertyChanged(nameof(PlayStopBtnText));
                }
            }
        }


        private bool _RecordIsEnabled;
        public bool RecordIsEnabled
        {
            get
            {
                return _RecordIsEnabled;
            }
            set
            {
                if (!_RecordIsEnabled.Equals(value))
                {
                    _RecordIsEnabled = value;
                    OnPropertyChanged(nameof(RecordIsEnabled));
                }
            }
        }

        private void PlayStopAction()
        {
            if (AudioPlayer.IsStopped)
            {
                RecordIsEnabled = false;
                AudioPlayer.Play();
                PlayStopBtnText = "Stop";
            }
            else
            {
                RecordIsEnabled = true;
                AudioPlayer.Stop();
                PlayStopBtnText = "Play";
            }
        }

        private void RecordAction()
        {
            if (AudioPlayer.IsStopped)
            {
                PlayStopBtnText = "Stop";
                RecordIsEnabled = false;
                AudioPlayer.Record();
            }
        }


        private bool _ColorPalletteIsVisible;
        public bool ColorPalletteIsVisible
        {
            get
            {
                return _ColorPalletteIsVisible;
            }
            set
            {
                if (!_ColorPalletteIsVisible.Equals(value))
                {
                    _ColorPalletteIsVisible = value;
                    OnPropertyChanged(nameof(ColorPalletteIsVisible));
                }
            }
        }

        public Command PlayStopClick { get; private set; }
        public Command RecordClick { get; private set; }
        public Command ColorClick { get; private set; }
        public Command BrushClick { get; private set; }
        public Command BucketClick { get; private set; }
        public Command PenClick { get; private set; }
        public Command AddLayerClick { get; private set; }

        public StoryBoardEditorViewModel(Story selectedStory)
        {
            AudioPlayer = new KarokeMachine(selectedStory.ID, CurrentStoryBoard.PageNumber);
            AudioPlayer.Stopped += (s,e) => {
                RecordIsEnabled = true;
                PlayStopBtnText = "Play";
            };

            SelectedColor = new KeyValuePair<string, string>("Black", "FF000000");
            CurrentStoryBoard = selectedStory.StoryBoards[0];

            PlayStopClick = new Command(() => PlayStopAction());
            RecordClick = new Command(() => RecordAction());
            ColorClick = new Command(() => { ColorPalletteIsVisible = (!ColorPalletteIsVisible); });
            BrushClick = new Command(() => { ToolManager.Instance.Tool = ToolType.Brush; });
            BucketClick = new Command(() => { ToolManager.Instance.Tool = ToolType.Bucket; });
            PenClick = new Command(() => { ToolManager.Instance.Tool = ToolType.Pen; });
            AddLayerClick = new Command(() => { CurrentStoryBoard.AddLayer(); });
            
            AbsoluteLayout.SetLayoutBounds(CurrentStoryBoard, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(CurrentStoryBoard, AbsoluteLayoutFlags.All);
            Layers.Children.Add(CurrentStoryBoard);


            LayersViewer.ItemSelected += LayersViewer_ItemSelected;

            //BrushSlider.ValueChanged += (s, e) => { ToolManager.Instance.BrushSize = e.NewValue; };
            //AlphaSlider.ValueChanged += (s, e) => { ToolManager.Instance.Alpha = (int)e.NewValue; };
            //BlurSlider.ValueChanged += (s, e) => { ToolManager.Instance.BlurRadius = e.NewValue; };
            ColorPallette.ItemSelected += (s, e) => {
                ToolManager.Instance.ForegroundColor = ((KeyValuePair<string, string>)e.SelectedItem).Value;
                SelectedColor = (KeyValuePair<string, string>)e.SelectedItem;
                ColorPallette.IsVisible = false;
            };
        }


        private void LayersViewer_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ImageLayer currentLayer = e.SelectedItem as ImageLayer;
                CurrentStoryBoard.SetLayerInFocus(currentLayer);
            }
        }
    }
}
