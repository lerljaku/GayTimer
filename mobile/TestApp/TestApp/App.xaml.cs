using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        }

        private static readonly Dictionary<Type, Page> m_viewCache = new Dictionary<Type, Page>();
        
        public static async Task PushAsync<TView>(ScreenBase sb)
            where TView : Page, new()
        {
            var viewType = typeof(TView);

            if (!m_viewCache.ContainsKey(viewType))
                m_viewCache.Add(viewType, new TView());

            var page = m_viewCache[viewType];

            page.BindingContext = sb;

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
        }
    }
}
