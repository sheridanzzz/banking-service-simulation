using BankingServiceSimulation.Domain.Entities;
using BankingServiceSimulation.Domain.Interfaces;
using BankingServiceSimulation.Infrastructure.Persistence;

namespace BankingServiceSimulation.Infrastructure.Repositories;

public class InMemoryTransactionRepository : ITransactionRepository
{
    private readonly InMemoryDatabase _database;

    public InMemoryTransactionRepository(InMemoryDatabase database)
    {
        _database = database;
    }

    public Task<Transaction> AddAsync(Transaction transaction)
    {
        lock (_database.Lock)
        {
            _database.Transactions.Add(transaction);
            return Task.FromResult(transaction);
        }
    }

    public Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId)
    {
        lock (_database.Lock)
        {
            var transactions = _database.Transactions
                .Where(t => t.AccountId == accountId || t.RelatedAccountId == accountId)
                .OrderByDescending(t => t.Timestamp)
                .ToList();
            return Task.FromResult<IEnumerable<Transaction>>(transactions);
        }
    }

    public Task<IEnumerable<Transaction>> GetAllAsync()
    {
        lock (_database.Lock)
        {
            var transactions = _database.Transactions
                .OrderByDescending(t => t.Timestamp)
                .ToList();
            return Task.FromResult<IEnumerable<Transaction>>(transactions);
        }
    }
}

