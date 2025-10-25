namespace BankingServiceSimulation.Application.DTOs;

public record AccountDto(
    Guid Id,
    string AccountNumber,
    string AccountHolderName,
    decimal Balance,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

