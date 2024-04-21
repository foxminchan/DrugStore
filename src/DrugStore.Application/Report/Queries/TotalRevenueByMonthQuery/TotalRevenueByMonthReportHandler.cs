using Ardalis.Result;
using Dapper;
using DrugStore.Application.Abstractions.Queries;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DrugStore.Application.Report.Queries.TotalRevenueByMonthQuery;

public sealed class TotalRevenueByMonthReportHandler(IConfiguration config)
    : IQueryHandler<TotalRevenueByMonthReport, Result<decimal>>
{
    public async Task<Result<decimal>> Handle(TotalRevenueByMonthReport request, CancellationToken cancellationToken)
    {
        const string sql = """
                           SELECT SUM(od.price * od.quantity)
                           FROM order_details AS od
                               INNER JOIN orders AS o ON od.order_id = o.id
                           WHERE EXTRACT(YEAR FROM o.created_date) = @Year
                               AND EXTRACT(MONTH FROM o.created_date) = @Month;
                           """;

        await using NpgsqlConnection conn = new(config.GetConnectionString("Postgres"));
        var result = await conn.QueryFirstOrDefaultAsync<decimal>(sql, new { request.Month, request.Year });
        return Result<decimal>.Success(result);
    }
}