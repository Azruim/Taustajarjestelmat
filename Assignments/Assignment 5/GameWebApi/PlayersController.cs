using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi
{
    [ApiController]
    [Route("[Controller]")]
    public class PlayersController : ControllerBase
    {
        IRepository _repository = new MongoDbRepository();

        [HttpGet]
        public Task<Player[]> GetAllPlayers([FromQuery] int? minScore = null)
        {
            return _repository.GetAllPlayers(minScore);
        }

        [HttpGet("{id:int}")]
        public Task<Player> GetPlayer(Guid id)
        {
            return _repository.GetPlayer(id);
        }

        [HttpGet("{name}")]
        public Task<Player[]> GetPlayersWithName(string name)
        {
            return _repository.GetPlayersWithName(name);
        }

        [HttpGet("itemType")]
        public Task<Player[]> GetPlayersWithItem([FromQuery] ItemType itemType)
        {
            return _repository.GetPlayersWithItem(itemType);
        }

        [HttpGet("tag")]
        public Task<Player[]> GetPlayersWithTag([FromQuery] string tag)
        {
            return _repository.GetPlayersWithTag(tag);
        }

        [HttpPost]
        public Task<Player> CreatePlayer([FromBody] NewPlayer newPlayer)
        {
            Player player = new Player();
            player.Name = newPlayer.Name;
            return _repository.CreatePlayer(player);
        }

        [HttpPut("{id}")]
        public Task<Player> UpdatePlayer(Guid id, [FromBody] ModifiedPlayer modifiedPlayer)
        {
            Player player = new Player();
            player.Id = id;
            player.Name = modifiedPlayer.Name;
            player.Score = modifiedPlayer.Score;
            player.Level = modifiedPlayer.Level;
            player.IsBanned = modifiedPlayer.IsBanned;
            player.Tags = modifiedPlayer.Tags;
            return _repository.UpdatePlayer(player);
        }

        [HttpDelete("{id}")]
        public Task<Player> DeletePlayer(Guid id)
        {
            return _repository.DeletePlayer(id);
        }
    }
}