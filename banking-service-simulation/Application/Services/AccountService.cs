using BankingServiceSimulation.Application.DTOs;
using BankingServiceSimulation.Application.Interfaces;
using BankingServiceSimulation.Domain.Entities;
using BankingServiceSimulation.Domain.Interfaces;

namespace BankingServiceSimulation.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccountDto)
    {
        if (string.IsNullOrWhiteSpace(createAccountDto.AccountHolderName))
            throw new Exception("Account holder name is required");

        if (createAccountDto.InitialDeposit < 0)
            throw new Exception("Initial deposit cannot be negative");

        // Generate a unique account number
        var accountNumber = GenerateAccountNumber();

        // Create the account entity
        var account = new Account(accountNumber, createAccountDto.AccountHolderName, createAccountDto.InitialDeposit);

        // Save to repository
        await _accountRepository.AddAsync(account);

        return MapToDto(account);
    }

    public async Task<AccountDto?> GetAccountByIdAsync(Guid id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        return account != null ? MapToDto(account) : null;
    }

    public async Task<AccountDto?> GetAccountByNumberAsync(string accountNumber)
    {
        var account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        return account != null ? MapToDto(account) : null;
    }

    public async Task<decimal> GetBalanceAsync(string accountNumber)
    {
        var account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        
        if (account == null)
            throw new Exception($"Account not found: {accountNumber}");

        return account.Balance;
    }

    public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
    {
        var accounts = await _accountRepository.GetAllAsync();
        return accounts.Select(MapToDto);
    }

    private string GenerateAccountNumber()
    {
        // Simple account number generation: 10 digits
        var random = new Random();
        var accountNumber = string.Empty;
        
        for (int i = 0; i < 10; i++)
        {
            accountNumber += random.Next(0, 10);
        }
        
        return accountNumber;
    }

    private AccountDto MapToDto(Account account)
    {
        return new AccountDto(
            account.Id,
            account.AccountNumber,
            account.AccountHolderName,
            account.Balance,
            account.CreatedAt,
            account.UpdatedAt
        );
    }
}

