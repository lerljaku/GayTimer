using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GayTimer
{
    public class ScreenBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_title;
        public string Title
        {
            get => m_title;
            set
            {
                if (m_title == value)
                    return;

                m_title = value;
                NotifyPropertyChanged();
            }
        }

        private bool m_isBusy;
        public bool IsBusy
        {
            get => m_isBusy;
            set
            {
                if (m_isBusy == value)
                    return;
                
                m_isBusy = value;
                NotifyPropertyChanged();
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Activated() {}
    }

    public abstract class Command :ICommand
    {
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteInternal(parameter);
        }

        protected abstract void ExecuteInternal(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}
