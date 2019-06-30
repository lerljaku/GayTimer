using System.Windows.Input;
using GayTimer.Events;
using Xamarin.Forms;

namespace GayTimer.ViewModels
{
    public class SelectLifeTotalViewModel : ScreenBase
    {
        public SelectLifeTotalViewModel()
        {
            Confirm20Command = new RelayCommand(Confirm20);
            Confirm30Command = new RelayCommand(Confirm30);
            Confirm40Command = new RelayCommand(Confirm40);

            Title = "Life total";
        }

        public ICommand Confirm20Command { get; }
        public ICommand Confirm30Command { get; }
        public ICommand Confirm40Command { get; }

        private void Confirm20()
        {
            PlayerCount(20);
        }

        private void Confirm30()
        {
            PlayerCount(30);
        }

        private void Confirm40()
        {
            PlayerCount(40);
        }

        private void PlayerCount(short startingLife)
        {
            MessagingCenter.Send(this, nameof(LifeTotalSelected), args: new LifeTotalSelected(startingLife));
        }
    }
}