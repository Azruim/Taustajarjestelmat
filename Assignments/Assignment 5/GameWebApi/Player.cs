using System;
using System.Collections.Generic;

namespace GameWebApi
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsBanned { get; set; }
        public DateTime Creationtime { get; set; }
        public List<Item> Items { get; set; }
        public List<string> Tags { get; set; }

        public Player()
        {
            this.Id = Guid.NewGuid();
            this.Name = "";
            this.Score = 0;
            this.Level = 1;
            this.IsBanned = false;
            this.Creationtime = DateTime.Now;
            this.Items = new List<Item>();
            this.Tags = new List<string>();
        }
    }

    public class NewPlayer
    {
        public string Name { get; set; }
    }

    public class ModifiedPlayer
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsBanned { get; set; }
        public List<string> Tags { get; set; }
    }
}