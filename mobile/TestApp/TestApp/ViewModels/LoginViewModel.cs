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

        public LoginViewModel(GayDao gayDao, MainMasterDetailPageMasterViewModel mainView, IAppContentService appContentService)
        {
            m_gayDao = gayDao;
            m_mainView = mainView;
            m_appContentService = appContentService;

            SignCommand = new RelayCommand(Sign);
            NewUserCommand = new RelayCommand(NewUser);

            test();
        }

        public ICommand SignCommand { get; }

        public ICommand NewUserCommand { get; }

        [Conditional("DEBUG")]
        private async void test()
        {
            Login = "jakub";
            Password = "1234";

            var users = await m_gayDao.SelectAll();

            NewUser();
        }

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

        private async void NewUser()
        {
            var salt = GetSalt();

            var bytes = Encoding.UTF8.GetBytes($"{Password}{salt}");

            var hash = m_md5.ComputeHash(bytes);

            var hashStr = Encoding.UTF8.GetString(hash);

            try
            {
                var result = await m_gayDao.Insert(Login, hashStr, salt);
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
                throw new NotImplementedException($"user '{Login}' not found");
                return;
            }

            var bytes = Encoding.UTF8.GetBytes($"{Password}{user.PasswordSalt}");

            var hash = m_md5.ComputeHash(bytes);

            var hashStr = Encoding.UTF8.GetString(hash);

            if (hashStr != user.Password)
                throw new NotImplementedException($"'{Login}' password is wrong");

            var view = new MainMasterDetailPage
            {
                Master = new MainMasterDetailPageMaster
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
