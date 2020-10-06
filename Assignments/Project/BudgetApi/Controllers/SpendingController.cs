using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BudgetApi.Controllers
{
    [ApiController]
    [Route("users/{userId}/[Controller]")]
    public class SpendingController
    {
        IRepository _repository = new MongoDbRepository();

        [HttpPost]
        public Task<Spending> CreateSpending(Guid userId, [FromBody] NewTransaction newTransaction)
        {
            var spending = new Spending();
            spending.description = newTransaction.description;
            spending.amount = newTransaction.amount;
            spending.isMonthly = newTransaction.isMonthly;
            spending.tag = newTransaction.tag;

            return _repository.CreateSpending(userId, spending);
        }

        [HttpPut("{spendingId}")]
        public Task<Spending> UpdateSpending(Guid userId, Guid spendingId, [FromBody] ModifiedTransaction modifiedTransaction)
        {
            var spending = new Spending();
            spending.id = spendingId;
            spending.description = modifiedTransaction.description;
            spending.amount = modifiedTransaction.amount;
            spending.transactionDate = modifiedTransaction.transactionDate;
            spending.isMonthly = modifiedTransaction.isMonthly;
            spending.tag = modifiedTransaction.tag;

            return _repository.UpdateSpending(userId, spending);
        }

        [HttpDelete("{spendingId}")]
        public Task<Spending> DeleteSpending(Guid userId, Guid spendingId)
        {
            return _repository.DeleteSpending(userId, spendingId);
        }

        [HttpGet("{spendingId}")]
        public Task<Spending> GetSpending(Guid userId, Guid spendingId)
        {
            return _repository.GetSpending(userId, spendingId);
        }

        [HttpGet("overview")]
        public string GetSpendingOverview(Guid userId, [FromQuery] int days = 30)
        {
            var user =  _repository.GetUser(userId).Result;
            var spending = user.spending.FindAll(s => s.transactionDate > DateTime.Now - new TimeSpan(days,0,0,0));
            var maxSpending = spending.Aggregate((max, s) => s.amount > max.amount ? s : max);
            var monthlySpending = spending.FindAll(s => s.isMonthly == true).Sum(s => s.amount);
            var otherSpending = spending.FindAll(s => s.isMonthly == false).Sum(s => s.amount);
            string transactions = "";

            foreach (var entry in spending)
            {
                string temp = "";
                for (int i = 0; i < entry.tag.Count; i++)
                {
                    temp += entry.tag[i];
                    if (i < entry.tag.Count - 1)
                        temp += ", "; 
                }
                transactions += entry.description + ": " + MathF.Round(entry.amount, 2) + " €" + "\t" + "tagit: " + temp + "\n";
            }
            
            return user.name + "\t" + " Spending overview last " + days + " days" + "\n" + 
            "Monthly spending: " + monthlySpending + "\t" + "Other spending: " + otherSpending + "\n" + 
            "Max single spending: " + maxSpending.description + ", " + maxSpending.amount + "\n" + 
            transactions;
        }

        [HttpGet]
        public string GetSpendingWithTag(Guid userId, [FromQuery] string tag = "")
        {
            var user =  _repository.GetUser(userId).Result;
            List<Spending> spending;
            string start;

            if (tag == "")
            {
                spending = user.spending;
                start = "All spending";
            }
            else
            {
                spending = user.spending.FindAll(s => s.tag.Contains(tag)); 
                start = "Spending with tag: " + tag;   
            }

            var totalSpending = spending.Sum(s => s.amount);

            var transactions = "";
            foreach (var entry in spending)
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
            "Summed spending: " + totalSpending + " €";
        }
    }
}