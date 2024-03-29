﻿using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class UpdateUserRequest
{
    public IdentityId Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public Address? Address { get; set; }
}