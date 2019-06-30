using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
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
            builder.RegisterType<PlayerListViewModel>().SingleInstance();
            builder.RegisterType<GamePageViewModel>().SingleInstance();
            builder.RegisterType<NewGameViewModel>().SingleInstance();
            builder.RegisterType<MainPageViewModel>().SingleInstance();
            builder.RegisterType<PlayerDetailViewModel>().SingleInstance();
            builder.RegisterType<SelectLifeTotalViewModel>().SingleInstance();
            builder.RegisterType<SelectPlayerCountViewModel>().SingleInstance();
            builder.RegisterType<GameHistoryViewModel>().SingleInstance();
            builder.RegisterType<GameSummaryViewModel>().SingleInstance();

            // services
            builder.RegisterType<SerializerProvider>().As<ISerializerProvider>().SingleInstance();
            builder.RegisterType<LoginService>().As<ILoginService>().WithParameter("userDataPath", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "userData.txt")).SingleInstance();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>().SingleInstance();
            builder.RegisterType<AppContentService>().As<IAppContentService>().SingleInstance();
            builder.RegisterType<ErrorService>().As<IErrorService>().SingleInstance();

            // dao
            builder.RegisterType<DataService>().As<IDataService>().SingleInstance();

            return builder;
        }
    }
}
