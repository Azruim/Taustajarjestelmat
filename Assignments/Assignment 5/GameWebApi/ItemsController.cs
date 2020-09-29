using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi
{
    [ApiController]
    [Route("players/{playerId}/[controller]")]
    public class ItemsController : ControllerBase
    {
        IRepository _repository = new MongoDbRepository();

        [HttpGet]
        public Task<Item[]> GetAllItems(Guid playerId)
        {
            return _repository.GetAllItems(playerId);
        }

        [HttpGet("{id}")]
        public Task<Item> GetItem(Guid playerI, Guid id)
        {
            return _repository.GetItem(playerI, id);
        }

        [HttpPost]
        public Task<Item> CreateItem(Guid playerId, [FromBody] NewItem newItem)
        {
            Item item = new Item();
            item.Level = newItem.Level;
            item.itemType = newItem.itemType;
            return _repository.CreateItem(playerId, item);
        }

        [HttpPut("{id}")]
        public Task<Item> UpdateItem(Guid playerId, Guid id, [FromBody] ModifiedItem modifiedItem)
        {
            Item item = new Item();
            item.Id = id;
            item.Level = modifiedItem.Level;
            item.itemType = modifiedItem.itemType;
            return _repository.UpdateItem(playerId, item);
        }

        [HttpDelete("{id}")]
        public Task<Item> DeleteItem(Guid playerId, Guid id)
        {
            return _repository.DeleteItem(playerId, id);
        }
    }
}