﻿using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetPostByIdQuery;

public record GetPostByIdQuery(Guid CategoryId, Guid PostId) : ICommand<Result<PostVm>>;
