using Autofac;
using GayTimer.Bootstrapper;
using GayTimer.Installer;
using GayTimer.MenuItems;
using GayTimer.ViewModels;

namespace GayTimer
{
	public partial class App
	{
		public App ()
		{
            InitializeComponent();

            var bootstrapper = new GayBootStrapper();
            
        }

        protected override void OnStart ()
		{
		    // Handle when your app starts
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
