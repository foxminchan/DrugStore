using FluentValidation;

namespace DrugStore.Application.Report.Queries.TotalRevenueByQuarterQuery;

public sealed class TotalRevenueByQuarterReportValidator : AbstractValidator<TotalRevenueByQuarterReport>
{
    public TotalRevenueByQuarterReportValidator()
    {
        RuleFor(x => x.Quarter)
            .NotEmpty()
            .InclusiveBetween(1, 4);

        RuleFor(x => x.Year)
            .NotEmpty()
            .InclusiveBetween(2000, 2100);
    }
}