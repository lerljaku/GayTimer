using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GayTimer;
using GayTimer.Entities.Dao;
using GayTimer.ViewModels;
using GayTimer.Views;
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

            BindingContext = new MainMasterDetailPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        public class MainMasterDetailPageMasterViewModel : ScreenBase
        {
            public ObservableCollection<MainMasterDetailPageMenuItem> MenuItems { get; set; }
            
            public MainMasterDetailPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainMasterDetailPageMenuItem>(new[]
                {
                    new MainMasterDetailPageMenuItem { Id = 0, Title = "GAY HRA", Page = new Lazy<Page>(() => new GamePageView()) },
                    new MainMasterDetailPageMenuItem { Id = 1, Title = "GAYOVE", Page = new Lazy<Page>(() => new GayPageView(){BindingContext = new GayPageViewModel(new GayDao())}) },
                    new MainMasterDetailPageMenuItem { Id = 2, Title = "GAY RATE", Page = new Lazy<Page>(() => new StatisticsView()) },
                    new MainMasterDetailPageMenuItem { Id = 3, Title = "Settings", Page = new Lazy<Page>(() => new SettingsView()) },
                });
            }
        }
    }
}