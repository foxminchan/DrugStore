using Ardalis.Result;
using Dapper;
using DrugStore.Application.Report.ViewModels;
using DrugStore.Domain.SharedKernel;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DrugStore.Application.Report.Queries.TotalRevenueByQuarterQuery;

public sealed class TotalRevenueByQuarterReportHandler(IConfiguration config)
    : IQueryHandler<TotalRevenueByQuarterReport, Result<List<TotalRevenueByQuarterVm>>>
{
    public async Task<Result<List<TotalRevenueByQuarterVm>>> Handle(
        TotalRevenueByQuarterReport request,
        CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT
                               EXTRACT(MONTH FROM o.created_date) AS Month,
                               SUM(od.quantity * od.price) AS TotalRevenue
                           FROM
                               orders AS o
                           INNER JOIN
                               order_details AS od ON o.id = od.order_id
                           WHERE
                               EXTRACT(YEAR FROM o.created_date) = @Year
                               AND CASE
                                       WHEN EXTRACT(MONTH FROM o.created_date) BETWEEN 1 AND 3 THEN 1
                                       WHEN EXTRACT(MONTH FROM o.created_date) BETWEEN 4 AND 6 THEN 2
                                       WHEN EXTRACT(MONTH FROM o.created_date) BETWEEN 7 AND 9 THEN 3
                                       WHEN EXTRACT(MONTH FROM o.created_date) BETWEEN 10 AND 12 THEN 4
                                   END = @Quarter
                           GROUP BY
                               EXTRACT(MONTH FROM o.created_date)
                           ORDER BY
                               EXTRACT(MONTH FROM o.created_date);
                           """;

        await using NpgsqlConnection conn = new(config.GetConnectionString("Postgres"));
        var result =
            await conn.QueryAsync<TotalRevenueByQuarterVm>(sql, new { request.Year, request.Quarter });
        return Result<List<TotalRevenueByQuarterVm>>.Success(result.ToList());
    }
}