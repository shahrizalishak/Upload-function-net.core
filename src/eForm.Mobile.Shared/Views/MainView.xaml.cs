using Xamarin.Forms;

namespace eForm.Views
{
    public partial class MainView : MasterDetailPage, IXamarinView
    {
        public MainView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
