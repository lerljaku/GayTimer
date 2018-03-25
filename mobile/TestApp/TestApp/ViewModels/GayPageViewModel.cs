using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GayTimer.Entities;
using GayTimer.Entities.Dao;

namespace GayTimer.ViewModels
{
    public class GayPageViewModel : ScreenBase
    {
        private readonly GayDao m_gayDao;

        public GayPageViewModel(GayDao gayDao)
        {
            m_gayDao = gayDao;

            Init();
        }

        private ObservableCollection<Gay> m_gays;
        public ObservableCollection<Gay> Gays
        {
            get => m_gays;
            set
            {
                m_gays = value;
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
                var gays = await m_gayDao.SelectAll();

                Gays = new ObservableCollection<Gay>(gays);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
