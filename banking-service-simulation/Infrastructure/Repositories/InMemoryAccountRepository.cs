using BankingServiceSimulation.Domain.Entities;
using BankingServiceSimulation.Domain.Interfaces;
using BankingServiceSimulation.Infrastructure.Persistence;

namespace BankingServiceSimulation.Infrastructure.Repositories;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly InMemoryDatabase _database;

    public InMemoryAccountRepository(InMemoryDatabase database)
    {
        _database = database;
    }

    public Task<Account?> GetByIdAsync(Guid id)
    {
        lock (_database.Lock)
        {
            _database.Accounts.TryGetValue(id, out var account);
            return Task.FromResult(account);
        }
    }

    public Task<Account?> GetByAccountNumberAsync(string accountNumber)
    {
        lock (_database.Lock)
        {
            if (_database.AccountNumberIndex.TryGetValue(accountNumber, out var accountId))
            {
                _database.Accounts.TryGetValue(accountId, out var account);
                return Task.FromResult(account);
            }
            return Task.FromResult<Account?>(null);
        }
    }

    public Task<IEnumerable<Account>> GetAllAsync()
    {
        lock (_database.Lock)
        {
            var accounts = _database.Accounts.Values.ToList();
            return Task.FromResult<IEnumerable<Account>>(accounts);
        }
    }

    public Task<Account> AddAsync(Account account)
    {
        lock (_database.Lock)
        {
            _database.Accounts[account.Id] = account;
            _database.AccountNumberIndex[account.AccountNumber] = account.Id;
            return Task.FromResult(account);
        }
    }

    public Task UpdateAsync(Account account)
    {
        lock (_database.Lock)
        {
            _database.Accounts[account.Id] = account;
            return Task.CompletedTask;
        }
    }

    public Task DeleteAsync(Guid id)
    {
        lock (_database.Lock)
        {
            if (_database.Accounts.TryGetValue(id, out var account))
            {
                _database.Accounts.Remove(id);
                _database.AccountNumberIndex.Remove(account.AccountNumber);
            }
            return Task.CompletedTask;
        }
    }

    public Task<bool> ExistsAsync(string accountNumber)
    {
        lock (_database.Lock)
        {
            var exists = _database.AccountNumberIndex.ContainsKey(accountNumber);
            return Task.FromResult(exists);
        }
    }
}

