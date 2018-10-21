using System.Linq;
using System.Windows.Input;
using GayTimer.Services;
using GayTimer.Views;

namespace GayTimer.ViewModels
{
    public class NewGameViewModel : ScreenBase
    {
        private readonly MainMasterDetailPageMasterViewModel m_masterDetail;
        private readonly GamePageViewModel m_gamePageVm;

        public NewGameViewModel(MainMasterDetailPageMasterViewModel masterDetail, GamePageViewModel gamePageVm)
        {
            m_masterDetail = masterDetail;
            m_gamePageVm = gamePageVm;
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

            m_gamePageVm.AllPlayers = players;
            
            m_masterDetail.Activate(m_gamePageVm);
        }

        private void SetStartingLifeTotal(object obj)
        {
            StartingLifeTotal = (ushort)obj;
        }
    }
}
