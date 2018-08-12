using System.Threading;
using System.Threading.Tasks;
using GayTimer.ViewModels;
using GayTimer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GayTimer.MenuItems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        public MainMasterDetailPage()
        {
            InitializeComponent();

            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMasterDetailPageMenuItem;
            if (item == null)
                return;

            var page = item.Page.Value;
            page.Title = item.Title;

            Detail = page;
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}