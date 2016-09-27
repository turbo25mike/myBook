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
        public StoryBoardEditorViewModel(Story selectedStory, int storyBoardID)
        {
            CurrentStoryBoard = selectedStory.StoryBoards[storyBoardID];

            _AudioPlayer = new KarokeMachine(selectedStory.ID, CurrentStoryBoard.PageNumber);
            _AudioPlayer.Stopped += (s, e) => {
                RecordIsEnabled = true;
                PlayStopBtnText = "Play";
            };

            PlayStopClick = new Command(() => PlayStopAction());
            RecordClick = new Command(() => RecordAction());
            ColorClick = new Command(() => { ColorPalletteIsVisible = (!ColorPalletteIsVisible); });
            BrushClick = new Command(() => { ToolManager.Instance.Tool = ToolType.Brush; });
            BucketClick = new Command(() => { ToolManager.Instance.Tool = ToolType.Bucket; });
            PenClick = new Command(() => { ToolManager.Instance.Tool = ToolType.Pen; });
            AddLayerClick = new Command(() => { CurrentStoryBoard.AddLayer(); });
            
            //BrushSlider.ValueChanged += (s, e) => { ToolManager.Instance.BrushSize = e.NewValue; };
            //AlphaSlider.ValueChanged += (s, e) => { ToolManager.Instance.Alpha = (int)e.NewValue; };
            //BlurSlider.ValueChanged += (s, e) => { ToolManager.Instance.BlurRadius = e.NewValue; };
        }

        private void PlayStopAction()
        {
            if (_AudioPlayer.IsStopped)
            {
                RecordIsEnabled = false;
                _AudioPlayer.Play();
                PlayStopBtnText = "Stop";
            }
            else
            {
                RecordIsEnabled = true;
                _AudioPlayer.Stop();
                PlayStopBtnText = "Play";
            }
        }

        private void RecordAction()
        {
            if (_AudioPlayer.IsStopped)
            {
                PlayStopBtnText = "Stop";
                RecordIsEnabled = false;
                _AudioPlayer.Record();
            }
        }


        private void LayersViewer_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ImageLayer currentLayer = e.SelectedItem as ImageLayer;
                CurrentStoryBoard.SetLayerInFocus(currentLayer);
            }
        }

        #region Members

        private KarokeMachine _AudioPlayer;

        public Command PlayStopClick { get; private set; }
        public Command RecordClick { get; private set; }
        public Command ColorClick { get; private set; }
        public Command BrushClick { get; private set; }
        public Command BucketClick { get; private set; }
        public Command PenClick { get; private set; }
        public Command AddLayerClick { get; private set; }


        private StoryBoard _CurrentStoryBoard;
        public StoryBoard CurrentStoryBoard
        {
            get
            {
                return _CurrentStoryBoard;
            }
            set
            {
                if (!_CurrentStoryBoard.Equals(value))
                {
                    _CurrentStoryBoard = value;
                    OnPropertyChanged(nameof(CurrentStoryBoard));
                }
            }
        }



        private KeyValuePair<string, string> _SelectedColor = new KeyValuePair<string, string>("Black", "FF000000");
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
                    ToolManager.Instance.ForegroundColor = value.Value;
                    _SelectedColor = value;
                    ColorPalletteIsVisible = false;
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
        #endregion
    }
}
