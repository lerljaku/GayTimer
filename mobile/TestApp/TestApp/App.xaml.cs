using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Distribute;
using Xamarin.Forms;

namespace GayTimer
{
	public partial class App
	{
		public App ()
		{
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            InitializeComponent();

            XF.Material.Forms.Material.Init(this, "Material.Configuration");

            Distribute.ReleaseAvailable = OnReleaseAvailable;

            AppCenter.Start("86475ab6d1895cb33d271577b62feb4884a7708a", typeof(Distribute));
        }

        //private static readonly Dictionary<Type, Page> m_viewCache = new Dictionary<Type, Page>();
        
        public static async Task PushAsync<TView>(ScreenBase sb)
            where TView : Page, new()
        {
            //var viewType = typeof(TView);

            //if (!m_viewCache.ContainsKey(viewType))
            //    m_viewCache.Add(viewType, new TView());

            var page = new TView {BindingContext = sb}; // m_viewCache[viewType];
            
            await Current.MainPage.NavigationProxy.PushAsync(page);

            sb.Activated();
        }

        public static Task PopAsync()
        {
            return Current.MainPage.NavigationProxy.PopAsync();
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

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception) e.ExceptionObject;

            var st = exception.StackTrace;
            var msg = exception.Message;
        }

        private static bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            // Look at releaseDetails public properties to get version information, release notes text or release notes URL
            var versionName = releaseDetails.ShortVersion;
            var versionCodeOrBuildNumber = releaseDetails.Version;
            var releaseNotes = releaseDetails.ReleaseNotes;
            var releaseNotesUrl = releaseDetails.ReleaseNotesUrl;

            // custom dialog
            var title = "Version " + versionName + " available!";

            // On mandatory update, user cannot postpone
            var answer = releaseDetails.MandatoryUpdate ? 
                Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install") : 
                Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install", "Maybe tomorrow...");

            answer.ContinueWith((task) =>
            {
                // If mandatory or if answer was positive
                if (releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                {
                    // Notify SDK that user selected update
                    Distribute.NotifyUpdateAction(UpdateAction.Update);
                }
                else
                {
                    // Notify SDK that user selected postpone (for 1 day)
                    // Note that this method call is ignored by the SDK if the update is mandatory
                    Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                }
            });

            // Return true if you are using your own dialog, false otherwise
            return true;
        }
    }
}
