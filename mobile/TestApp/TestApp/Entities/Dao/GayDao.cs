using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GayTimer.Entities.Dao
{
    public class GayDao : DaoBase
    {
        private static readonly HttpClient m_client = new HttpClient();

        private readonly string m_connectionString;

        public GayDao(string connectionString = "http://192.168.0.104:8080/api")
        {
            m_connectionString = connectionString;
        }

        public async Task<Gay[]> SelectAll()
        {
            var uri = new Uri($"{m_connectionString}/gay/read.php");

            var response = await m_client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Gay[]>(content);
            }
            
            throw new HttpRequestException($"Invalid status code: {response.StatusCode}");
        }

        public async Task<bool> Insert(string nickname, string password, string pwdSalt)
        {
            var uri = new Uri($"{m_connectionString}/gay/create.php");

            var newGay = new Gay
            {
                Created = DateTime.Now,
                FirstName = nickname,
                Password = password,
                PasswordSalt = pwdSalt,
            };

            var serObj = JsonConvert.SerializeObject(newGay);

            var response = await m_client.PostAsync(uri, new StringContent(serObj, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new HttpRequestException($"Invalid status code: {response.StatusCode}{Environment.NewLine}{response.Content}");
        }
    }
}