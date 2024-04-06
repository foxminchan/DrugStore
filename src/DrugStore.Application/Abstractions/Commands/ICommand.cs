using DrugStore.Domain.SharedKernel;
using MediatR;

namespace DrugStore.Application.Abstractions.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>, ITxRequest;