using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Services;

namespace GayTimer.ViewModels
{
    public class PlayerDecksViewModel : ScreenBase
    {
        private readonly IDataService m_dataService;

        public PlayerDecksViewModel(IDataService dataService)
        {
            m_dataService = dataService;

            AddDeckCommand = new RelayCommand(AddDeck);
        }

        public ICommand AddDeckCommand { get; set; }

        private ObservableCollection<Deck> m_decks;
        public ObservableCollection<Deck> Decks
        {
            get => m_decks;
            set
            {
                m_decks = value;
                NotifyPropertyChanged();
            }
        }

        public async void Init(Player player)
        {
            Title = $"Decks - {player.Nick}";

            Decks = new ObservableCollection<Deck>(await m_dataService.SelectDecks(player.Id));
        }

        private void AddDeck()
        {
        }
    }
}