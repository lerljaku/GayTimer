using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using GayTimer.Entities.Dao;
using GayTimer.MenuItems;
using GayTimer.Services;

namespace GayTimer.ViewModels
{
    public class LoginViewModel : ScreenBase
    {
        private readonly MD5 m_md5 = MD5.Create();
        private readonly GayDao m_gayDao;
        private readonly MainMasterDetailPageMasterViewModel m_mainView;
        private readonly IAppContentService m_appContentService;
        private readonly ILoginService m_loginService;

        public LoginViewModel(GayDao gayDao, MainMasterDetailPageMasterViewModel mainView, IAppContentService appContentService, ILoginService loginService)
        {
            m_gayDao = gayDao;
            m_mainView = mainView;
            m_appContentService = appContentService;
            m_loginService = loginService;

            SignCommand = new RelayCommand(Sign);
            NewUserCommand = new RelayCommand(NewUser);
        }

        public ICommand SignCommand { get; }

        public ICommand NewUserCommand { get; }
        
        private string m_login;
        public string Login
        {
            get => m_login;
            set
            {
                m_login = value;
                NotifyPropertyChanged();
            }
        }

        private string m_password;
        public string Password
        {
            get => m_password;
            set
            {
                m_password = value;
                NotifyPropertyChanged();
            }
        }

        private string m_errorMessage;
        public string ErrorMessage
        {
            get => m_errorMessage;
            set
            {
                m_errorMessage = value;
                NotifyPropertyChanged();
            }
        }

        private async void NewUser()
        {
            var salt = GetSalt();

            var bytes = Encoding.UTF8.GetBytes($"{Password}{salt}");

            var hash = m_md5.ComputeHash(bytes);

            var hashStr = Encoding.UTF8.GetString(hash);

            try
            {
                ErrorMessage = await m_gayDao.Insert(Login, hashStr, salt);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private async void Sign()
        {
            var users = await m_gayDao.SelectAll();

            var user = users.FirstOrDefault(d => d.Nick == Login);
            if (user == null)
            {
                ErrorMessage = $"user '{Login}' not found";
                return;
            }

            var bytes = Encoding.UTF8.GetBytes($"{Password}{user.PasswordSalt}");

            var hash = m_md5.ComputeHash(bytes);

            var hashStr = Encoding.UTF8.GetString(hash);
            if (hashStr != user.PasswordHash)
            {
                ErrorMessage = $"'{Login}' password is wrong";
                return;
            }

            m_loginService.SetLogin(user);

            GoToMainView();
        }

        private void GoToMainView()
        {
            var view = new MainMasterDetailPage
            {
                Master = 
                {
                    BindingContext = m_mainView,
                }
            };

            m_appContentService.SetContent(view);
        }
        
        private static string GetSalt()
        {
            var random = new RNGCryptoServiceProvider();

            const int maxLength = 32;

            // Empty salt array
            var salt = new byte[maxLength];

            random.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }
    }
}
