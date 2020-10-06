using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        IRepository _repository = new MongoDbRepository();

        [HttpPost]
        public Task<User> CreateUser([FromBody] NewUser newUser)
        {
            var user = new User();
            user.name = newUser.name;
            return _repository.CreateUser(user);
        }

        [HttpPut("{userId}")]
        public Task<User> UpdateUser(Guid userId, [FromBody] ModifiedUser modifiedUser)
        {
            var user = new User();
            user.id = userId;
            user.name = modifiedUser.name;
            return _repository.UpdateUser(user);
        }

        [HttpDelete("{userId}")]
        public Task<User> DeleteUser(Guid userId)
        {
            return _repository.DeleteUser(userId);
        }

        [HttpGet("{userId}")]
        public Task<User> GetUser(Guid userId)
        {
            return _repository.GetUser(userId);
        }

        [HttpGet]
        public Task<User[]> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        [HttpGet("{userId}/overview")]
        public string GetOverview(Guid userId, [FromQuery] int days = 30)
        {
            var user =  _repository.GetUser(userId).Result;
            var income = user.income.FindAll(i => i.transactionDate > DateTime.Now - new TimeSpan(days,0,0,0));
            var maxIncome = income.Aggregate((max, i) => i.amount > max.amount ? i : max);
            var monthlyIncome = income.FindAll(i => i.isMonthly == true).Sum(i => i.amount);
            var otherIncome = income.FindAll(i => i.isMonthly == false).Sum(i => i.amount);

            var spending = user.spending.FindAll(s => s.transactionDate > DateTime.Now - new TimeSpan(days,0,0,0));
            var maxSpending = spending.Aggregate((max, s) => s.amount > max.amount ? s : max);
            var monthlySpending = spending.FindAll(s => s.isMonthly == true).Sum(s => s.amount);
            var otherSpending = spending.FindAll(s => s.isMonthly == false).Sum(s => s.amount);
            
            
            return user.name + "\t" + "Total oveview last " + days + " days" + "\n" + 
            "Balance: " + (monthlyIncome - monthlySpending) + "\n" +
            "Monthly income: " + monthlyIncome + "\t" + "Other income: " + otherIncome + "\n" + 
            "Max single income: " + maxIncome.description + ", " + maxIncome.amount + "\n" + 
            "Monthly spending: " + monthlySpending + "\t" + "Other spending: " + otherSpending +  "\n" + 
            "Max single spending: " + maxSpending.description + ", " + maxSpending.amount + "\n" ;
        }
    }
}