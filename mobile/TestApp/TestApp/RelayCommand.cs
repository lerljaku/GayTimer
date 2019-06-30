using System;
using System.Windows.Input;

namespace GayTimer
{
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