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
        // TODO: Implement get by id logic
        throw new NotImplementedException();
    }

    public Task<Account?> GetByAccountNumberAsync(string accountNumber)
    {
        // TODO: Implement get by account number logic
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Account>> GetAllAsync()
    {
        // TODO: Implement get all logic
        throw new NotImplementedException();
    }

    public Task<Account> AddAsync(Account account)
    {
        // TODO: Implement add logic
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Account account)
    {
        // TODO: Implement update logic
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        // TODO: Implement delete logic
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string accountNumber)
    {
        // TODO: Implement exists logic
        throw new NotImplementedException();
    }
}

