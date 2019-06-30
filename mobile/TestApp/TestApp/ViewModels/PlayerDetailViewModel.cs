using System;
using System.Collections.Generic;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Events;
using GayTimer.Services;
using Xamarin.Forms;

namespace GayTimer.ViewModels
{
    public class PlayerDetailViewModel : ScreenBase
    {
        private Player m_player;
        private readonly IDataService m_dataService;

        public PlayerDetailViewModel(IDataService dataService)
        {
            m_dataService = dataService;

            SaveCommand = new RelayCommand(Save);
            DiscardCommand = new RelayCommand(Discard);
        }

        public ICommand DiscardCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand NavigateToDecksCommand { get; }

        private List<Deck> m_decks;
        public List<Deck> Decks
        {
            get => m_decks;
            set
            {
                m_decks = value;
                NotifyPropertyChanged();
            }
        }

        private string m_nick;
        public string Nick
        {
            get => m_nick;
            set
            {
                m_nick = value;
                NotifyPropertyChanged();
            }
        }

        private string m_color;
        public string Color
        {
            get => m_color;
            set
            {
                m_color = value;
                NotifyPropertyChanged();
            }
        }

        public void Init(Player player)
        {
            m_player = player;

            Nick = m_player.Nick;

            Title = string.IsNullOrWhiteSpace(player.Nick) ? $"New user" : $"User - {player.Nick}";
        }

        private void Save()
        {
            m_player.Nick = Nick;

            m_dataService.Insert(m_player);

            App.PopAsync();

            MessagingCenter.Send(this, nameof(PlayerInserted), m_player);
        }

        private void Discard()
        {
            App.PopAsync();
        }
    }
}