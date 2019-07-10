using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using GayTimer.Entities;
using GayTimer.Services;
using GayTimer.ViewModels;

namespace GayTimer.Installer
{
    public class GayInstaller
    {
        private static string DbPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3");

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
            builder.RegisterType<PlayerDecksViewModel>().SingleInstance();
            builder.RegisterType<DeckDetailViewModel>().SingleInstance();

            // services
            builder.RegisterType<SerializerProvider>().As<ISerializerProvider>().SingleInstance();
            builder.RegisterType<LoginService>().As<ILoginService>().WithParameter("userDataPath", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "userData.txt")).SingleInstance();
            builder.RegisterType<CurrentUser>().As<ICurrentUser>().SingleInstance();
            builder.RegisterType<AppContentService>().As<IAppContentService>().SingleInstance();
            builder.RegisterType<ErrorService>().As<IErrorService>().SingleInstance();
            builder.RegisterType<PlayerSelector>().As<ISelectPlayer>().SingleInstance();


            // dao
            builder.RegisterType<SqlDataService>().As<IDataService>().WithParameter("databasePath", DbPath).SingleInstance();

            return builder;
        }
    }
}
