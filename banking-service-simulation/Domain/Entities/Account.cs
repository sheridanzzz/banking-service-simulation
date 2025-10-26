namespace BankingServiceSimulation.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string AccountNumber { get; private set; }
    public string AccountHolderName { get; private set; }
    public decimal Balance { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Account(string accountNumber, string accountHolderName, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new Exception("Account number cannot be empty");
        
        if (string.IsNullOrWhiteSpace(accountHolderName))
            throw new Exception("Account holder name cannot be empty");
        
        if (initialBalance < 0)
            throw new Exception("Initial balance cannot be negative");

        Id = Guid.NewGuid();
        AccountNumber = accountNumber;
        AccountHolderName = accountHolderName;
        Balance = initialBalance;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Deposit amount must be greater than zero");

        Balance += amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Withdrawal amount must be greater than zero");

        if (amount > Balance)
            throw new Exception($"Insufficient funds. Available: {Balance:C}, Requested: {amount:C}");

        Balance -= amount;
        UpdatedAt = DateTime.UtcNow;
    }
}

