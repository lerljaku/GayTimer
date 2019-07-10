using System;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using GayTimer;
using GayTimer.Entities;
using GayTimer.Services;
using XF.Material.Forms.UI.Dialogs;

namespace GayTimer.ViewModels
{
    public delegate void TimerToggledEvent(PlayerViewModel player);

    public class PlayerViewModel : ScreenBase
    {
        private readonly ISelectPlayer m_playerSelector;
        private const int Miliseconds = 1000;
        private readonly Timer m_timer;
        private bool m_isRunning;

        public PlayerViewModel(ISelectPlayer playerSelector, Player player)
        {
            Player = player;
            m_playerSelector = playerSelector;
            m_timer = new Timer(IncrementTime);

            IncrementHealthCommand = new RelayCommand(IncrementHealth);
            DecrementHealthCommand = new RelayCommand(DecrementHealth);
            ToggleTimerCommand = new RelayCommand(ToggleTimer);
            SelectPlayerCommand = new RelayCommand(SelectPlayer);
        }

        public event TimerToggledEvent TimerToggled;

        public ICommand IncrementHealthCommand { get; set; }
        public ICommand DecrementHealthCommand { get; set; }
        public ICommand ToggleTimerCommand { get; set; }
        public ICommand SelectPlayerCommand { get; set; }

        public Player Player { get; private set; }

        public string Nick
        {
            get => Player.Nick;
            set
            {
                Player.Nick = value;
                NotifyPropertyChanged();
            }
        }

        private double m_rotation = 90;
        public double Rotation
        {
            get => m_rotation;
            set
            {
                m_rotation = value;
                NotifyPropertyChanged();
            }
        }

        private short m_health;
        public short Health
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

        private async void SelectPlayer()
        {
            if (await m_playerSelector.Start())
            {
                Player = m_playerSelector.Selected;
                NotifyPropertyChanged(nameof(Nick));
            }
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