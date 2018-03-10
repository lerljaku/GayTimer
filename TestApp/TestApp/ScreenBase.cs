using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace GayTimer
{
    public class ScreenBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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

    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> m_canExecuteFunc;
        private readonly Action<object> m_executeFunc;

        public RelayCommand(Action<object> executeFunc, Func<object, bool> canExecuteFunc)
        {
            m_canExecuteFunc = canExecuteFunc;
            m_executeFunc = executeFunc;
        }

        public RelayCommand(Action<object> executeFunc)
            : this(executeFunc, (obj) => true)
        { }

        public RelayCommand(Action executeFunc, Func<bool> canExecuteFunc)
            : this((obj) => executeFunc.Invoke(), (obj) => canExecuteFunc.Invoke())
        { }

        public RelayCommand(Action executeFunc)
            : this((obj) => executeFunc.Invoke(), (obj) => true)
        { }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            try
            {
                m_executeFunc.Invoke(parameter);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return m_canExecuteFunc.Invoke(parameter);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
