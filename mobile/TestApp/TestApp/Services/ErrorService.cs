using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GayTimer.Services
{
    public interface IErrorService
    {
        void LogError(string message);
        void LogWarn(string message);
        void LogInfo(string message);
        void LogDebug(string message);

        void LogError(Exception ex);
        void LogWarn(Exception ex);
        void LogInfo(Exception ex);
        void LogDebug(Exception ex);
    }

    public class ErrorService : IErrorService
    {
        public ObservableCollection<string> Errors { get; } = new ObservableCollection<string>();

        public void LogError(string message)
        {
            Errors.Add(message);
        }

        public void LogWarn(string message)
        {
            Errors.Add(message);
        }

        public void LogInfo(string message)
        {
            Errors.Add(message);
        }

        public void LogDebug(string message)
        {
            Errors.Add(message);
        }

        public void LogError(Exception ex)
        {
            LogError(ex.Message);
        }

        public void LogWarn(Exception ex)
        {
            LogWarn(ex.Message);
        }

        public void LogInfo(Exception ex)
        {
            LogInfo(ex.Message);
        }

        public void LogDebug(Exception ex)
        {
            LogDebug(ex.Message);
        }
    }
}