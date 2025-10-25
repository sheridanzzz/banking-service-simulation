namespace BankingServiceSimulation.Application.DTOs;

public record CreateAccountDto(
    string AccountHolderName,
    decimal InitialDeposit
);

