using Ardalis.Result;
using Dapper;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Report.ViewModels;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DrugStore.Application.Report.Queries.TopProductByMonthQuery;

public sealed class TopProductsByMonthReportHandler(IConfiguration config)
    : IQueryHandler<TopProductsByMonthReport, Result<List<TopProductByMonthVm>>>
{
    public async Task<Result<List<TopProductByMonthVm>>> Handle(TopProductsByMonthReport request,
        CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT p.id AS Id, p.name AS Name, SUM(od.quantity) AS TotalQuantity
                           FROM products AS p
                               INNER JOIN order_details AS od ON p.id = od.product_id
                               INNER JOIN orders AS o ON od.order_id = o.id
                           WHERE EXTRACT(YEAR FROM o.created_date) = @Year
                               AND EXTRACT(MONTH FROM o.created_date) = @Month
                           GROUP BY p.Id, p.Name
                           ORDER BY TotalQuantity DESC
                           LIMIT @Limit;
                           """;

        await using NpgsqlConnection conn = new(config.GetConnectionString("Postgres"));
        var result =
            await conn.QueryAsync<TopProductByMonthVm>(sql, new { request.Month, request.Year, request.Limit });
        return Result<List<TopProductByMonthVm>>.Success(result.ToList());
    }
}