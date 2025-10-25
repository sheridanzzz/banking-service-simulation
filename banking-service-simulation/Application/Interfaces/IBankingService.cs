using BankingServiceSimulation.Application.DTOs;

namespace BankingServiceSimulation.Application.Interfaces;

public interface IBankingService
{
    Task<TransactionDto> DepositAsync(string accountNumber, decimal amount);
    Task<TransactionDto> WithdrawAsync(string accountNumber, decimal amount);
    Task<(TransactionDto fromTransaction, TransactionDto toTransaction)> TransferAsync(
        string fromAccountNumber, 
        string toAccountNumber, 
        decimal amount);
}

