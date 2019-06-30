using System;

namespace GayTimer.Entities
{
    public class Player : ScreenBase
    {
        private int m_id;
        public int Id
        {
            get => m_id;
            set
            {
                m_id = value;
                NotifyPropertyChanged();
            }
        }

        private string m_firstName;
        public string FirstName
        {
            get => m_firstName;
            set
            {
                m_firstName = value;
                NotifyPropertyChanged();
            }
        }

        private string m_lastName;
        public string LastName
        {
            get => m_lastName;
            set
            {
                m_lastName = value;
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

        public DateTime Created { get; set; }
    }
}
