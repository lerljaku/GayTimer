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
        public GayDao(string connectionString) : base (connectionString)
        {
        }

        public async Task<Gay[]> SelectAll()
        {
            var uri = new Uri($"{ConStr}/gay/read.php");

            HttpResponseMessage response = null;

            using (var client = CreateClient())
            {
                response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Gay[]>(content);
                }

            }
            
            throw new HttpRequestException($"Invalid status code: {response.StatusCode}");
        }

        public async Task<string> Insert(string nickname, string password, string pwdSalt)
        {
            var uri = new Uri($"{ConStr}/gay/create.php");

            var newGay = new Gay
            {
                Created = DateTime.Now,
                Nick = nickname,
                Password = password,
                PasswordSalt = pwdSalt,
            };

            var serObj = JsonConvert.SerializeObject(newGay);

            using (var client = CreateClient())
            {
                var response = await client.PostAsync(uri, new StringContent(serObj, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                throw new HttpRequestException($"Invalid status code: {response.StatusCode}{Environment.NewLine}{response.Content}");
            }
        }
    }
}