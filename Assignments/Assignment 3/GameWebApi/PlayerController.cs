using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameWebApi
{
    [Route("api/players")]
    public class PlayerController : ControllerBase
    {
        IRepository _repository = new FileRepository();

        [HttpGet("/players/all")]
        public Task<Player[]> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpGet("/players")]
        public Task<Player> Get([FromBody] Guid id)
        {
            return _repository.Get(id);
        }

        [HttpPost("/players")]
        public Task<Player> Create([FromBody] NewPlayer player)
        {
            return _repository.Create(player);
        }

        [HttpPut("/players/{id}")]
        public Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player)
        {
            return _repository.Modify(id, player);
        }

        [HttpDelete("/players/{id}")]
        public Task<Player> Delete(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}