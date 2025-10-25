namespace BankingServiceSimulation.Domain.Entities;

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}

public class Transaction
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public Guid? RelatedAccountId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string Description { get; private set; }

    public Transaction(Guid accountId, TransactionType type, decimal amount, string description, Guid? relatedAccountId = null)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        Type = type;
        Amount = amount;
        Description = description;
        RelatedAccountId = relatedAccountId;
        Timestamp = DateTime.UtcNow;
    }
}

