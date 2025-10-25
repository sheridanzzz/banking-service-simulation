using BankingServiceSimulation.Domain.Entities;

namespace BankingServiceSimulation.Domain.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);
    Task<Account?> GetByAccountNumberAsync(string accountNumber);
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account> AddAsync(Account account);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(string accountNumber);
}

