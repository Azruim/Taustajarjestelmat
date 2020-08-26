using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Assignment_1
{
    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {
            var client = new HttpClient();
            string json = await client.GetStringAsync("http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            BikeRentalStationList list = JsonConvert.DeserializeObject<BikeRentalStationList>(json);
            foreach (Stations stations in list.stations)
            {
                if (stations.name == stationName)
                    return stations.bikesAvailable;
            }
            throw new StationNotFoundException(stationName);
        }
    }
}