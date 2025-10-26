using BankingServiceSimulation.Application.DTOs;
using BankingServiceSimulation.Application.Interfaces;
using BankingServiceSimulation.Domain.Entities;
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

    public async Task<TransactionDto> DepositAsync(string accountNumber, decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Deposit amount must be greater than zero");

        // Get the account
        var account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (account == null)
            throw new Exception($"Account not found: {accountNumber}");

        // Deposit money
        account.Deposit(amount);

        // Update account in repository
        await _accountRepository.UpdateAsync(account);

        // Create transaction record
        var transaction = new Transaction(
            account.Id,
            TransactionType.Deposit,
            amount,
            $"Deposit to account {accountNumber}"
        );
        await _transactionRepository.AddAsync(transaction);

        return MapToDto(transaction);
    }

    public async Task<TransactionDto> WithdrawAsync(string accountNumber, decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Withdrawal amount must be greater than zero");

        // Get the account
        var account = await _accountRepository.GetByAccountNumberAsync(accountNumber);
        if (account == null)
            throw new Exception($"Account not found: {accountNumber}");

        // Withdraw money (will throw if insufficient funds)
        account.Withdraw(amount);

        // Update account in repository
        await _accountRepository.UpdateAsync(account);

        // Create transaction record
        var transaction = new Transaction(
            account.Id,
            TransactionType.Withdrawal,
            amount,
            $"Withdrawal from account {accountNumber}"
        );
        await _transactionRepository.AddAsync(transaction);

        return MapToDto(transaction);
    }

    public async Task<(TransactionDto fromTransaction, TransactionDto toTransaction)> TransferAsync(
        string fromAccountNumber,
        string toAccountNumber,
        decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Transfer amount must be greater than zero");

        if (fromAccountNumber == toAccountNumber)
            throw new Exception("Cannot transfer to the same account");

        // Get both accounts
        var fromAccount = await _accountRepository.GetByAccountNumberAsync(fromAccountNumber);
        if (fromAccount == null)
            throw new Exception($"Source account not found: {fromAccountNumber}");

        var toAccount = await _accountRepository.GetByAccountNumberAsync(toAccountNumber);
        if (toAccount == null)
            throw new Exception($"Destination account not found: {toAccountNumber}");

        // Withdraw from source account (will throw if insufficient funds)
        fromAccount.Withdraw(amount);

        // Deposit to destination account
        toAccount.Deposit(amount);

        // Update both accounts
        await _accountRepository.UpdateAsync(fromAccount);
        await _accountRepository.UpdateAsync(toAccount);

        // Create transaction records
        var fromTransaction = new Transaction(
            fromAccount.Id,
            TransactionType.Transfer,
            amount,
            $"Transfer to account {toAccountNumber}",
            toAccount.Id
        );
        await _transactionRepository.AddAsync(fromTransaction);

        var toTransaction = new Transaction(
            toAccount.Id,
            TransactionType.Transfer,
            amount,
            $"Transfer from account {fromAccountNumber}",
            fromAccount.Id
        );
        await _transactionRepository.AddAsync(toTransaction);

        return (MapToDto(fromTransaction), MapToDto(toTransaction));
    }

    private TransactionDto MapToDto(Transaction transaction)
    {
        return new TransactionDto(
            transaction.Id,
            transaction.AccountId,
            transaction.Type,
            transaction.Amount,
            transaction.RelatedAccountId,
            transaction.Timestamp,
            transaction.Description
        );
    }
}

