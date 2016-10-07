using Xamarin.Forms;

namespace DrawIt
{
    public partial class StoryBoardEditor : ContentPage
    {
        public StoryBoardEditor(Story selectedStory)
        {
            InitializeComponent();
            BindingContext = new StoryBoardEditorViewModel(selectedStory, 0);

            AbsoluteLayout.SetLayoutBounds(selectedStory.StoryBoards[0], new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(selectedStory.StoryBoards[0], AbsoluteLayoutFlags.All);
            StoryBoardContainer.Children.Add(selectedStory.StoryBoards[0]);
        }
    }
}
