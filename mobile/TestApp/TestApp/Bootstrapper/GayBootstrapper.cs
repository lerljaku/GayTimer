using System;
using Autofac;
using GayTimer.Installer;
using GayTimer.MenuItems;
using GayTimer.Services;
using GayTimer.ViewModels;
using GayTimer.Views;
using Xamarin.Forms;

namespace GayTimer.Bootstrapper
{
    public class GayBootStrapper
    {
        private IContainer m_container;

        public GayBootStrapper()
        {
            Init();
        }

        private void Init()
        {
            m_container = new GayInstaller().Install().Build();

            var loginService = m_container.Resolve<ILoginService>();

            var appContentService = m_container.Resolve<IAppContentService>();

            Page view;

            var logged = loginService.Login();
            if (logged)
            {
                view = new MainMasterDetailPage
                {
                    Master = new MainMasterDetailPageMaster
                    {
                        BindingContext = m_container.Resolve<MainMasterDetailPageMasterViewModel>()
                    }
                };
            }
            else
            {
                view = new LoginView {BindingContext = m_container.Resolve<LoginViewModel>()};
            }

            appContentService.SetContent(view);
        }
    }
}
