﻿using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Commands.CreateUserCommand;

public sealed record UserCreateRequest(
    string Email,
    string Password,
    string ConfirmPassword,
    string? FullName,
    string? Phone,
    Address? Address);

public sealed record CreateUserCommand(Guid RequestId, UserCreateRequest UserRequest)
    : IdempotencyCommand<Result<IdentityId>>(RequestId);