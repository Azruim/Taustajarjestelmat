using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BudgetApi
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<User> _UserCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Budget");
            _UserCollection = database.GetCollection<User>("Users");
            _bsonDocumentCollection = database.GetCollection<BsonDocument>("Users");
        }

        #region User

        public async Task<User> CreateUser(User user)
        {
            await _UserCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, user.id);
            var result = await _UserCollection.ReplaceOneAsync(filter, user);
            if (result.MatchedCount == 1)
                return user;
            return null;
        }

        public async Task<User> DeleteUser(Guid userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, userId);
            return await _UserCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task<User> GetUser(Guid userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, userId);
            var result = await _UserCollection.Find(filter).FirstAsync();
            if (result == null)
                throw new NotFoundException("User does not exist!");
            return result;
        }

        public async Task<User[]> GetAllUsers()
        {
            var filter = Builders<User>.Filter.Empty;
            List<User> users = await _UserCollection.Find(filter).ToListAsync();
            return users.ToArray();
        }

        #endregion
        #region Income

        public async Task<Income> CreateIncome(Guid userId, Income income)
        {
            var user = await GetUser(userId);
            user.income.Add(income);
            await UpdateUser(user);
            return income;
        }

        public async Task<Income> UpdateIncome(Guid userId, Income income)
        {
            var user = await GetUser(userId);
            var i = user.income.FindIndex(i => i.id == income.id);
            user.income[i] = income;
            await UpdateUser(user);
            return income;
        }

        public async Task<Income> DeleteIncome(Guid userId, Guid incomeId)
        {
            var user = await GetUser(userId);
            var i = user.income.FindIndex(i => i.id == incomeId);
            List<Income> incomeList = user.income.ToList();
            var income = user.income[i];
            incomeList.RemoveAt(i);
            user.income = incomeList;
            await UpdateUser(user);
            return income;
        }

        public async Task<Income> GetIncome(Guid userId, Guid incomeId)
        {
            var user = await GetUser(userId);
            return user.income.Find(i => i.id == incomeId);
        }

        #endregion
        #region Spending

        public async Task<Spending> CreateSpending(Guid userId, Spending spending)
        {
            var user = await GetUser(userId);
            user.spending.Add(spending);
            await UpdateUser(user);
            return spending;
        }

        public async Task<Spending> UpdateSpending(Guid userId, Spending spending)
        {
            var user = await GetUser(userId);
            var i = user.spending.FindIndex(s => s.id == spending.id);
            user.spending[i] = spending;
            await UpdateUser(user);
            return spending;
        }

        public async Task<Spending> DeleteSpending(Guid userId, Guid spendingId)
        {
            var user = await GetUser(userId);
            var i = user.spending.FindIndex(s => s.id == spendingId);
            List<Spending> spendingList = user.spending.ToList();
            var spending = user.spending[i];
            spendingList.RemoveAt(i);
            user.spending = spendingList;
            await UpdateUser(user);
            return spending;
        }

        public async Task<Spending> GetSpending(Guid userId, Guid spendingId)
        {
            var user = await GetUser(userId);
            return user.spending.Find(s => s.id == spendingId);
        }

        #endregion
    }
}