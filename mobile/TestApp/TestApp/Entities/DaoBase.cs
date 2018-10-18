using System.Net.Http;

namespace GayTimer.Entities
{
    public abstract class DaoBase
    {
        protected readonly string ConStr;

        protected DaoBase(string connectionString)
        {
            ConStr = connectionString;
        }

        protected HttpClient CreateClient()
        {
            return new HttpClient();
        }
    }
}