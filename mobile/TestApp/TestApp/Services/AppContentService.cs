using Xamarin.Forms;

namespace GayTimer.Services
{
    public interface IAppContentService
    {
        void SetContent(Page view);
        
        void PushModel(Page view, ScreenBase vm);
    }

    public class AppContentService : IAppContentService
    {
        public void SetContent(Page view)
        {
            Application.Current.MainPage = view;
        }

        public void PushModel(Page view, ScreenBase vm)
        {
            view.BindingContext = vm;

            Application.Current.NavigationProxy.PushModalAsync(view);
        }
    }
}