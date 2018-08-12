using Autofac;
using GayTimer.Installer;
using GayTimer.MenuItems;
using GayTimer.ViewModels;
using Xamarin.Forms;

namespace GayTimer.Bootstrapper
{
    public class GayBootStrapper
    {
        private IContainer m_gayInstaller;

        public GayBootStrapper()
        {
            Init();
        }

        private void Init()
        {
            m_gayInstaller = new GayInstaller().Install().Build();

            Application.Current.MainPage = new MainMasterDetailPage
            {
                Master = new MainMasterDetailPageMaster
                {
                    BindingContext = m_gayInstaller.Resolve<MainMasterDetailPageMasterViewModel>()
                }
            };
        }
    }
}
