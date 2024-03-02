﻿using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.Application.Users.ViewModels;

public sealed record UserVm(
    Guid Id,
    string? Email,
    string? FullName,
    string? Phone,
    Address? Address);