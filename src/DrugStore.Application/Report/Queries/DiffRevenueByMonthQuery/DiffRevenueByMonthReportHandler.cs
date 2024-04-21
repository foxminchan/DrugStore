using Ardalis.Result;
using Dapper;
using DrugStore.Application.Report.ViewModels;
using DrugStore.Domain.SharedKernel;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DrugStore.Application.Report.Queries.DiffRevenueByMonthQuery;

public sealed class DiffRevenueByMonthReportHandler(IConfiguration configuration)
    : IQueryHandler<DiffRevenueByMonthReport, Result<DiffRevenueByMonthVm>>
{
    public async Task<Result<DiffRevenueByMonthVm>> Handle(DiffRevenueByMonthReport request,
        CancellationToken cancellationToken)
    {
        const string sql = """
                           WITH MonthYearRevenue AS
                               (
                               SELECT EXTRACT(MONTH FROM o.created_date) AS month,
                                      EXTRACT(YEAR FROM o.created_date)  AS year,
                                      SUM(od.quantity * od.price)        AS total_revenue
                               FROM orders AS o
                                   INNER JOIN order_details AS od ON o.id = od.order_id
                               WHERE (
                                   EXTRACT(YEAR FROM o.created_date) = @SourceYear
                                       AND EXTRACT(MONTH FROM o.created_date) = @SourceMonth
                                   )
                                  OR (
                                      EXTRACT(YEAR FROM o.created_date) = @TargetYear AND
                                      EXTRACT(MONTH FROM o.created_date) = @TargetMonth
                                      )
                               GROUP BY EXTRACT(MONTH FROM o.created_date), EXTRACT(YEAR FROM o.created_date)
                               )
                           SELECT CONCAT(s.month, '/', s.year) AS SourceMonthYear,
                                  CONCAT(d.month, '/', d.year) AS TargetMonthYear,
                                  ABS(s.total_revenue - d.total_revenue) AS Diff
                           FROM MonthYearRevenue s
                               CROSS JOIN MonthYearRevenue d
                           WHERE s.month = @SourceMonth
                             AND s.year = @SourceYear
                             AND d.month = @TargetMonth
                             AND d.year = @TargetYear;
                           """;

        await using NpgsqlConnection conn = new(configuration.GetConnectionString("Postgres"));
        var result =
            await conn.QueryAsync<DiffRevenueByMonthVm>(sql,
                new { request.SourceMonth, request.SourceYear, request.TargetMonth, request.TargetYear }
            );
        return Result<DiffRevenueByMonthVm>.Success(result.First());
    }
}