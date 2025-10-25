using BankingServiceSimulation.Domain.Entities;

namespace BankingServiceSimulation.Infrastructure.Persistence;

public class InMemoryDatabase
{
    private readonly Dictionary<Guid, Account> _accounts = new();
    private readonly Dictionary<string, Guid> _accountNumberIndex = new();
    private readonly List<Transaction> _transactions = new();
    private readonly object _lock = new();

    public Dictionary<Guid, Account> Accounts => _accounts;
    public Dictionary<string, Guid> AccountNumberIndex => _accountNumberIndex;
    public List<Transaction> Transactions => _transactions;
    public object Lock => _lock;
}

