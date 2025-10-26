using BankingServiceSimulation.Application.DTOs;
using BankingServiceSimulation.Application.Services;
using BankingServiceSimulation.Infrastructure.Persistence;
using BankingServiceSimulation.Infrastructure.Repositories;

namespace BankingServiceSimulation.Tests.Services;

public class AccountServiceTests
{
    [Fact]
    public async Task CreateAccountAsync_ShouldCreateAccount_WithValidData()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var service = new AccountService(repository);
        var createDto = new CreateAccountDto("John Doe", 1000m);

        // Act
        var result = await service.CreateAccountAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.AccountHolderName);
        Assert.Equal(1000m, result.Balance);
        Assert.NotEmpty(result.AccountNumber);
    }

    [Fact]
    public async Task CreateAccountAsync_ShouldThrowException_WhenNameIsEmpty()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var service = new AccountService(repository);
        var createDto = new CreateAccountDto("", 1000m);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            service.CreateAccountAsync(createDto));
        
        Assert.Equal("Account holder name is required", exception.Message);
    }

    [Fact]
    public async Task CreateAccountAsync_ShouldThrowException_WhenInitialDepositIsNegative()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var service = new AccountService(repository);
        var createDto = new CreateAccountDto("John Doe", -100m);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            service.CreateAccountAsync(createDto));
        
        Assert.Equal("Initial deposit cannot be negative", exception.Message);
    }

    [Fact]
    public async Task GetAccountByNumberAsync_ShouldReturnAccount_WhenExists()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var service = new AccountService(repository);
        var created = await service.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));

        // Act
        var result = await service.GetAccountByNumberAsync(created.AccountNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(created.AccountNumber, result.AccountNumber);
    }

    [Fact]
    public async Task GetAccountByNumberAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var service = new AccountService(repository);

        // Act
        var result = await service.GetAccountByNumberAsync("9999999999");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBalanceAsync_ShouldReturnBalance()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var service = new AccountService(repository);
        var account = await service.CreateAccountAsync(new CreateAccountDto("John Doe", 1500m));

        // Act
        var balance = await service.GetBalanceAsync(account.AccountNumber);

        // Assert
        Assert.Equal(1500m, balance);
    }

    [Fact]
    public async Task GetBalanceAsync_ShouldThrowException_WhenAccountNotFound()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var service = new AccountService(repository);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            service.GetBalanceAsync("9999999999"));
        
        Assert.Contains("Account not found", exception.Message);
    }

    [Fact]
    public async Task GetAllAccountsAsync_ShouldReturnAllAccounts()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var service = new AccountService(repository);
        await service.CreateAccountAsync(new CreateAccountDto("John Doe", 1000m));
        await service.CreateAccountAsync(new CreateAccountDto("Jane Smith", 2000m));

        // Act
        var accounts = await service.GetAllAccountsAsync();

        // Assert
        Assert.Equal(2, accounts.Count());
    }
}

