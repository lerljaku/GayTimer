using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GayTimer.Entities;
using GayTimer.Services;

namespace TestApp.Droid.Services
{
    public class Players_Android 
    {
        public Task ImportPlayers(IDataService dataService)
        {
            using (var str = Android.App.Application.Context.Assets.Open("players.txt"))
            {
                using (var sr = new StreamReader(str))
                {
                    var now = DateTimeOffset.Now;

                    var rows = sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(d => d.Trim()).ToList();

                    var players = rows.Select(d => new Player() {Nick = d, Created = now.LocalDateTime});

                    var sqlDs = (SqlDataService)dataService;

                    return sqlDs.InsertAllAsync(players);
                }
            }
        }
    }
}