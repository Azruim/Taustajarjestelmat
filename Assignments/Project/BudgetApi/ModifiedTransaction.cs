using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApi
{
    public class ModifiedTransaction
    {
        public string description { get; set; }
        [Range(0f, 100000f)]
        public float amount { get; set; }
        [DateValidation]
        public DateTime transactionDate { get; set; }
        public bool isMonthly { get; set; }
        public List<string> tag { get; set; }
    }
}