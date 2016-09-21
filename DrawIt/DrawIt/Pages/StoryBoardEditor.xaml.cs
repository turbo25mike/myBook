using Xamarin.Forms;

namespace DrawIt
{
    public partial class StoryBoardEditor : ContentPage
    {
        public StoryBoardEditor(Story selectedStory)
        {
            InitializeComponent();
            BindingContext = new StoryBoardEditorViewModel(selectedStory);
        }
    }
}
