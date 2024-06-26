﻿using Ardalis.Result;
using Dapper;
using DrugStore.Application.Report.ViewModels;
using DrugStore.Domain.SharedKernel;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DrugStore.Application.Report.Queries.TopCustomerByYearQuery;

public sealed class TopCustomerByYearReportHandler(IConfiguration configuration)
    : IQueryHandler<TopCustomerByYearReport, Result<List<TopCustomerByYearVm>>>
{
    public async Task<Result<List<TopCustomerByYearVm>>> Handle(
        TopCustomerByYearReport request,
        CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT u.full_name AS FullName, SUM(od.quantity * od.price ) AS TotalAmount
                           FROM "AspNetUsers" AS u
                               INNER JOIN orders AS o ON u.id = o.customer_id
                               INNER JOIN order_details AS od ON o.id = od.order_id
                           WHERE EXTRACT(YEAR FROM o.created_date) = @Year
                           GROUP BY u.full_name
                           ORDER BY TotalAmount DESC
                           LIMIT @Limit;
                           """;

        await using NpgsqlConnection conn = new(configuration.GetConnectionString("Postgres"));
        var result = await conn.QueryAsync<TopCustomerByYearVm>(sql, new { request.Year, request.Limit });
        return Result<List<TopCustomerByYearVm>>.Success(result.ToList());
    }
}