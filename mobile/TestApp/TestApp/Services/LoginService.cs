using System;
using System.IO;
using GayTimer.Entities;
using GayTimer.ViewModels;
using GayTimer.Views;
using Xamarin.Forms;

namespace GayTimer.Services
{
    public interface ILoginService
    {
        bool Login();
    }

    public class LoginService : ILoginService
    {
        private readonly string m_userDataPath;
        private readonly ICurrentUser m_currentUser;
        private readonly ISerializerProvider m_serializerProvider;

        public LoginService(string userDataPath, ICurrentUser currentUser, ISerializerProvider serializerProvider)
        {
            m_userDataPath = userDataPath;
            m_currentUser = currentUser;
            m_serializerProvider = serializerProvider;
        }

        public bool Login()
        {
            if (!File.Exists(m_userDataPath))
            {
                return false;
            }

            var user = m_serializerProvider.Deserialize<Gay>(File.Open(m_userDataPath, FileMode.Open));
            
            m_currentUser.User = user;

            return true;
        }
    }
}