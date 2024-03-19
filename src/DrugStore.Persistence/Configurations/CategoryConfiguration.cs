using DrugStore.Domain.CategoryAggregate;
using DrugStore.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrugStore.Persistence.Configurations;

public sealed class CategoryConfiguration : BaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            )
            .HasDefaultValueSql(UniqueId.UuidAlgorithm)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasMaxLength(DatabaseSchemaLength.DefaultLength)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(DatabaseSchemaLength.LongLength);

        builder.HasData(GetSampleCategoryData());
    }

    private static IEnumerable<Category> GetSampleCategoryData()
    {
        yield return new()
        {
            Name = "Analgesics",
            Description = "Drugs used to relieve pain without causing loss of consciousness."
        };

        yield return new()
        {
            Name = "Antibiotics",
            Description = "Medications used to treat bacterial infections."
        };

        yield return new()
        {
            Name = "Antidepressants",
            Description = "Medications used to alleviate symptoms of depression."
        };

        yield return new()
        {
            Name = "Antifungals",
            Description = "Medications used to treat fungal infections."
        };

        yield return new()
        {
            Name = "Antivirals",
            Description = "Drugs used to treat viral infections."
        };

        yield return new()
        {
            Name = "Anti emetics",
            Description = "Medications used to prevent or alleviate nausea and vomiting."
        };

        yield return new()
        {
            Name = "Antihistamines",
            Description = "Drugs that block the action of histamine and are used to treat allergic conditions."
        };

        yield return new()
        {
            Name = "Anti hypertensives",
            Description = "Medications used to lower blood pressure."
        };

        yield return new()
        {
            Name = "Anti-inflammatory Drugs",
            Description = "Medications used to reduce inflammation and alleviate pain."
        };

        yield return new()
        {
            Name = "Bronchiectasis",
            Description = "Medications used to relax the muscles in the airways, making breathing easier."
        };

        yield return new()
        {
            Name = "Diuretics",
            Description = "Medications used to increase urine production and reduce fluid retention."
        };

        yield return new()
        {
            Name = "Hormones",
            Description = "Chemical messengers that regulate various bodily functions."
        };

        yield return new()
        {
            Name = "Immunosuppressant",
            Description = "Medications used to suppress the immune system, often used in transplant patients."
        };

        yield return new()
        {
            Name = "Laxatives",
            Description = "Substances that promote bowel movements and relieve constipation."
        };

        yield return new()
        {
            Name = "Muscle Relaxants",
            Description = "Medications used to relax muscles and reduce muscle spasms."
        };

        yield return new()
        {
            Name = "Sedatives",
            Description = "Drugs that induce relaxation and sleepiness."
        };

        yield return new()
        {
            Name = "Stimulants",
            Description = "Substances that increase alertness, attention, and energy."
        };

        yield return new()
        {
            Name = "Thrombolytic",
            Description = "Medications used to dissolve blood clots."
        };

        yield return new()
        {
            Name = "Vaccines",
            Description = "Preparations that stimulate the immune system to protect against specific diseases."
        };

        yield return new()
        {
            Name = "Vitamins and Minerals",
            Description = "Essential nutrients required for various bodily functions."
        };
    }
}