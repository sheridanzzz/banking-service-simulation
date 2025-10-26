using BankingServiceSimulation.Application.DTOs;
using BankingServiceSimulation.Application.Interfaces;
using BankingServiceSimulation.Application.Services;
using BankingServiceSimulation.Domain.Interfaces;
using BankingServiceSimulation.Infrastructure.Persistence;
using BankingServiceSimulation.Infrastructure.Repositories;

namespace BankingServiceSimulation;

public class Program
{
    private static IAccountService _accountService = null!;
    private static IBankingService _bankingService = null!;

    public static async Task Main(string[] args)
    {
        // Initialize Infrastructure Layer
        var database = new InMemoryDatabase();
        
        // Initialize Repositories
        IAccountRepository accountRepository = new InMemoryAccountRepository(database);
        ITransactionRepository transactionRepository = new InMemoryTransactionRepository(database);
        
        // Initialize Services
        _accountService = new AccountService(accountRepository);
        _bankingService = new BankingService(accountRepository, transactionRepository);
        
        Console.WriteLine("=== Banking Service Simulation ===");
        Console.WriteLine();
        
        await RunBankingSimulation();
    }

    private static async Task RunBankingSimulation()
    {
        while (true)
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. View Account");
            Console.WriteLine("3. View All Accounts");
            Console.WriteLine("4. Deposit Money");
            Console.WriteLine("5. Withdraw Money");
            Console.WriteLine("6. Transfer Money");
            Console.WriteLine("7. Check Balance");
            Console.WriteLine("8. Exit");
            Console.Write("\nSelect option: ");
            
            var choice = Console.ReadLine();
            
            try
            {
                switch (choice)
                {
                    case "1":
                        await CreateAccount();
                        break;
                    case "2":
                        await ViewAccount();
                        break;
                    case "3":
                        await ViewAllAccounts();
                        break;
                    case "4":
                        await DepositMoney();
                        break;
                    case "5":
                        await WithdrawMoney();
                        break;
                    case "6":
                        await TransferMoney();
                        break;
                    case "7":
                        await CheckBalance();
                        break;
                    case "8":
                        Console.WriteLine("\nThank you for using Banking Service!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }
    }

    private static async Task CreateAccount()
    {
        Console.WriteLine("\n--- Create Account ---");
        Console.Write("Enter account holder name: ");
        var name = Console.ReadLine() ?? "";
        
        Console.Write("Enter initial deposit amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }
        
        var createDto = new CreateAccountDto(name, amount);
        var account = await _accountService.CreateAccountAsync(createDto);
        
        Console.WriteLine($"\n✓ Account created successfully!");
        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Account Holder: {account.AccountHolderName}");
        Console.WriteLine($"Initial Balance: {account.Balance:C}");
    }

    private static async Task ViewAccount()
    {
        Console.WriteLine("\n--- View Account ---");
        Console.Write("Enter account number: ");
        var accountNumber = Console.ReadLine() ?? "";
        
        var account = await _accountService.GetAccountByNumberAsync(accountNumber);
        
        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }
        
        DisplayAccount(account);
    }

    private static async Task ViewAllAccounts()
    {
        Console.WriteLine("\n--- All Accounts ---");
        var accounts = await _accountService.GetAllAccountsAsync();
        var accountList = accounts.ToList();
        
        if (!accountList.Any())
        {
            Console.WriteLine("No accounts found.");
            return;
        }
        
        foreach (var account in accountList)
        {
            DisplayAccount(account);
            Console.WriteLine();
        }
    }

    private static async Task DepositMoney()
    {
        Console.WriteLine("\n--- Deposit Money ---");
        Console.Write("Enter account number: ");
        var accountNumber = Console.ReadLine() ?? "";
        
        Console.Write("Enter deposit amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }
        
        var transaction = await _bankingService.DepositAsync(accountNumber, amount);
        
        Console.WriteLine($"\n✓ Deposit successful!");
        Console.WriteLine($"Amount: {transaction.Amount:C}");
        Console.WriteLine($"Transaction ID: {transaction.Id}");
        
        var balance = await _accountService.GetBalanceAsync(accountNumber);
        Console.WriteLine($"New Balance: {balance:C}");
    }

    private static async Task WithdrawMoney()
    {
        Console.WriteLine("\n--- Withdraw Money ---");
        Console.Write("Enter account number: ");
        var accountNumber = Console.ReadLine() ?? "";
        
        Console.Write("Enter withdrawal amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }
        
        var transaction = await _bankingService.WithdrawAsync(accountNumber, amount);
        
        Console.WriteLine($"\n✓ Withdrawal successful!");
        Console.WriteLine($"Amount: {transaction.Amount:C}");
        Console.WriteLine($"Transaction ID: {transaction.Id}");
        
        var balance = await _accountService.GetBalanceAsync(accountNumber);
        Console.WriteLine($"New Balance: {balance:C}");
    }

    private static async Task TransferMoney()
    {
        Console.WriteLine("\n--- Transfer Money ---");
        Console.Write("Enter source account number: ");
        var fromAccount = Console.ReadLine() ?? "";
        
        Console.Write("Enter destination account number: ");
        var toAccount = Console.ReadLine() ?? "";
        
        Console.Write("Enter transfer amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }
        
        var (fromTransaction, toTransaction) = await _bankingService.TransferAsync(fromAccount, toAccount, amount);
        
        Console.WriteLine($"\n✓ Transfer successful!");
        Console.WriteLine($"Amount: {amount:C}");
        Console.WriteLine($"From Account: {fromAccount}");
        Console.WriteLine($"To Account: {toAccount}");
        
        var fromBalance = await _accountService.GetBalanceAsync(fromAccount);
        var toBalance = await _accountService.GetBalanceAsync(toAccount);
        Console.WriteLine($"\nSource Account New Balance: {fromBalance:C}");
        Console.WriteLine($"Destination Account New Balance: {toBalance:C}");
    }

    private static async Task CheckBalance()
    {
        Console.WriteLine("\n--- Check Balance ---");
        Console.Write("Enter account number: ");
        var accountNumber = Console.ReadLine() ?? "";
        
        var balance = await _accountService.GetBalanceAsync(accountNumber);
        
        Console.WriteLine($"\nAccount Number: {accountNumber}");
        Console.WriteLine($"Current Balance: {balance:C}");
    }

    private static void DisplayAccount(AccountDto account)
    {
        Console.WriteLine($"Account Number: {account.AccountNumber}");
        Console.WriteLine($"Account Holder: {account.AccountHolderName}");
        Console.WriteLine($"Balance: {account.Balance:C}");
        Console.WriteLine($"Created: {account.CreatedAt:g}");
        Console.WriteLine($"Last Updated: {account.UpdatedAt:g}");
    }
}