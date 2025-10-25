namespace BankingServiceSimulation.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string AccountNumber { get; private set; }
    public string AccountHolderName { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Constructor for creating new accounts
    public Account(string accountNumber, string accountHolderName, decimal initialBalance)
    {
        Id = Guid.NewGuid();
        AccountNumber = accountNumber;
        AccountHolderName = accountHolderName;
        Balance = initialBalance;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Constructor for reconstitution from storage
    private Account(Guid id, string accountNumber, string accountHolderName, decimal balance, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        AccountNumber = accountNumber;
        AccountHolderName = accountHolderName;
        Balance = balance;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}

