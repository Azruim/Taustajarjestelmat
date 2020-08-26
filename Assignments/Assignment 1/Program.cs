using System;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                foreach (var ch in args)
                    if (ch.Any(char.IsDigit))
                        throw new ArgumentException("Argument contains numbers!");

                if (args[1] == "realtime")
                {
                    RealTimeCityBikeDataFetcher rf = new RealTimeCityBikeDataFetcher();
                    int output = await rf.GetBikeCountInStation(args[0]);
                    Console.WriteLine(output);
                }
                else if (args[1] == "offline")
                {
                    OfflineCityBikeDataFetcher of = new OfflineCityBikeDataFetcher();
                    int output = await of.GetBikeCountInStation(args[0]);
                    Console.WriteLine(output);
                }
                else
                    throw new ArgumentException(args[1]);

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid argument: " + ex.Message);
            }
            catch (StationNotFoundException ex)
            { 
                Console.WriteLine("Not found: " + ex.Message);
            }       
        }
    }
    public interface ICityBikeDataFetcher
    {
        Task<int> GetBikeCountInStation(string stationName);
    }
}
