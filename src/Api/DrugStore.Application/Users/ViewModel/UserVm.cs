using DrugStore.Domain.Identity;

namespace DrugStore.Application.Users.ViewModel;

public sealed record UserVm(
    Guid Id,
    string? Email,
    string? FullName,
    string? Phone,
    Address? Address);
