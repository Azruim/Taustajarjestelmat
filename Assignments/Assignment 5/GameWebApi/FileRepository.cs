using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi
{
    public class FileRepository //: IRepository
    {
        string path = Directory.GetCurrentDirectory() + "/game-dev.txt";

        public Task<Player> CreatePlayer(NewPlayer newPlayer)
        {
            List<string> lines = File.ReadAllLines(path).ToList();
            var player = new Player();
            player.Id = Guid.NewGuid();
            player.Name = newPlayer.Name;
            player.Score = 0;
            player.Level = 0;
            player.IsBanned = false;
            player.Creationtime = DateTime.Now;

            lines.Add(player.Id.ToString());
            lines.Add(player.Name);
            lines.Add(player.Score.ToString());
            lines.Add(player.Level.ToString());
            lines.Add(player.IsBanned.ToString());
            lines.Add(player.Creationtime.ToString());

            File.WriteAllLines(path, lines);
            return Task.FromResult(player);
        }

        public Task<Player> DeletePlayer(Guid id)
        {
            List<string> lines = File.ReadAllLines(path).ToList();
            var player = new Player();
            for (int i = 0; i < lines.Count(); i += 6)
            {
                if (lines[i] == id.ToString())
                {
                    player.Id = Guid.Parse(lines[i]);
                    player.Name = lines.ElementAt(i + 1);
                    player.Score = int.Parse(lines.ElementAt(i + 2));
                    player.Level = int.Parse(lines.ElementAt(i + 3));
                    player.IsBanned = bool.Parse(lines.ElementAt(i + 4));
                    player.Creationtime = DateTime.Parse(lines.ElementAt(i + 5));
                    lines.RemoveRange(i, 6);
                    break;
                }
            }
            File.WriteAllLines(path, lines);
            return Task.FromResult(player);
        }

        public Task<Player> GetPlayer(Guid id)
        {
            List<string> lines = File.ReadAllLines(path).ToList();
            var player = new Player();
            for (int i = 0; i < lines.Count(); i += 6)
            {
                if (lines[i] == id.ToString())
                {
                    player.Id = Guid.Parse(lines[i]);
                    player.Name = lines.ElementAt(i + 1);
                    player.Score = int.Parse(lines.ElementAt(i + 2));
                    player.Level = int.Parse(lines.ElementAt(i + 3));
                    player.IsBanned = bool.Parse(lines.ElementAt(i + 4));
                    player.Creationtime = DateTime.Parse(lines.ElementAt(i + 5));
                }
            }
            return Task.FromResult(player);
        }

        public Task<Player[]> GetAllPlayers()
        {
            List<string> lines = File.ReadAllLines(path).ToList();
            var players = new List<Player>();
            for (int i = 0; i < lines.Count(); i += 6)
            {
                var player = new Player();
                player.Id = Guid.Parse(lines[i]);
                player.Name = lines.ElementAt(i + 1);
                player.Score = int.Parse(lines.ElementAt(i + 2));
                player.Level = int.Parse(lines.ElementAt(i + 3));
                player.IsBanned = bool.Parse(lines.ElementAt(i + 4));
                player.Creationtime = DateTime.Parse(lines.ElementAt(i + 5));
                players.Add(player);
            }
            return Task.FromResult(players.ToArray());
        }

        public Task<Player> UpdatePlayer(Guid id, ModifiedPlayer player)
        {
            List<string> lines = File.ReadAllLines(path).ToList();
            var modifiedPlayer = new Player();
            for (int i = 0; i < lines.Count(); i += 6)
            {
                if (lines[i] == id.ToString())
                {
                    lines[i + 2] = player.Score.ToString();
                    modifiedPlayer.Id = Guid.Parse(lines[i]);
                    modifiedPlayer.Name = lines.ElementAt(i + 1);
                    modifiedPlayer.Score = player.Score;
                    modifiedPlayer.Level = int.Parse(lines.ElementAt(i + 3));
                    modifiedPlayer.IsBanned = bool.Parse(lines.ElementAt(i + 4));
                    modifiedPlayer.Creationtime = DateTime.Parse(lines.ElementAt(i + 5));
                }
            }
            File.WriteAllLines(path, lines);
            return Task.FromResult(modifiedPlayer);
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            return null;
        }

        public Task<Item> UpdateItem(Guid playerId, ModifiedItem item)
        {
            throw new NotImplementedException();
        }

        public Task<Item> DeleteItem(Guid playerId, Item item)
        {
            throw new NotImplementedException();
        }
    }
}