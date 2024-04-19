using FluentValidation;

namespace DrugStore.Application.Report.Queries.TopCustomerByYearQuery;

public sealed class TopCustomerByYearReportValidator : AbstractValidator<TopCustomerByYearReport>
{
    public TopCustomerByYearReportValidator()
    {
        RuleFor(x => x.Year)
            .NotEmpty()
            .InclusiveBetween(2000, 2100);

        RuleFor(x => x.Limit)
            .NotEmpty()
            .InclusiveBetween(1, 100);
    }
}