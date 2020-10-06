using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApi
{
    public class Spending
    {
        public Guid id { get; set; }
        public string description { get; set; }
        [Range(0f, 100000f)]
        public float amount { get; set; }
        public DateTime transactionDate { get; set; }
        public bool isMonthly { get; set; }
        public List<string> tag { get; set; }

        public Spending()
        {
            this.id = Guid.NewGuid();
            this.description = "";
            this.amount = 0f;
            this.transactionDate = DateTime.Now;
            this.isMonthly = false;
            this.tag = new List<string>();
        }
    }
}