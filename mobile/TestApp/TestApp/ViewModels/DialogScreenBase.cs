using System.Windows.Input;

namespace GayTimer.ViewModels
{
    public class DialogScreenBase : ScreenBase
    {
        public ICommand ApplyCommand { get; set; }
        public ICommand DiscardCommand { get; set; }
    }
}