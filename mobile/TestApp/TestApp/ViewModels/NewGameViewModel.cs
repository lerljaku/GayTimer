using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GayTimer.ViewModels
{
    public class NewGameViewModel : ScreenBase
    {
        public NewGameViewModel()
        {
            ApplyCommand = new RelayCommand(Apply);
            AddCommand = new RelayCommand(AddPlayer);
            RemoveCommand = new RelayCommand(RemovePlayer);
        }

        public ICommand ApplyCommand { get; }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        
        private ObservableCollection<PlayerViewModel> m_players;
        public ObservableCollection<PlayerViewModel> Players
        {
            get => m_players;
            set
            {
                m_players = value; 
                NotifyPropertyChanged();
            }
        }

        private void AddPlayer()
        {
            Players.Add(new PlayerViewModel());
        }

        private void RemovePlayer(object arg)
        {
            var player = (PlayerViewModel) arg;

            Players.Remove(player);
        }

        private void Apply()
        {
            var newGameVm = new GamePageViewModel()
            {
                AllPlayers = Players.ToArray(),
            };

            newGameVm.Init();


        }
    }
}
