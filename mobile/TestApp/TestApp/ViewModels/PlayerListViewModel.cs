using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GayTimer.Entities;
using GayTimer.Services;
using GayTimer.Views;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace GayTimer.ViewModels
{
    public class PlayerListViewModel : ScreenBase
    {
        private readonly PlayerDetailViewModel m_detailVm;

        private readonly IDataService m_dataService;

        public PlayerListViewModel(IDataService dataService, PlayerDetailViewModel detailVm)
        {
            m_dataService = dataService;
            m_detailVm = detailVm;

            Init();

            AddPlayerCommand = new RelayCommand(AddPlayer);
            DeletePlayerCommand = new RelayCommand(DeletePlayer);
            UpdatePlayerCommand = new RelayCommand(UpdatePlayer);

            MessagingCenter.Subscribe<PlayerDetailViewModel, Player>(this, nameof(PlayerInserted), PlayerInserted);
        }

        public ICommand AddPlayerCommand { get; }
        public ICommand DeletePlayerCommand { get; }
        public ICommand UpdatePlayerCommand { get; }

        private ObservableCollection<Player> m_players;
        public ObservableCollection<Player> Players
        {
            get => m_players;
            set
            {
                m_players = value;
                NotifyPropertyChanged();
            }
        }

        private void Init()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                var gays = await m_dataService.SelectPlayers();

                Players = new ObservableCollection<Player>(gays);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async void AddPlayer()
        {
            m_detailVm.Init(new Player());

            await App.PushAsync<PlayerDetailView>(m_detailVm);
        }

        private async void UpdatePlayer(object arg)
        {
            var player = (Player) arg;

            m_detailVm.Init(player);

            await App.PushAsync<PlayerDetailView>(m_detailVm);
        }

        private async void DeletePlayer(object obj)
        {
            var player = (Player)obj;

            await m_dataService.Delete(player);

            Players.Remove(player);

            await MaterialDialog.Instance.SnackbarAsync(message: $"Player {player.Nick} deleted.", actionButtonText: "Got it", msDuration: 3000);
        }

        private  async void PlayerInserted(PlayerDetailViewModel sender, Player inserted)
        {
            Players.Add(inserted);

            await MaterialDialog.Instance.SnackbarAsync(message: $"Player {inserted.Nick} inserted.", actionButtonText:"Got it", msDuration: 3000);
        }
    }
}
