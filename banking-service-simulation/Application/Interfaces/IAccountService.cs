using BankingServiceSimulation.Application.DTOs;

namespace BankingServiceSimulation.Application.Interfaces;

public interface IAccountService
{
    Task<AccountDto> CreateAccountAsync(CreateAccountDto createAccountDto);
    Task<AccountDto?> GetAccountByIdAsync(Guid id);
    Task<AccountDto?> GetAccountByNumberAsync(string accountNumber);
    Task<decimal> GetBalanceAsync(string accountNumber);
    Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
}

