using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.OS;
using GayTimer.Entities;
using GayTimer.Services;
using Environment = System.Environment;

namespace TestApp.Droid.Services
{
    public class GayRate_Android 
    {
        public void GetGayRate(IDataService dataService)
        {
            using (var str = Android.App.Application.Context.Assets.Open("legacyData.txt"))
            {
                using (var sr = new StreamReader(str))
                {
                    var rows = sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(d => d.Trim()).ToList();

                    var now = DateTimeOffset.Now;

                    var decks = new Dictionary<string, Deck>();
                    var players = dataService.SelectPlayers().Result.ToDictionary(d => d.Nick, d => d);

                    var g = 0;
                    foreach (var row in rows)
                    {
                        var game = new Game()
                        {
                            Players = new List<PlayerToGame>()
                        };

                        var cells = row.Split(';', StringSplitOptions.RemoveEmptyEntries);
                        
                        var date = DateTime.Parse(cells[0]);
                        var playerCnt = int.Parse(cells[1]);
                        var winnerNick = cells[2];
                        var winnerDick = cells[3];

                        for (int i = 4; i < cells.Length; i+=2)
                        {
                            var playerNick = cells[i].Trim();
                            var playerDick = cells[i+1].Trim();
                            if (!players.ContainsKey(playerNick))
                            {
                                 var player = new Player(){Created = now.DateTime, Nick = playerNick};
                                 dataService.SaveAsync(player).Wait();
                                 players.Add(playerNick, player);
                            }
                            if (!decks.ContainsKey(playerDick))
                            {
                                var deck = new Deck()
                                {
                                    Name = playerDick,
                                    PlayerId = players[playerNick].Id,
                                };
                                dataService.SaveAsync(deck).Wait();
                                decks.Add(playerDick, deck);
                            }

                            game.Players.Add(new PlayerToGame()
                            {
                                DeckId = decks[playerDick].Id,
                                IsWinner = playerNick == winnerNick,
                                PlayerId = players[playerNick].Id,
                            });
                        }

                        game.Created = date;

                        dataService.SaveAsync(game).Wait();

                        System.Diagnostics.Debug.WriteLine($"GayRate_Android game{g++} complete");
                    }
                }
            }
        }
    }
}