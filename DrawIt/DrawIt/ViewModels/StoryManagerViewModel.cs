using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Navigation;
using System.Text;
using Xamarin.Forms;

namespace DrawIt
{
    public class StoryManagerViewModel : BindableBase, INavigationAware
    {
        public Command ReadButtonClick { get; private set; }
        public Command DeleteButtonClick { get; private set; }
        public Command CreateButtonClick { get; private set; }

        private string _ReadStoryBtnText;
        public string ReadStoryBtnText
        {
            get
            {
                return _ReadStoryBtnText;
            }
            set
            {
                _ReadStoryBtnText = value;
                OnPropertyChanged(nameof(ReadStoryBtnText));
            }
        }

        private bool _ReadButtonVisible;
        public bool ReadButtonVisible
        {
            get
            {
                return _ReadButtonVisible;
            }
            set
            {
                _ReadButtonVisible = value;
                OnPropertyChanged(nameof(ReadButtonVisible));
            }
        }

        private bool _DeleteButtonVisible;
        public bool DeleteButtonVisible
        {
            get
            {
                return _DeleteButtonVisible;
            }
            set
            {
                _DeleteButtonVisible = value;
                OnPropertyChanged(nameof(DeleteButtonVisible));
            }
        }

        private Story _SelectedStory;
        public Story SelectedStory
        {
            get
            {
                return _SelectedStory;
            }
            set
            {
                if (value != null)
                {
                    _SelectedStory = value;
                    OnPropertyChanged(nameof(SelectedStory));
                    ReadButtonVisible = true;
                    DeleteButtonVisible = true;
                }
            }
        }

        private ObservableCollection<Story> _Stories = new ObservableCollection<Story>();
        public ObservableCollection<Story> Stories
        {
            get
            {
                return _Stories;
            }
            set
            {
                _Stories = value;
                OnPropertyChanged(nameof(Stories));
            }
        }

        public StoryManagerViewModel()
        {
            ReadButtonClick = new Command(() => ReadButtonAction());
            DeleteButtonClick = new Command(() => DeleteButtonAction());
            CreateButtonClick = new Command(() => CreateButtonAction());
            Stories.Add(new Story());
        }

        private void CreateButtonAction()
        {
            //App.Current.MainPage = new StoryBoardEditor(new Story());
        }

        private void ReadButtonAction()
        {
            //App.Current.MainPage = new StoryBoardEditor(SelectedStory);
        }

        private void DeleteButtonAction()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }
    }
}
