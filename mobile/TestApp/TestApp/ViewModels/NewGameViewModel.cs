﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GayTimer.Entities;
using GayTimer.Events;
using GayTimer.Services;
using GayTimer.Views;
using Xamarin.Forms;

namespace GayTimer.ViewModels
{
    public class NewGameViewModel : ScreenBase
    {
        private int m_playerCount = 4;
        private short m_startingLifeTotal = 40;

        private readonly GamePageViewModel m_gamePageVm;
        private readonly SelectLifeTotalViewModel m_selectLifeTotalVm;
        private readonly SelectPlayerCountViewModel m_selectPlayerCntVm;
        private readonly ISelectPlayer m_playerSelector;

        public NewGameViewModel(GamePageViewModel gamePageVm, SelectLifeTotalViewModel selectLifeTotalVm, 
            SelectPlayerCountViewModel selectPlayerCntVm, ISelectPlayer playerSelector)
        {
            m_gamePageVm = gamePageVm;
            m_selectLifeTotalVm = selectLifeTotalVm;
            m_selectPlayerCntVm = selectPlayerCntVm;
            m_playerSelector = playerSelector;

            MessagingCenter.Subscribe<SelectPlayerCountViewModel, PlayerCountSelected>(this, nameof(Events.PlayerCountSelected), PlayerCountSelected);
            MessagingCenter.Subscribe<SelectLifeTotalViewModel, LifeTotalSelected>(this, nameof(Events.LifeTotalSelected), LifeTotalSelected);
        }

        private ObservableCollection<ScreenBase> m_screens;
        public ObservableCollection<ScreenBase> Screens
        {
            get => m_screens;
            set
            {
                m_screens = value;
                NotifyPropertyChanged();
            }
        }

        private ScreenBase m_selectedScreen;
        public ScreenBase SelectedScreen
        {
            get => m_selectedScreen;
            set
            {
                m_selectedScreen = value;
                NotifyPropertyChanged();
            }
        }

        public override void Activated()
        {
            Screens = new ObservableCollection<ScreenBase>(){ m_selectPlayerCntVm };
            SelectedScreen = m_selectPlayerCntVm;
        }

        private void PlayerCountSelected(SelectPlayerCountViewModel vm, PlayerCountSelected playerCnt)
        {
            if (!Screens.Contains(m_selectLifeTotalVm))
                Screens.Add(m_selectLifeTotalVm);

            m_playerCount = playerCnt.PlayerCount;

            SelectedScreen = m_selectLifeTotalVm;
        }

        private void LifeTotalSelected(SelectLifeTotalViewModel vm, LifeTotalSelected lifeTotal)
        {
            m_startingLifeTotal = lifeTotal.StartingLifeTotal;

            Apply();
        }

        private async void Apply()
        {
            var players = Enumerable.Range(0, m_playerCount).Select(i => new PlayerViewModel(m_playerSelector, Player.Dummy(i))
            {
                Health = m_startingLifeTotal,
            }).ToArray();

            m_gamePageVm.AllPlayers = players;

            await Task.WhenAll(App.PopAsync(), App.PushAsync<GamePageView>(m_gamePageVm));
        }
    }
}
