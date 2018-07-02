using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GayTimer.Entities.Dao
{
    public class GayDao
    {
        private static HttpClient m_client = new HttpClient();

        private readonly string m_connectionString;

        public GayDao(string connectionString = "http://192.168.0.103:8080/api")
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
    }
}