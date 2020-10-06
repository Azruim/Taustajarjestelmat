using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApi
{
    public class Income
    {
        public Guid id { get; set; }
        [Range(0f, 100000f)]
        public string description { get; set; }
        public float amount { get; set; }
        public DateTime transactionDate { get; set; }
        public bool isMonthly { get; set; }
        public List<string> tag { get; set; }

        public Income()
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