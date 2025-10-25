using BankingServiceSimulation.Application.DTOs;
using BankingServiceSimulation.Application.Interfaces;
using BankingServiceSimulation.Domain.Interfaces;

namespace BankingServiceSimulation.Application.Services;

public class BankingService : IBankingService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public BankingService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public Task<TransactionDto> DepositAsync(string accountNumber, decimal amount)
    {
        // TODO: Implement deposit logic
        throw new NotImplementedException();
    }

    public Task<TransactionDto> WithdrawAsync(string accountNumber, decimal amount)
    {
        // TODO: Implement withdrawal logic
        throw new NotImplementedException();
    }

    public Task<(TransactionDto fromTransaction, TransactionDto toTransaction)> TransferAsync(
        string fromAccountNumber, 
        string toAccountNumber, 
        decimal amount)
    {
        // TODO: Implement transfer logic
        throw new NotImplementedException();
    }
}

