using System.Linq;
using System.Collections.Generic;
namespace Assignment_2
{
    public class Game<T> where T : IPlayer
    {
        private List<T> _players;

        public Game(List<T> players)
        {
            _players = players;
        }

        public T[] GetTop10Players()
        {
            T[] pl = _players.OrderBy(x => x.Score).Reverse().ToArray();
            T[] list;
            if (pl.Count() >= 10)
                list = new T[10];
            else
                list = new T[pl.Count()];
            for (int i = 0; i < 10; i++)
            {
                if (pl[i] != null)
                    list[i] = pl[i];
                else
                    return list;
            }
            return list;
        }
    }
}