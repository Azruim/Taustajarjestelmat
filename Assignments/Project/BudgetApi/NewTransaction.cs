using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetApi
{
    public class NewTransaction
    {
        public string description { get; set; }
        [Range(0f, 100000f)]
        public float amount { get; set; }
        public bool isMonthly { get; set; }
        public List<string> tag { get; set; }
    }
}