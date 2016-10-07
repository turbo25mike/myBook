using Xamarin.Forms;

namespace DrawIt
{
    public partial class StoryManagerView : ContentPage
    {
        public StoryManagerView(StoryManagerViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
