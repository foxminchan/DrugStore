using FluentValidation;

namespace DrugStore.Application.Report.Queries.TopProductByMonthQuery;

public sealed class TopProductsByMonthReportValidator : AbstractValidator<TopProductsByMonthReport>
{
    public TopProductsByMonthReportValidator()
    {
        RuleFor(x => x.Month)
            .NotEmpty()
            .InclusiveBetween(1, 12);

        RuleFor(x => x.Year)
            .NotEmpty()
            .InclusiveBetween(2000, 2100);

        RuleFor(x => x.Limit)
            .NotEmpty()
            .InclusiveBetween(1, 100);
    }
}