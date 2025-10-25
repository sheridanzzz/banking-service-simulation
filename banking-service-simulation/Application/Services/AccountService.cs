using BankingServiceSimulation.Application.DTOs;
using BankingServiceSimulation.Application.Interfaces;
using BankingServiceSimulation.Domain.Interfaces;

namespace BankingServiceSimulation.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccountDto)
    {
        // TODO: Implement account creation logic
        throw new NotImplementedException();
    }

    public Task<AccountDto?> GetAccountByIdAsync(Guid id)
    {
        // TODO: Implement get account by id logic
        throw new NotImplementedException();
    }

    public Task<AccountDto?> GetAccountByNumberAsync(string accountNumber)
    {
        // TODO: Implement get account by number logic
        throw new NotImplementedException();
    }

    public Task<decimal> GetBalanceAsync(string accountNumber)
    {
        // TODO: Implement get balance logic
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
    {
        // TODO: Implement get all accounts logic
        throw new NotImplementedException();
    }
}

