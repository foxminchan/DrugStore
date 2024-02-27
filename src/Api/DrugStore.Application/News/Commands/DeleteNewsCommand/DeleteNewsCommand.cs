using Ardalis.Result;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.News.Commands.DeleteNewsCommand;

public sealed record DeleteNewsCommand(Guid Id) : ICommand<Result>;
