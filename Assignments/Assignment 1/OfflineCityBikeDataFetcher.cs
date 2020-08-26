using System;
using System.IO;
using System.Threading.Tasks;

namespace Assignment_1
{
    public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public Task<int> GetBikeCountInStation(string stationName)
        {
            string[] file = File.ReadAllLines("bikedata.txt");
            foreach (string line in file)
            {
                string name = line.Split(" : ")[0];
                if (name == stationName)
                    return Task.FromResult(Int32.Parse(line.Split(" : ")[1]));
            }
            throw new StationNotFoundException(stationName);
         }
    }
}