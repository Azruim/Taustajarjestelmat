using System;

namespace BudgetApi
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}