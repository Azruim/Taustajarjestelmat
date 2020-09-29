using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GameWebApi
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _PlayerCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("game");
            _PlayerCollection = database.GetCollection<Player>("players");
            _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            Player player = await GetPlayer(playerId);

            player.Items.Add(item);
            await UpdatePlayer(player);
            return item;
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            await _PlayerCollection.InsertOneAsync(player);
            return player;
        }

        public async Task<Item> DeleteItem(Guid playerId, Guid id)
        {
            Player player = await GetPlayer(playerId);
            int i = player.Items.FindIndex(x => x.Id == id);
            List<Item> items = player.Items.ToList();
            Item item = player.Items[i];
            items.RemoveAt(i);
            player.Items = items;
            await UpdatePlayer(player);
            return item;
        }

        public async Task<Player> DeletePlayer(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            return await _PlayerCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            Player player = await GetPlayer(playerId);
            return player.Items.ToArray();
        }

        public async Task<Player[]> GetAllPlayers(int? minScore)
        {
            List<Player> players;
            if (minScore != null)
            {
                var filter = Builders<Player>.Filter.Gt(p => p.Score, minScore);
                players = await _PlayerCollection.Find(filter).ToListAsync();
            }
            else
            {
                var filter = Builders<Player>.Filter.Empty;
                players = await _PlayerCollection.Find(filter).ToListAsync();
            }
            return players.ToArray();
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            Player player = await GetPlayer(playerId);
            if (player == null)
                throw new NotFoundException("Player not found!");
            return player.Items.Find(x => x.Id == itemId);
        }

        public Task<Player> GetPlayer(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
            return _PlayerCollection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetPlayersWithItem(ItemType itemType)
        {
            List<Player> players;
            var filter = Builders<Player>.Filter.ElemMatch<Item>(p => p.Items, Builders<Item>.Filter.Eq(i => i.itemType, itemType));
            players = await _PlayerCollection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player[]> GetPlayersWithName(string name)
        {
            List<Player> players;
            var filter = Builders<Player>.Filter.Eq(p => p.Name, name);
            players = await _PlayerCollection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Player[]> GetPlayersWithTag(string tag)
        {
            List<Player> players;
            var filter = Builders<Player>.Filter.AnyEq(p => p.Tags, tag);
            players = await _PlayerCollection.Find(filter).ToListAsync();
            return players.ToArray();
        }

        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {
            Player player = await GetPlayer(playerId);
            int i = player.Items.FindIndex(x => x.Id == item.Id);
            player.Items[i] = item;
            await UpdatePlayer(player);
            return item;
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
            await _PlayerCollection.ReplaceOneAsync(filter, player);
            return player;
        }
    }
}