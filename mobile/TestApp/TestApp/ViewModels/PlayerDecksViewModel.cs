using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Events;
using GayTimer.Services;
using GayTimer.Views;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace GayTimer.ViewModels
{
    public class PlayerDecksViewModel : ScreenBase
    {
        private readonly IDataService m_dataService;
        private readonly DeckDetailViewModel m_deckDetailVm;
        
        private Player m_player;

        public PlayerDecksViewModel(IDataService dataService, DeckDetailViewModel deckDetailVm)
        {
            m_dataService = dataService;
            m_deckDetailVm = deckDetailVm;

            AddDeckCommand = new RelayCommand(AddDeck);

            MessagingCenter.Subscribe<DeckDetailViewModel, DeckInserted>(this, nameof(DeckInserted), DeckInserted);
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
            m_player = player;

            Title = $"Decks - {player.Nick}";

            Decks = new ObservableCollection<Deck>(await m_dataService.SelectDecks(player.Id));
        }

        private async void AddDeck()
        {
            m_deckDetailVm.Init(m_player, new Deck());

            await App.PushAsync<DeckDetailView>(m_deckDetailVm);
        }

        private void DeckInserted(DeckDetailViewModel senderVm, DeckInserted deckArg)
        {
            Decks.Add(deckArg.Deck);
        }
    }
}