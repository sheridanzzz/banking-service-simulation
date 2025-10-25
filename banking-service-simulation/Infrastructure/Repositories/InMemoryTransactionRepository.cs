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
        // TODO: Implement add transaction logic
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId)
    {
        // TODO: Implement get by account id logic
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetAllAsync()
    {
        // TODO: Implement get all logic
        throw new NotImplementedException();
    }
}

