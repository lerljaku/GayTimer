using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Events;
using GayTimer.Services;
using Xamarin.Forms;

namespace GayTimer.ViewModels
{
    public class DeckDetailViewModel : ScreenBase
    {
        private readonly IDataService m_dataService;
        private Player m_player;
        private Deck m_deck;

        public DeckDetailViewModel(IDataService dataService)
        {
            m_dataService = dataService;

            SaveCommand = new RelayCommand(Save);
            DiscardCommand = new RelayCommand(Discard);
        }

        public ICommand SaveCommand { get; set; }
        public ICommand DiscardCommand { get; set; }

        private string m_commander;
        public string Commander
        {
            get => m_commander;
            set
            {
                m_commander = value;
                NotifyPropertyChanged();
            }
        }

        private string m_name;
        public string Name
        {
            get => m_name;
            set
            {
                m_name = value;
                NotifyPropertyChanged();
            }
        }

        private string m_note;
        public string Note
        {
            get => m_note;
            set
            {
                m_note = value;
                NotifyPropertyChanged();
            }
        }

        public List<string> Commanders => DependencyService.Get<ICommanders>().GetCommanderList();

        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
                .Where(x => x.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(x => x)
                .ToList();

        public void Init(Player player, Deck deck)
        {
            m_player = player;
            m_deck = deck;

            Name = deck.Name;
            Commander = deck.Commander;
            Note = deck.Note;

            Title = $"Deck {Name} ({player.Nick})";
        }

        private void Save()
        {
            m_deck.Name = Name;
            m_deck.Commander = Commander;
            m_deck.Note = Note;
            m_deck.PlayerId = m_player.Id;

            m_dataService.Insert(m_deck);

            App.PopAsync();

            MessagingCenter.Send(this, nameof(DeckInserted), new DeckInserted(m_deck));
        }
        
        private void Discard()
        {
            App.PopAsync();
        }
    }
}