using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        IRepository _repository = new MongoDbRepository();

        [HttpGet("players/{playerId}/items/all")]
        public Task<Item[]> GetAllItems(Guid playerId)
        {
            return _repository.GetAllItems(playerId);
        }

        [HttpGet("players/{playerId}/items/{itemId}")]
        public Task<Item> GetItem(Guid playerI, Guid itemId)
        {
            return _repository.GetItem(playerI, itemId);
        }

        [HttpPost("players/{playerId}/items")]
        public Task<Item> CreateItem(Guid playerId, [FromBody] Item item)
        {
            return _repository.CreateItem(playerId, item);
        }

        [HttpPut("players/{playerId}/items")]
        public Task<Item> UpdateItem(Guid playerId, [FromBody] Item item)
        {
            return _repository.UpdateItem(playerId, item);
        }

        [HttpDelete("players/{playerId}/items")]
        public Task<Item> DeleteItem(Guid playerId, [FromBody] Item item)
        {
            return _repository.DeleteItem(playerId, item);
        }
    }
}