using FluentValidation;

namespace DrugStore.Application.Report.Queries.TotalRevenueByMonthQuery;

public sealed class TotalRevenueByMonthReportValidator : AbstractValidator<TotalRevenueByMonthReport>
{
    public TotalRevenueByMonthReportValidator()
    {
        RuleFor(x => x.Month)
            .NotEmpty()
            .InclusiveBetween(1, 12);

        RuleFor(x => x.Year)
            .NotEmpty()
            .InclusiveBetween(2000, 2100);
    }
}