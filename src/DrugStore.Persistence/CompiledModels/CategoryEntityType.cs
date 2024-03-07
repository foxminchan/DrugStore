﻿// <auto-generated />
using System;
using System.Reflection;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

#pragma warning disable 219, 612, 618
#nullable disable

namespace DrugStore.Persistence.CompiledModels
{
    internal partial class CategoryEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "DrugStore.Domain.CategoryAggregate.Category",
                typeof(Category),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(CategoryId),
                propertyInfo: typeof(Category).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Category).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw);
            id.TypeMapping = GuidTypeMapping.Default.Clone(
                comparer: new ValueComparer<CategoryId>(
                    (CategoryId v1, CategoryId v2) => v1.Equals(v2),
                    (CategoryId v) => v.GetHashCode(),
                    (CategoryId v) => v),
                keyComparer: new ValueComparer<CategoryId>(
                    (CategoryId v1, CategoryId v2) => v1.Equals(v2),
                    (CategoryId v) => v.GetHashCode(),
                    (CategoryId v) => v),
                providerValueComparer: new ValueComparer<Guid>(
                    (Guid v1, Guid v2) => v1 == v2,
                    (Guid v) => v.GetHashCode(),
                    (Guid v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "uuid"),
                converter: new ValueConverter<CategoryId, Guid>(
                    (CategoryId id) => id.Value,
                    (Guid value) => new CategoryId(value)),
                jsonValueReaderWriter: new JsonConvertedValueReaderWriter<CategoryId, Guid>(
                    JsonGuidReaderWriter.Instance,
                    new ValueConverter<CategoryId, Guid>(
                        (CategoryId id) => id.Value,
                        (Guid value) => new CategoryId(value))));
            id.SetSentinelFromProviderValue(new Guid("00000000-0000-0000-0000-000000000000"));
            id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
            id.AddAnnotation("Relational:ColumnName", "id");
            id.AddAnnotation("Relational:DefaultValueSql", "uuid_generate_v4()");

            var createdDate = runtimeEntityType.AddProperty(
                "CreatedDate",
                typeof(DateTime),
                propertyInfo: typeof(EntityBase).GetProperty("CreatedDate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EntityBase).GetField("<CreatedDate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                sentinel: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            createdDate.TypeMapping = NpgsqlTimestampTzTypeMapping.Default.Clone(
                comparer: new ValueComparer<DateTime>(
                    (DateTime v1, DateTime v2) => v1.Equals(v2),
                    (DateTime v) => v.GetHashCode(),
                    (DateTime v) => v),
                keyComparer: new ValueComparer<DateTime>(
                    (DateTime v1, DateTime v2) => v1.Equals(v2),
                    (DateTime v) => v.GetHashCode(),
                    (DateTime v) => v),
                providerValueComparer: new ValueComparer<DateTime>(
                    (DateTime v1, DateTime v2) => v1.Equals(v2),
                    (DateTime v) => v.GetHashCode(),
                    (DateTime v) => v));
            createdDate.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
            createdDate.AddAnnotation("Relational:ColumnName", "created_date");
            createdDate.AddAnnotation("Relational:DefaultValue", new DateTime(2024, 3, 7, 14, 23, 41, 433, DateTimeKind.Utc).AddTicks(7302));

            var description = runtimeEntityType.AddProperty(
                "Description",
                typeof(string),
                propertyInfo: typeof(Category).GetProperty("Description", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Category).GetField("<Description>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 200);
            description.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
                comparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                keyComparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                providerValueComparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "character varying(200)",
                    size: 200));
            description.TypeMapping = ((NpgsqlStringTypeMapping)description.TypeMapping).Clone(npgsqlDbType: NpgsqlTypes.NpgsqlDbType.Varchar);
        description.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        description.AddAnnotation("Relational:ColumnName", "description");

        var name = runtimeEntityType.AddProperty(
            "Name",
            typeof(string),
            propertyInfo: typeof(Category).GetProperty("Name", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Category).GetField("<Name>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            maxLength: 50);
        name.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
            comparer: new ValueComparer<string>(
                (string v1, string v2) => v1 == v2,
                (string v) => v.GetHashCode(),
                (string v) => v),
            keyComparer: new ValueComparer<string>(
                (string v1, string v2) => v1 == v2,
                (string v) => v.GetHashCode(),
                (string v) => v),
            providerValueComparer: new ValueComparer<string>(
                (string v1, string v2) => v1 == v2,
                (string v) => v.GetHashCode(),
                (string v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "character varying(50)",
                size: 50));
        name.TypeMapping = ((NpgsqlStringTypeMapping)name.TypeMapping).Clone(npgsqlDbType: NpgsqlTypes.NpgsqlDbType.Varchar);
    name.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
    name.AddAnnotation("Relational:ColumnName", "name");

    var updateDate = runtimeEntityType.AddProperty(
        "UpdateDate",
        typeof(DateTime?),
        propertyInfo: typeof(EntityBase).GetProperty("UpdateDate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        fieldInfo: typeof(EntityBase).GetField("<UpdateDate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        nullable: true,
        valueGenerated: ValueGenerated.OnAdd);
    updateDate.TypeMapping = NpgsqlTimestampTzTypeMapping.Default.Clone(
        comparer: new ValueComparer<DateTime?>(
            (Nullable<DateTime> v1, Nullable<DateTime> v2) => v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
            (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
            (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)),
        keyComparer: new ValueComparer<DateTime?>(
            (Nullable<DateTime> v1, Nullable<DateTime> v2) => v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
            (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
            (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)),
        providerValueComparer: new ValueComparer<DateTime?>(
            (Nullable<DateTime> v1, Nullable<DateTime> v2) => v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
            (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
            (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)));
    updateDate.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
    updateDate.AddAnnotation("Relational:ColumnName", "update_date");
    updateDate.AddAnnotation("Relational:DefaultValue", new DateTime(2024, 3, 7, 14, 23, 41, 433, DateTimeKind.Utc).AddTicks(7729));

    var version = runtimeEntityType.AddProperty(
        "Version",
        typeof(Guid),
        propertyInfo: typeof(EntityBase).GetProperty("Version", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        fieldInfo: typeof(EntityBase).GetField("<Version>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        concurrencyToken: true,
        sentinel: new Guid("00000000-0000-0000-0000-000000000000"));
    version.TypeMapping = GuidTypeMapping.Default.Clone(
        comparer: new ValueComparer<Guid>(
            (Guid v1, Guid v2) => v1 == v2,
            (Guid v) => v.GetHashCode(),
            (Guid v) => v),
        keyComparer: new ValueComparer<Guid>(
            (Guid v1, Guid v2) => v1 == v2,
            (Guid v) => v.GetHashCode(),
            (Guid v) => v),
        providerValueComparer: new ValueComparer<Guid>(
            (Guid v1, Guid v2) => v1 == v2,
            (Guid v) => v.GetHashCode(),
            (Guid v) => v),
        mappingInfo: new RelationalTypeMappingInfo(
            storeTypeName: "uuid"));
    version.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
    version.AddAnnotation("Relational:ColumnName", "version");

    var key = runtimeEntityType.AddKey(
        new[] { id });
    runtimeEntityType.SetPrimaryKey(key);
    key.AddAnnotation("Relational:Name", "pk_categories");

    return runtimeEntityType;
}

public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
{
    runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
    runtimeEntityType.AddAnnotation("Relational:Schema", null);
    runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
    runtimeEntityType.AddAnnotation("Relational:TableName", "categories");
    runtimeEntityType.AddAnnotation("Relational:ViewName", null);
    runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

    Customize(runtimeEntityType);
}

static partial void Customize(RuntimeEntityType runtimeEntityType);
}
}
