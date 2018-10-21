using System;
using System.Collections.ObjectModel;
using System.Linq;
using Autofac;
using GayTimer.Entities;
using GayTimer.Services;
using GayTimer.Views;
using Xamarin.Forms;

namespace GayTimer.ViewModels
{
    public class MainMasterDetailPageMasterViewModel : ScreenBase
    {
        private readonly ICurrentUser m_currentUser;

        public MainMasterDetailPageMasterViewModel(IComponentContext container, ICurrentUser currentUser, GamePageViewModel gamePageVm)
        {
            m_currentUser = currentUser;

            MenuItems = new ObservableCollection<MainMasterDetailPageMenuItem>(new[]
            {
                new MainMasterDetailPageMenuItem { Id = 0, Title = "Nova", Page = new Lazy<Page>(() => new NewGamePageView() { BindingContext = container.Resolve<NewGameViewModel>() }) },
                new MainMasterDetailPageMenuItem { Id = 0, Title = "Aktualni", Page = new Lazy<Page>(() => new GamePageView(){BindingContext = gamePageVm}) },
                new MainMasterDetailPageMenuItem { Id = 1, Title = "GAYOVE", Page = new Lazy<Page>(() => new GayPageView() { BindingContext = container.Resolve<GayPageViewModel>() }) },
                new MainMasterDetailPageMenuItem { Id = 2, Title = "GAY RATE", Page = new Lazy<Page>(() => new StatisticsView()) },
                new MainMasterDetailPageMenuItem { Id = 3, Title = "Settings", Page = new Lazy<Page>(() => new SettingsView()) },
            });
        }

        public ObservableCollection<MainMasterDetailPageMenuItem> MenuItems { get; set; }

        private MainMasterDetailPageMenuItem m_selectedItem;
        public MainMasterDetailPageMenuItem SelectedItem
        {
            get => m_selectedItem;
            set
            {
                m_selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        public Gay User => m_currentUser.User;

        public void Activate(ScreenBase vm)
        {
            var item = MenuItems.First(d => Equals(d.Page.Value.BindingContext, vm));

            SelectedItem = item;
        }
    }
}