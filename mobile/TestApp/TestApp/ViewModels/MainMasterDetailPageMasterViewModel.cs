using System;
using System.Collections.ObjectModel;
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

        public MainMasterDetailPageMasterViewModel(IComponentContext container, ICurrentUser currentUser)
        {
            m_currentUser = currentUser;

            MenuItems = new ObservableCollection<MainMasterDetailPageMenuItem>(new[]
            {
                new MainMasterDetailPageMenuItem { Id = 0, Title = "Nova", Page = new Lazy<Page>(() => new NewGamePageView() { BindingContext = container.Resolve<NewGameViewModel>() }) },
                new MainMasterDetailPageMenuItem { Id = 0, Title = "Aktualni", Page = new Lazy<Page>(() => new CurrentGamePageView()) },
                new MainMasterDetailPageMenuItem { Id = 1, Title = "GAYOVE", Page = new Lazy<Page>(() => new GayPageView() { BindingContext = container.Resolve<GayPageViewModel>() }) },
                new MainMasterDetailPageMenuItem { Id = 2, Title = "GAY RATE", Page = new Lazy<Page>(() => new StatisticsView()) },
                new MainMasterDetailPageMenuItem { Id = 3, Title = "Settings", Page = new Lazy<Page>(() => new SettingsView()) },
            });
        }

        public ObservableCollection<MainMasterDetailPageMenuItem> MenuItems { get; set; }

        public Gay User => m_currentUser.User;
    }
}