using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GayTimer.MenuItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterDetailPageMaster : ContentPage
    {
        public ListView ListView;

        public MainMasterDetailPageMaster()
        {
            InitializeComponent();

            ListView = MenuItemsListView;
        }
    }
}