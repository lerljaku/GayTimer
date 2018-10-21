using System;
using System.Collections.Generic;
using System.IO;
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
            builder.RegisterType<GamePageViewModel>().SingleInstance();
            builder.RegisterType<NewGameViewModel>();
            builder.RegisterType<MainMasterDetailPageMasterViewModel>().SingleInstance();

            // services
            builder.RegisterType<SerializerProvider>().As<ISerializerProvider>().SingleInstance();
            builder.RegisterType<LoginService>().As<ILoginService>().WithParameter("userDataPath", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "userData.txt")).SingleInstance();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>().SingleInstance();
            builder.RegisterType<AppContentService>().As<IAppContentService>().SingleInstance();
            builder.RegisterType<ErrorService>().As<IErrorService>().SingleInstance();

            // dao
            builder.RegisterType<GayDao>().WithParameter("connectionString", "http://192.168.0.103:8080/api");

            return builder;
        }
    }
}
