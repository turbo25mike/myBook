using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Prism.Mvvm;

namespace DrawIt
{
    public class Story : BindableBase
    {
        private string _Name;
        public string Name {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private Guid _ID;
        public Guid ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public ObservableCollection<StoryBoard> StoryBoards;

        public Story()
        {
            Name = "New Story";
            ID = Guid.NewGuid();
            StoryBoards = new ObservableCollection<StoryBoard>();
            StoryBoards.Add(new StoryBoard());
        }
    }
}
