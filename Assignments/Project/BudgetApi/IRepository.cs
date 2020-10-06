using System;
using System.Threading.Tasks;

namespace BudgetApi
{
    public interface IRepository
    {
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(Guid userId);
        Task<User> GetUser(Guid userId);
        Task<User[]> GetAllUsers();

        Task<Income> CreateIncome(Guid userId, Income income);
        Task<Income> UpdateIncome(Guid userId, Income income);
        Task<Income> DeleteIncome(Guid userId, Guid incomeId);
        Task<Income> GetIncome(Guid userId, Guid incomeId);

        Task<Spending> CreateSpending(Guid userId, Spending spending);
        Task<Spending> UpdateSpending(Guid userId, Spending spending);
        Task<Spending> DeleteSpending(Guid userId, Guid spendingId);
        Task<Spending> GetSpending(Guid userId, Guid spendingId);
    }
}