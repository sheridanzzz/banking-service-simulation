using BankingServiceSimulation.Application.DTOs;
using BankingServiceSimulation.Application.Services;
using BankingServiceSimulation.Infrastructure.Persistence;
using BankingServiceSimulation.Infrastructure.Repositories;

namespace BankingServiceSimulation.Tests.Services;

public class BankingServiceTests
{
    [Fact]
    public async Task DepositAsync_ShouldIncreaseBalance()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account = await accountService.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));

        // Act
        var transaction = await bankingService.DepositAsync(account.AccountNumber, 500m);

        // Assert
        var balance = await accountService.GetBalanceAsync(account.AccountNumber);
        Assert.Equal(1500m, balance);
        Assert.Equal(500m, transaction.Amount);
    }

    [Fact]
    public async Task DepositAsync_ShouldThrowException_WhenAmountIsZero()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account = await accountService.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            bankingService.DepositAsync(account.AccountNumber, 0m));
        
        Assert.Equal("Deposit amount must be greater than zero", exception.Message);
    }

    [Fact]
    public async Task DepositAsync_ShouldThrowException_WhenAccountNotFound()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var bankingService = new BankingService(accountRepository, transactionRepository);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            bankingService.DepositAsync("9999999999", 100m));
        
        Assert.Contains("Account not found", exception.Message);
    }

    [Fact]
    public async Task WithdrawAsync_ShouldDecreaseBalance()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account = await accountService.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));

        // Act
        var transaction = await bankingService.WithdrawAsync(account.AccountNumber, 300m);

        // Assert
        var balance = await accountService.GetBalanceAsync(account.AccountNumber);
        Assert.Equal(700m, balance);
        Assert.Equal(300m, transaction.Amount);
    }

    [Fact]
    public async Task WithdrawAsync_ShouldThrowException_WhenInsufficientFunds()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account = await accountService.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            bankingService.WithdrawAsync(account.AccountNumber, 1500m));
        
        Assert.Contains("Insufficient funds", exception.Message);
    }

    [Fact]
    public async Task TransferAsync_ShouldTransferMoney_BetweenAccounts()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account1 = await accountService.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));
        var account2 = await accountService.CreateAccountAsync(new CreateAccountDto("Jane Smith", 500m));

        // Act
        await bankingService.TransferAsync(account1.AccountNumber, account2.AccountNumber, 300m);

        // Assert
        var balance1 = await accountService.GetBalanceAsync(account1.AccountNumber);
        var balance2 = await accountService.GetBalanceAsync(account2.AccountNumber);
        Assert.Equal(700m, balance1);
        Assert.Equal(800m, balance2);
    }

    [Fact]
    public async Task TransferAsync_ShouldThrowException_WhenInsufficientFunds()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account1 = await accountService.CreateAccountAsync(new CreateAccountDto("John Doe", 500m));
        var account2 = await accountService.CreateAccountAsync(new CreateAccountDto("Jane Smith", 1000m));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            bankingService.TransferAsync(account1.AccountNumber, account2.AccountNumber, 1000m));
        
        Assert.Contains("Insufficient funds", exception.Message);
    }

    [Fact]
    public async Task TransferAsync_ShouldThrowException_WhenTransferringToSameAccount()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account = await accountService.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            bankingService.TransferAsync(account.AccountNumber, account.AccountNumber, 100m));
        
        Assert.Equal("Cannot transfer to the same account", exception.Message);
    }

    [Fact]
    public async Task TransferAsync_ShouldThrowException_WhenSourceAccountNotFound()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account2 = await accountService.CreateAccountAsync(new CreateAccountDto("Jane Smith", 500m));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            bankingService.TransferAsync("9999999999", account2.AccountNumber, 100m));
        
        Assert.Contains("Source account not found", exception.Message);
    }

    [Fact]
    public async Task TransferAsync_ShouldThrowException_WhenDestinationAccountNotFound()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var accountRepository = new InMemoryAccountRepository(database);
        var transactionRepository = new InMemoryTransactionRepository(database);
        var accountService = new AccountService(accountRepository);
        var bankingService = new BankingService(accountRepository, transactionRepository);
        
        var account1 = await accountService.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            bankingService.TransferAsync(account1.AccountNumber, "9999999999", 100m));
        
        Assert.Contains("Destination account not found", exception.Message);
    }
}

