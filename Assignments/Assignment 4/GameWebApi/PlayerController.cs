using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        IRepository _repository = new MongoDbRepository();

        [HttpGet("/players/all")]
        public Task<Player[]> GetAllPlayers()
        {
            return _repository.GetAllPlayers();
        }

        [HttpGet("/players")]
        public Task<Player> GetPlayer([FromBody] Guid playerId)
        {
            return _repository.GetPlayer(playerId);
        }

        [HttpPost("/players")]
        public Task<Player> CreatePlayer([FromBody] Player player)
        {
            return _repository.CreatePlayer(player);
        }

        [HttpPut("/players")]
        public Task<Player> UpdatePlayer([FromBody] Player player)
        {
            return _repository.UpdatePlayer(player);
        }

        [HttpDelete("/players/{id}")]
        public Task<Player> DeletePlayer(Guid playerId)
        {
            return _repository.DeletePlayer(playerId);
        }
    }
}