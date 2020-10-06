using System;
using System.Collections.Generic;

namespace BudgetApi
{
    public class User
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public List<Income> income { get; set; }
        public List<Spending> spending { get; set; }

        public User()
        {
            this.id = Guid.NewGuid();
            this.name = "";
            this.income = new List<Income>();
            this.spending = new List<Spending>();
        }
    }

    public class NewUser
    {
        public string name { get; set; }
    }

    public class ModifiedUser
    {
        public string name { get; set; }
    }
}