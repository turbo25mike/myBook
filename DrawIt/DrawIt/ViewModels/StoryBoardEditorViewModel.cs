using System;
using System.Collections.Generic;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace DrawIt
{
    public class StoryBoardEditorViewModel : BindableBase, INavigationAware
    {
        public StoryBoardEditorViewModel()
        {
            PlayStopClick = new Command(PlayStopAction);
            RecordClick = new Command(RecordAction);
            ColorClick = new Command(() => { ColorPalletteIsVisible = (!ColorPalletteIsVisible); });
            ToolClick = new Command(SetTool);
            AddLayerClick = new Command(() => { CurrentStoryBoard.AddLayer(); });
            HideBrushSettings = new Command(() => { BrushSettingsIsVisible = false; });
        }


        private void SetTool(object toolName)
        {
            string tool = toolName as string;
            ToolType selectedTool = ToolType.Brush;
            switch (tool)
            {
                case "Brush":
                    selectedTool = ToolType.Brush;
                    BrushSettingsIsVisible = !BrushSettingsIsVisible;
                    break;
                case "Bucket":
                    selectedTool = ToolType.Bucket;
                    BrushSettingsIsVisible = false;
                    break;
                case "Pen":
                    selectedTool = ToolType.Pen;
                    BrushSettingsIsVisible = !BrushSettingsIsVisible;
                    break;
            }

            ToolManager.Instance.Tool = selectedTool;
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
                Layer currentLayer = e.SelectedItem as Layer;
                CurrentStoryBoard.SetLayerInFocus(currentLayer);
            }
        }


        private KarokeMachine _AudioPlayer;

        public Command PlayStopClick { get; private set; }
        public Command RecordClick { get; private set; }
        public Command ColorClick { get; private set; }
        public Command ToolClick { get; private set; }
        public Command AddLayerClick { get; private set; }
        public Command HideBrushSettings { get; private set; }



        private StoryBoard _CurrentStoryBoard;
        public StoryBoard CurrentStoryBoard
        {
            get
            {
                return _CurrentStoryBoard;
            }
            set
            {
                if (_CurrentStoryBoard == null || !_CurrentStoryBoard.Equals(value))
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
                if (value.Value != null && !_SelectedColor.Equals(value))
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



        private bool _BrushSettingsIsVisible;
        public bool BrushSettingsIsVisible
        {
            get
            {
                return _BrushSettingsIsVisible;
            }
            set
            {
                if (!_BrushSettingsIsVisible.Equals(value))
                {
                    _BrushSettingsIsVisible = value;
                    OnPropertyChanged(nameof(BrushSettingsIsVisible));
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var nextStory = parameters["SelectedStory"] as Story;
            if (nextStory == null)
                return;
            if (_SelectedStory == null || _SelectedStory.ID != nextStory.ID)
            {
                _SelectedStory = nextStory;
                CurrentStoryBoard = _SelectedStory.StoryBoards[_SelectedStoryBoardID];
                _AudioPlayer = new KarokeMachine(_SelectedStory.ID, CurrentStoryBoard.PageNumber);
                _AudioPlayer.Stopped += AudioPlayerStopped;
            }
        }

        private void AudioPlayerStopped(object sender, EventArgs e)
        {
            RecordIsEnabled = true;
            PlayStopBtnText = "Play";
        }

        private int _SelectedStoryBoardID = 0;
        private Story _SelectedStory;
    }
}
