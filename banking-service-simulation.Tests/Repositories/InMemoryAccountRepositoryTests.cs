using BankingServiceSimulation.Domain.Entities;
using BankingServiceSimulation.Infrastructure.Persistence;
using BankingServiceSimulation.Infrastructure.Repositories;

namespace BankingServiceSimulation.Tests.Repositories;

public class InMemoryAccountRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldAddAccount()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var account = new Account("1234567890", "John Doe", 1000m);

        // Act
        var result = await repository.AddAsync(account);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(account.Id, result.Id);
        Assert.Equal(1, database.Accounts.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAccount_WhenExists()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var account = new Account("1234567890", "John Doe", 1000m);
        await repository.AddAsync(account);

        // Act
        var result = await repository.GetByIdAsync(account.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(account.Id, result.Id);
        Assert.Equal("John Doe", result.AccountHolderName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);

        // Act
        var result = await repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByAccountNumberAsync_ShouldReturnAccount_WhenExists()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var account = new Account("1234567890", "John Doe", 1000m);
        await repository.AddAsync(account);

        // Act
        var result = await repository.GetByAccountNumberAsync("1234567890");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1234567890", result.AccountNumber);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAccounts()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        await repository.AddAsync(new Account("1111111111", "John Doe", 1000m));
        await repository.AddAsync(new Account("2222222222", "Jane Smith", 2000m));

        // Act
        var results = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, results.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateAccount()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var account = new Account("1234567890", "John Doe", 1000m);
        await repository.AddAsync(account);

        // Act
        account.Deposit(500m);
        await repository.UpdateAsync(account);

        // Assert
        var updated = await repository.GetByIdAsync(account.Id);
        Assert.Equal(1500m, updated!.Balance);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenAccountExists()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);
        var account = new Account("1234567890", "John Doe", 1000m);
        await repository.AddAsync(account);

        // Act
        var exists = await repository.ExistsAsync("1234567890");

        // Assert
        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalse_WhenAccountDoesNotExist()
    {
        // Arrange
        var database = new InMemoryDatabase();
        var repository = new InMemoryAccountRepository(database);

        // Act
        var exists = await repository.ExistsAsync("9999999999");

        // Assert
        Assert.False(exists);
    }
}

