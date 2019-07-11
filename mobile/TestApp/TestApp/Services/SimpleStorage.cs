using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GayTimer.Services
{
    public class SimpleStorage
    {
        private static string CachePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "simpleStorage.json");

        private static Dictionary<string, string> m_cache;
        private static Dictionary<string, string> Cache => m_cache ?? (m_cache = InitCache());
        
        public static Task Save(string key, string value)
        {
            if (!Cache.ContainsKey(key))
                Cache.Add(key, value);
            else Cache[key] = value;

            return Persist();
        }

        public static string Get(string key)
        {
            if (Cache.ContainsKey(key))
                return Cache[key];

            return null;
        }

        private static Task Persist()
        {
            return Task.Run(() =>
            {
                try
                {
                    var data = JsonConvert.SerializeObject(Cache);

                    File.WriteAllText(CachePath, data);
                }
                catch (Exception e)
                {
                    Debug.Fail(e.Message, e.StackTrace);
                }
            });
        }

        private static Dictionary<string, string> InitCache()
        {
            if (!File.Exists(CachePath))
                return new Dictionary<string, string>();

            try
            {
                var data = File.ReadAllText(CachePath);

                return JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message, e.StackTrace);

                return new Dictionary<string, string>();
            }
        }
    }
}