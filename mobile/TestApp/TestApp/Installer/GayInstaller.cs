using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using GayTimer.Entities.Dao;
using GayTimer.ViewModels;

namespace GayTimer.Installer
{
    public class GayInstaller
    {
        public ContainerBuilder Install()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GayPageViewModel>();
            builder.RegisterType<GamePageViewModel>();
            builder.RegisterType<NewGameViewModel>();
            builder.RegisterType<MainMasterDetailPageMasterViewModel>();

            builder.RegisterType<GayDao>().WithParameter("connectionString", "http://localhost:8080/api");

            return builder;
        }
    }
}
