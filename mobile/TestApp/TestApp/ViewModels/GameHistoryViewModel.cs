using System.Collections.ObjectModel;
using GayTimer.Entities;

namespace GayTimer.ViewModels
{
    public class GameHistoryViewModel : ScreenBase
    {
        public ObservableCollection<Game> Games { get; set; }


    }
}