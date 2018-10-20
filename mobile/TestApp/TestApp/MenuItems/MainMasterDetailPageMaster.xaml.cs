using GayTimer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GayTimer.MenuItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterDetailPageMaster : ContentPage
    {
        public MainMasterDetailPageMaster()
        {
            InitializeComponent();

            ListView = MenuItemsListView;
        }

        public ListView ListView { get; private set; }
    }
}