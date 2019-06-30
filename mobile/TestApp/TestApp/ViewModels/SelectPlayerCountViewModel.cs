using System.Windows.Input;
using GayTimer.Events;
using Xamarin.Forms;

namespace GayTimer.ViewModels
{
    public class SelectPlayerCountViewModel : ScreenBase
    {
        public SelectPlayerCountViewModel()
        {
            Confirm2Command = new RelayCommand(Confirm2);
            Confirm3Command = new RelayCommand(Confirm3);
            Confirm4Command = new RelayCommand(Confirm4);

            Title = "Player count";
        }

        public ICommand Confirm2Command { get; }
        public ICommand Confirm3Command { get; }
        public ICommand Confirm4Command { get; }
        
        private void Confirm2()
        {
            PlayerCount(2);
        }

        private void Confirm3()
        {
            PlayerCount(3);
        }

        private void Confirm4()
        {
            PlayerCount(4);
        }

        private void PlayerCount(int cnt)
        {
            MessagingCenter.Send(this, nameof(PlayerCountSelected), args: new PlayerCountSelected(cnt));
        }
    }
}