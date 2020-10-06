using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BudgetApi.Controllers
{
    [ApiController]
    [Route("users/{userId}/[Controller]")]
    public class IncomeController
    {
        IRepository _repository = new MongoDbRepository();

        [HttpPost]
        public Task<Income> CreateIncome(Guid userId, [FromBody] NewTransaction newTransaction)
        {
            var income = new Income();
            income.id = Guid.NewGuid();
            income.description = newTransaction.description;
            income.amount = newTransaction.amount;
            income.isMonthly = newTransaction.isMonthly;
            income.tag = newTransaction.tag;

            return _repository.CreateIncome(userId, income);
        }

        [HttpPut("{incomeId}")]
        public Task<Income> UpdateIncome(Guid userId, Guid incomeId, [FromBody] ModifiedTransaction modifiedTransaction)
        {
            var income = new Income();
            income.id = incomeId;
            income.description = modifiedTransaction.description;
            income.amount = modifiedTransaction.amount;
            income.transactionDate = modifiedTransaction.transactionDate;
            income.isMonthly = modifiedTransaction.isMonthly;
            income.tag = modifiedTransaction.tag;

            return _repository.UpdateIncome(userId, income);
        }

        [HttpDelete("{incomeId}")]
        public Task<Income> DeleteIncome(Guid userId, Guid incomeId)
        {
            return _repository.DeleteIncome(userId, incomeId);
        }

        [HttpGet("{incomeId}")]
        public Task<Income> GetIncome(Guid userId, Guid incomeId)
        {
            return _repository.GetIncome(userId, incomeId);
        }

        [HttpGet("overview")]
        public string GetIncomeOverview(Guid userId, [FromQuery] int days = 30)
        {
            var user =  _repository.GetUser(userId).Result;
            var income = user.income.FindAll(i => i.transactionDate > DateTime.Now - new TimeSpan(days,0,0,0));
            var maxIncome = income.Aggregate((max, i) => i.amount > max.amount ? i : max);
            var monthlyIncome = income.FindAll(i => i.isMonthly == true).Sum(i => i.amount);
            var otherIncome = income.FindAll(i => i.isMonthly == false).Sum(i => i.amount);
            var transactions = "";

            foreach (var entry in income)
            {
                var temp = "";
                for (int i = 0; i < entry.tag.Count; i++)
                {
                    temp += entry.tag[i];
                    if (i < entry.tag.Count - 1)
                        temp += ", "; 
                }
                transactions += entry.description + ": " + MathF.Round(entry.amount, 2) + " €" + "\t" + "tagit: " + temp + "\n";
            }
            
            return user.name + "\t" + " Income overview last " + days + " days" + "\n" + 
            "Monthly income: " + monthlyIncome + "\t" + "Other income: " + otherIncome + "\n" + 
            "Max single income: " + maxIncome.description + ", " + maxIncome.amount + "\n" + 
            transactions;
        }

        [HttpGet]
        public string GetIncomeWithTag(Guid userId, [FromQuery] string tag = "")
        {
            var user =  _repository.GetUser(userId).Result;
            List<Income> income;
            string start;

            if (tag == "")
            {
                income = user.income;
                start = "All income";
            }
            else
            {
                income = user.income.FindAll(i => i.tag.Contains(tag)); 
                start = "Income with tag: " + tag;   
            }

            var totalIncome = income.Sum(i => i.amount);

            var transactions = "";
            foreach (var entry in income)
            {
                var temp = "";
                for (int i = 0; i < entry.tag.Count; i++)
                {
                    temp += entry.tag[i];
                    if (i < entry.tag.Count - 1)
                        temp += ", "; 
                }
                transactions += entry.description + ": " + MathF.Round(entry.amount, 2) + " €" + "\t" + "tags: " + temp + "\n";
            }

            return user.name + "\t" + start + "\n" +
            transactions + "\n" +
            "Summed income: " + totalIncome + " €";
        }
    }
}