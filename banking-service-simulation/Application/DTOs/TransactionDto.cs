using BankingServiceSimulation.Domain.Entities;

namespace BankingServiceSimulation.Application.DTOs;

public record TransactionDto(
    Guid Id,
    Guid AccountId,
    TransactionType Type,
    decimal Amount,
    Guid? RelatedAccountId,
    DateTime Timestamp,
    string Description
);

