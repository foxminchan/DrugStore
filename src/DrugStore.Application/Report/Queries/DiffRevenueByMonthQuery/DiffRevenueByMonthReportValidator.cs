using FluentValidation;

namespace DrugStore.Application.Report.Queries.DiffRevenueByMonthQuery;

public sealed class DiffRevenueByMonthReportValidator : AbstractValidator<DiffRevenueByMonthReport>
{
    public DiffRevenueByMonthReportValidator()
    {
        RuleFor(x => x.SourceMonth)
            .NotEmpty()
            .InclusiveBetween(1, 12);

        RuleFor(x => x.SourceYear)
            .NotEmpty()
            .InclusiveBetween(2000, 2100);

        RuleFor(x => x.TargetMonth)
            .NotEmpty()
            .InclusiveBetween(1, 12);

        RuleFor(x => x.TargetYear)
            .NotEmpty()
            .InclusiveBetween(2000, 2100);
    }
}