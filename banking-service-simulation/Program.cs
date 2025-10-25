using BankingServiceSimulation.Application.Interfaces;
using BankingServiceSimulation.Application.Services;
using BankingServiceSimulation.Domain.Interfaces;
using BankingServiceSimulation.Infrastructure.Persistence;
using BankingServiceSimulation.Infrastructure.Repositories;

namespace BankingServiceSimulation;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Initialize Infrastructure Layer
        var database = new InMemoryDatabase();
        
        // Initialize Repositories
        IAccountRepository accountRepository = new InMemoryAccountRepository(database);
        ITransactionRepository transactionRepository = new InMemoryTransactionRepository(database);
        
        // Initialize Services
        IAccountService accountService = new AccountService(accountRepository);
        IBankingService bankingService = new BankingService(accountRepository, transactionRepository);
        
        // TODO: Application entry point
        
        await Task.CompletedTask;
    }
}