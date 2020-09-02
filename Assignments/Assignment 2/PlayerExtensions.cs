using System;
namespace Assignment_2
{
    public static class PlayerExtensions
    {
        public static Item GetHighestLevelItem(this Player player)
        {
            Item highest = new Item();
            highest.Level = player.Items[0].Level;
            foreach (var item in player.Items)
            {
                if (item.Level > highest.Level)
                    highest.Level = item.Level;
            }
            return highest;
        }
    }
}