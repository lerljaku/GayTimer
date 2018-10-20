using System;
using System.IO;
using GayTimer.Entities;

namespace GayTimer.Services
{
    public interface ILoginService
    {
        bool Login();

        void SetLogin(Gay user);
    }

    public class LoginService : ILoginService
    {
        private readonly string m_userDataPath;
        private readonly ICurrentUser m_currentUser;
        private readonly ISerializerProvider m_serializerProvider;
        private readonly IErrorService m_errorService;

        public LoginService(string userDataPath, ICurrentUser currentUser, ISerializerProvider serializerProvider, IErrorService errorService)
        {
            m_userDataPath = userDataPath;
            m_currentUser = currentUser;
            m_serializerProvider = serializerProvider;
            m_errorService = errorService;
        }

        public bool Login()
        {
            if (!File.Exists(m_userDataPath))
            {
                return false;
            }

            try
            {
                m_currentUser.User = m_serializerProvider.Deserialize<Gay>(File.ReadAllText(m_userDataPath));

                return true;
            }
            catch (Exception e)
            {
                m_errorService.LogError(e);
                File.Delete(m_userDataPath);
                return false;
            }
        }

        public void SetLogin(Gay user)
        {
            File.WriteAllText(m_userDataPath, m_serializerProvider.Serialize(user));
        }
    }
}