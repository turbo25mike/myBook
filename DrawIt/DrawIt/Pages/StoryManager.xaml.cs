using Xamarin.Forms;

namespace DrawIt
{
    public partial class StoryManager : ContentPage
    {
        public StoryManager()
        {
            InitializeComponent();
            BindingContext = new StoryManagerViewModel();
        }
    }
}
