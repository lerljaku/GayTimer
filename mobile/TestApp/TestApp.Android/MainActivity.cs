using System;

using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Autofac;
using GayTimer;
using GayTimer.Controls;
using GayTimer.Installer;
using GayTimer.ViewModels;
using GayTimer.Views;
using Xamarin.Forms;
using Color = Android.Graphics.Color;
using Orientation = Android.Content.Res.Orientation;

namespace TestApp.Droid
{
    [Activity(Label = "TestApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            SetStatusBarColor(new Color(8, 127, 35));

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            XF.Material.Droid.Material.Init(this, bundle);

            var container = new GayInstaller().Install().Build();

            var app = new App
            {
                MainPage = new NavigationPage(new MainPageView()
                {
                    BindingContext = container.Resolve<MainPageViewModel>()
                })
            };


            LoadApplication(app);
        }

        private WindowManagerFlags m_originalFlags;

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            if (newConfig.Orientation == Orientation.Landscape)
            {
                var attrs = Window.Attributes;
                m_originalFlags = attrs.Flags;
                attrs.Flags |= WindowManagerFlags.Fullscreen;
                Window.Attributes = attrs;
            }
            else
            {
                var attrs = Window.Attributes;
                attrs.Flags = m_originalFlags;
                Window.Attributes = attrs;
            }
        }
    }
}

