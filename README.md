# Banking Service Simulation

A console-based banking service implementation demonstrating core banking operations.

## Overview

This project simulates a basic banking system that allows account creation, deposits, withdrawals, and transfers between accounts. The system enforces real-world banking constraints such as overdraft prevention and transaction validation.

## Prerequisites

- .NET 8.0 SDK
- Any IDE that supports .NET (Visual Studio, Rider, VS Code)

## Quick Start

### Clone and Build

```bash
cd banking-service-simulation
dotnet restore
dotnet build
```

### Run the Application

```bash
dotnet run --project banking-service-simulation/banking-service-simulation.csproj
```

Or if using Rider/Visual Studio:
- Open `banking-service-simulation.sln`
- Press F5 or click Run

### Run Tests

```bash
dotnet test
```

## Architecture

This project follows Clean Architecture principles with clear separation between layers:

### Domain Layer
Contains core business entities and repository interfaces. Independent of external frameworks and implementation details.

### Application Layer
Implements business use cases and orchestrates domain entities. Depends only on the Domain layer.

### Infrastructure Layer
Provides concrete implementations of repository interfaces. Currently uses thread-safe in-memory storage.

### Presentation Layer
Console UI for user interaction. Manages dependency injection and delegates to application services.

## Features

### Account Management
- Create new accounts with initial deposit
- View account details by account number
- List all accounts
- Check account balance

### Banking Operations
- Deposit funds to an account
- Withdraw funds with overdraft prevention
- Transfer funds between accounts
- Automatic transaction logging

### Validation & Constraints
- Prevents negative amounts
- Prevents overdrafts
- Validates account existence
- Thread-safe concurrent operations

## Testing
Run tests with:
```bash
dotnet test --verbosity normal
```

## Design Decisions

### Repository Pattern
Abstracts data access through interfaces, allowing easy swapping of storage implementations without affecting business logic.

### Async/Await
All operations are asynchronous to align with modern .NET best practices and enable future scalability.

### In-Memory Storage
Uses thread-safe in-memory storage for simplicity. Can be replaced with database implementation by creating new repository classes.

### DTOs
Separate Data Transfer Objects maintain clear boundaries between layers and prevent tight coupling to domain entities.

## Usage Example

```
--- Main Menu ---
1. Create Account
2. View Account
3. View All Accounts
4. Deposit Money
5. Withdraw Money
6. Transfer Money
7. Check Balance
8. Exit

Select option: 1
Enter account holder name: John Doe
Enter initial deposit amount: 1000

Account created successfully!
Account Number: 1234567890
Account Holder: John Doe
Initial Balance: $1,000.00
```

