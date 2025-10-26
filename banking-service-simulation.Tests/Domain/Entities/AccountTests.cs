using BankingServiceSimulation.Domain.Entities;

namespace BankingServiceSimulation.Tests.Domain.Entities;

public class AccountTests
{
    [Fact]
    public void Account_ShouldCreate_WithValidData()
    {
        // Arrange & Act
        var account = new Account("1234567890", "John Doe", 1000m);

        // Assert
        Assert.NotEqual(Guid.Empty, account.Id);
        Assert.Equal("1234567890", account.AccountNumber);
        Assert.Equal("John Doe", account.AccountHolderName);
        Assert.Equal(1000m, account.Balance);
        Assert.True(account.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Account_ShouldThrowException_WhenAccountNumberIsEmpty()
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            new Account("", "John Doe", 1000m));
        
        Assert.Equal("Account number cannot be empty", exception.Message);
    }

    [Fact]
    public void Account_ShouldThrowException_WhenAccountHolderNameIsEmpty()
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            new Account("1234567890", "", 1000m));
        
        Assert.Equal("Account holder name cannot be empty", exception.Message);
    }

    [Fact]
    public void Account_ShouldThrowException_WhenInitialBalanceIsNegative()
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            new Account("1234567890", "John Doe", -100m));
        
        Assert.Equal("Initial balance cannot be negative", exception.Message);
    }

    [Fact]
    public void Deposit_ShouldIncreaseBalance()
    {
        // Arrange
        var account = new Account("1234567890", "John Doe", 1000m);

        // Act
        account.Deposit(500m);

        // Assert
        Assert.Equal(1500m, account.Balance);
    }

    [Fact]
    public void Deposit_ShouldThrowException_WhenAmountIsZero()
    {
        // Arrange
        var account = new Account("1234567890", "John Doe", 1000m);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => account.Deposit(0m));
        Assert.Equal("Deposit amount must be greater than zero", exception.Message);
    }

    [Fact]
    public void Deposit_ShouldThrowException_WhenAmountIsNegative()
    {
        // Arrange
        var account = new Account("1234567890", "John Doe", 1000m);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => account.Deposit(-100m));
        Assert.Equal("Deposit amount must be greater than zero", exception.Message);
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalance()
    {
        // Arrange
        var account = new Account("1234567890", "John Doe", 1000m);

        // Act
        account.Withdraw(300m);

        // Assert
        Assert.Equal(700m, account.Balance);
    }

    [Fact]
    public void Withdraw_ShouldThrowException_WhenInsufficientFunds()
    {
        // Arrange
        var account = new Account("1234567890", "John Doe", 1000m);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => account.Withdraw(1500m));
        Assert.Contains("Insufficient funds", exception.Message);
    }

    [Fact]
    public void Withdraw_ShouldThrowException_WhenAmountIsZero()
    {
        // Arrange
        var account = new Account("1234567890", "John Doe", 1000m);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => account.Withdraw(0m));
        Assert.Equal("Withdrawal amount must be greater than zero", exception.Message);
    }

    [Fact]
    public void Withdraw_ShouldThrowException_WhenAmountIsNegative()
    {
        // Arrange
        var account = new Account("1234567890", "John Doe", 1000m);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => account.Withdraw(-100m));
        Assert.Equal("Withdrawal amount must be greater than zero", exception.Message);
    }
}

