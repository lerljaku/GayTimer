using System.Linq;
using System.Windows.Input;
using GayTimer.Services;
using GayTimer.Views;

namespace GayTimer.ViewModels
{
    public class NewGameViewModel : ScreenBase
    {
        private readonly IAppContentService m_appContentService;

        public NewGameViewModel(IAppContentService appContentService)
        {
            m_appContentService = appContentService;
            ApplyCommand = new RelayCommand(Apply);
            SetStartingLifeTotalCommand = new RelayCommand(SetStartingLifeTotal);
        }

        public static ushort Twenty => 20;
        public static ushort Thirty => 30;
        public static ushort Forty => 40;

        public ICommand ApplyCommand { get; }
        public ICommand SetStartingLifeTotalCommand { get; }

        private int m_playerCount = 4;
        public int PlayerCount
        {
            get => m_playerCount;
            set
            {
                m_playerCount = value;
                NotifyPropertyChanged();
            }
        }

        private ushort m_startingLifeTotal = 40;
        public ushort StartingLifeTotal
        {
            get => m_startingLifeTotal;
            set
            {
                m_startingLifeTotal = value;
                NotifyPropertyChanged();
            }
        }

        private void Apply()
        {
            var players = Enumerable.Range(0, PlayerCount).Select(d => new PlayerViewModel()
            {
                Health = StartingLifeTotal,
            }).ToArray();

            var newGameVm = new GamePageViewModel()
            {
                AllPlayers = players,
            };

            newGameVm.Init();

            m_appContentService.SetContent(new GamePageView(), newGameVm);
        }

        private void SetStartingLifeTotal(object obj)
        {
            StartingLifeTotal = (ushort)obj;
        }
    }
}
