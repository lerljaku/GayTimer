using System;
using System.Threading;
using System.Windows.Input;
using GayTimer;
using GayTimer.Entities;

namespace GayTimer.ViewModels
{
    public delegate void TimerToggledEvent(PlayerViewModel player);

    public class PlayerViewModel : ScreenBase
    {
        private const int Miliseconds = 1000;
        private readonly Timer m_timer;
        private bool m_isRunning;

        public PlayerViewModel(SelectGayViewModel selectGayViewModel)
        {
            m_timer = new Timer(IncrementTime);

            IncrementHealthCommand = new RelayCommand(IncrementHealth);
            DecrementHealthCommand = new RelayCommand(DecrementHealth);
            ToggleTimerCommand = new RelayCommand(ToggleTimer);
            SelectGayCommand = new RelayCommand(SelectGay);
        }

        public event TimerToggledEvent TimerToggled;

        public ICommand IncrementHealthCommand { get; set; }
        public ICommand DecrementHealthCommand { get; set; }
        public ICommand ToggleTimerCommand { get; set; }
        public ICommand SelectGayCommand { get; set; }

        private Gay m_player;
        public Gay Player
        {
            get => m_player;
            set
            {
                m_player = value;
                NotifyPropertyChanged();
            }
        }

        private ushort m_health;
        public ushort Health
        {
            get => m_health;
            set
            {
                m_health = value; 
                NotifyPropertyChanged();
            }
        }

        private TimeSpan m_timeSpent = TimeSpan.Zero;
        public TimeSpan TimeSpent
        {
            get => m_timeSpent;
            set
            {
                m_timeSpent = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsRunning
        {
            get => m_isRunning;
            private set
            {
                m_isRunning = value;
                NotifyPropertyChanged();
            }
        }

        public void ToggleTimer()
        {
            TimerToggled?.Invoke(this);
        }

        public void StartTimer()
        {
            if(IsRunning)
                return;

            m_timer.Change(Miliseconds, Miliseconds);
            IsRunning = true;
        }

        public void StopTimer()
        {
            if (IsRunning)
                m_timer.Change(0, 0);

            IsRunning = false;
        }

        private void SelectGay()
        {

        }

        private void IncrementHealth()
        {
            Health++;
        }

        private void DecrementHealth()
        {
            Health--;
        }
        
        private void IncrementTime(object state)
        {
            if (IsRunning)
                TimeSpent = TimeSpent.Add(TimeSpan.FromSeconds(1));
        }
    }
}