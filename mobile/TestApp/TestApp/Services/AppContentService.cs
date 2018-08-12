using Xamarin.Forms;

namespace GayTimer.Services
{
    public interface IAppContentService
    {
        void SetContent(Page view);

        void SetContent(Page view, ScreenBase screen);
    }

    public class AppContentService : IAppContentService
    {
        public void SetContent(Page view)
        {
            Application.Current.MainPage = view;
        }

        public void SetContent(Page view, ScreenBase vm)
        {
            view.BindingContext = vm;

            Application.Current.MainPage = view;
        }
    }
}