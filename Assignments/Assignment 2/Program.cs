using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Guid
            Player[] players1 = GeneratePlayers(1000000);
            Console.WriteLine("Player count: " + players1.Count());

            // 2. Extension method
            Player[] players2 = GeneratePlayers(5);
            foreach (var player in players2)
            {
                Random rng = new Random();
                player.Items = GenerateItems(rng.Next(1, 6));
            }
            Console.WriteLine("Highest level: " + players2[0].GetHighestLevelItem().Level);
            foreach (var item in players2[0].Items)
            {
                Console.WriteLine(item.Level);
            }

            // 3. LINQ
            Console.WriteLine("Item count (normal): " + GetItems(players2[0]).Count());
            Console.WriteLine("Item count (LINQ): " + GetItemsWithLinq(players2[0]).Count());

            // 4. LINQ 2
            Console.WriteLine("First item level (normal): " + FirstItem(players2[0]).Level);
            Console.WriteLine("First item level (LINQ): " + FirstItemWithLinq(players2[0]).Level);

            // 5. Delegates
            ProcessEachItem(players2[0], PrintItem);

            // 6. Lambda
            ProcessEachItem(players2[0], x => Console.WriteLine("Item id: " + x.Id + "\nItem level: " + x.Level));

            // 7. Generics
            Player[] p1 = new Player[15];
            PlayerFromAnotherGame[] p2 = new PlayerFromAnotherGame[15];
            var random = new Random();
            for (int i = 0; i < 15; i++)
            {
                p1[i] = new Player();
                p1[i].Score = random.Next(0, 5001);
                p2[i] = new PlayerFromAnotherGame();
                p2[i].Score = random.Next(0, 5001);
            }
            Game<Player> g1 = new Game<Player>(p1.ToList());
            Game<PlayerFromAnotherGame> g2 = new Game<PlayerFromAnotherGame>(p2.ToList());
            foreach (var player in g1.GetTop10Players())
            {
                Console.WriteLine("Highscore (Player): " + player.Score);
            }
            foreach (var player in g2.GetTop10Players())
            {
                Console.WriteLine("Highscore (PlayerFromAnotherGame): " + player.Score);
            }
        }
        public static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach (var item in player.Items)
            {
                process(item);
            }
        }
        public static void PrintItem(Item item)
        {
            Console.WriteLine("Item id: " + item.Id + "\nItem level: " + item.Level);
        }
        public static Item FirstItem(Player player)
        {
            if (player.Items[0] != null)
                return player.Items[0];
            else
                return null;
        }
        public static Item FirstItemWithLinq(Player player)
        {
            return player.Items.First();
        }
        public static Item[] GetItems(Player player)
        {
            Item[] items = new Item[player.Items.Count()];

            for (int i = 0; i < player.Items.Count(); i++)
            {
                items[i] = player.Items[i];
            }
            return items;
        }
        public static Item[] GetItemsWithLinq(Player player)
        {
            return player.Items.ToArray();
        }
        public static Player[] GeneratePlayers(int amount)
        {
            Guid[] guids = GenerateGuids(amount);
            Player[] players = new Player[amount];
            for (int i = 0; i < amount; i++)
            {
                players[i] = new Player();
                players[i].Id = guids[i];
            }
            return players;
        }
        public static List<Item> GenerateItems(int amount)
        {
            Guid[] guids = GenerateGuids(amount);
            List<Item> items = new List<Item>();

            for (int i = 0; i < amount; i++)
            {
                Item item = new Item();
                item.Id = guids[i];
                Random rnd = new Random();
                item.Level = rnd.Next(1, 51);
                items.Add(item);
            }
            return items;
        }
        public static Guid[] GenerateGuids(int amount)
        {
            Guid[] guids = new Guid[amount];
            do
            {
                for (int i = 0; i < amount; i++)
                {
                    guids[i] = Guid.NewGuid();
                }
            } while (guids.Count() != guids.Distinct().Count());
            return guids;
        }
    }
}