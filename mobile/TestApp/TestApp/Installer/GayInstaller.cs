using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using GayTimer.Entities.Dao;
using GayTimer.Services;
using GayTimer.ViewModels;

namespace GayTimer.Installer
{
    public class GayInstaller
    {
        public ContainerBuilder Install()
        {
            var builder = new ContainerBuilder();

            // vm
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<GayPageViewModel>();
            builder.RegisterType<GamePageViewModel>();
            builder.RegisterType<NewGameViewModel>();
            builder.RegisterType<MainMasterDetailPageMasterViewModel>();

            // services
            builder.RegisterType<SerializerProvider>().As<ISerializerProvider>();
            builder.RegisterType<LoginService>().As<ILoginService>().WithParameter("userDataPath", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            builder.RegisterType<CurrentUser>().As<ICurrentUser>();
            builder.RegisterType<AppContentService>().As<IAppContentService>();

            // dao
            builder.RegisterType<GayDao>().WithParameter("connectionString", "http://192.168.0.103:8080/api");

            return builder;
        }
    }
}
