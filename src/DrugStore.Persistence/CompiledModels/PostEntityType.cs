﻿// <auto-generated />

using System.Reflection;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using NpgsqlTypes;

#pragma warning disable 219, 612, 618
#nullable disable

namespace DrugStore.Persistence.CompiledModels;

internal partial class PostEntityType
{
    public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
    {
        var runtimeEntityType = model.AddEntityType(
            "DrugStore.Domain.CategoryAggregate.Post",
            typeof(Post),
            baseEntityType);

        var id = runtimeEntityType.AddProperty(
            "Id",
            typeof(Guid),
            propertyInfo: typeof(EntityBase).GetProperty("Id",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(EntityBase).GetField("<Id>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            valueGenerated: ValueGenerated.OnAdd,
            afterSaveBehavior: PropertySaveBehavior.Throw,
            sentinel: new Guid("00000000-0000-0000-0000-000000000000"));
        id.TypeMapping = GuidTypeMapping.Default.Clone(
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
        id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        id.AddAnnotation("Relational:ColumnName", "id");

        var categoryId = runtimeEntityType.AddProperty(
            "CategoryId",
            typeof(Guid?),
            propertyInfo: typeof(Post).GetProperty("CategoryId",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Post).GetField("<CategoryId>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true);
        categoryId.TypeMapping = GuidTypeMapping.Default.Clone(
            comparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) =>
                    v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            keyComparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) =>
                    v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            providerValueComparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) =>
                    v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "uuid"));
        categoryId.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        categoryId.AddAnnotation("Relational:ColumnName", "category_id");

        var createdDate = runtimeEntityType.AddProperty(
            "CreatedDate",
            typeof(DateTime),
            propertyInfo: typeof(AuditableEntityBase).GetProperty("CreatedDate",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(AuditableEntityBase).GetField("<CreatedDate>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
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
        createdDate.AddAnnotation("Relational:DefaultValue",
            new DateTime(2024, 3, 2, 5, 36, 52, 123, DateTimeKind.Utc).AddTicks(6593));

        var detail = runtimeEntityType.AddProperty(
            "Detail",
            typeof(string),
            propertyInfo: typeof(Post).GetProperty("Detail",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Post).GetField("<Detail>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            maxLength: 500);
        detail.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
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
                storeTypeName: "character varying(500)",
                size: 500));
        detail.TypeMapping = ((NpgsqlStringTypeMapping)detail.TypeMapping).Clone(npgsqlDbType: NpgsqlDbType.Varchar);
        detail.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        detail.AddAnnotation("Relational:ColumnName", "detail");

        var image = runtimeEntityType.AddProperty(
            "Image",
            typeof(string),
            propertyInfo: typeof(Post).GetProperty("Image",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Post).GetField("<Image>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true,
            maxLength: 100);
        image.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
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
                storeTypeName: "character varying(100)",
                size: 100));
        image.TypeMapping = ((NpgsqlStringTypeMapping)image.TypeMapping).Clone(npgsqlDbType: NpgsqlDbType.Varchar);
        image.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        image.AddAnnotation("Relational:ColumnName", "image");

        var title = runtimeEntityType.AddProperty(
            "Title",
            typeof(string),
            propertyInfo: typeof(Post).GetProperty("Title",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Post).GetField("<Title>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            maxLength: 50);
        title.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
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
        title.TypeMapping = ((NpgsqlStringTypeMapping)title.TypeMapping).Clone(npgsqlDbType: NpgsqlDbType.Varchar);
        title.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        title.AddAnnotation("Relational:ColumnName", "title");

        var updateDate = runtimeEntityType.AddProperty(
            "UpdateDate",
            typeof(DateTime?),
            propertyInfo: typeof(AuditableEntityBase).GetProperty("UpdateDate",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(AuditableEntityBase).GetField("<UpdateDate>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true,
            valueGenerated: ValueGenerated.OnAdd);
        updateDate.TypeMapping = NpgsqlTimestampTzTypeMapping.Default.Clone(
            comparer: new ValueComparer<DateTime?>(
                (Nullable<DateTime> v1, Nullable<DateTime> v2) =>
                    v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
                (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)),
            keyComparer: new ValueComparer<DateTime?>(
                (Nullable<DateTime> v1, Nullable<DateTime> v2) =>
                    v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
                (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)),
            providerValueComparer: new ValueComparer<DateTime?>(
                (Nullable<DateTime> v1, Nullable<DateTime> v2) =>
                    v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
                (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)));
        updateDate.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        updateDate.AddAnnotation("Relational:ColumnName", "update_date");
        updateDate.AddAnnotation("Relational:DefaultValue",
            new DateTime(2024, 3, 2, 5, 36, 52, 123, DateTimeKind.Utc).AddTicks(6974));

        var version = runtimeEntityType.AddProperty(
            "Version",
            typeof(Guid),
            propertyInfo: typeof(AuditableEntityBase).GetProperty("Version",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(AuditableEntityBase).GetField("<Version>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
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
        key.AddAnnotation("Relational:Name", "pk_posts");

        var index = runtimeEntityType.AddIndex(
            new[] { categoryId });
        index.AddAnnotation("Relational:Name", "ix_posts_category_id");

        return runtimeEntityType;
    }

    public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType,
        RuntimeEntityType principalEntityType)
    {
        var runtimeForeignKey = declaringEntityType.AddForeignKey(
            new[] { declaringEntityType.FindProperty("CategoryId") },
            principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
            principalEntityType,
            deleteBehavior: DeleteBehavior.SetNull);

        var category = declaringEntityType.AddNavigation("Category",
            runtimeForeignKey,
            onDependent: true,
            typeof(Category),
            propertyInfo: typeof(Post).GetProperty("Category",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Post).GetField("<Category>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

        var posts = principalEntityType.AddNavigation("Posts",
            runtimeForeignKey,
            onDependent: false,
            typeof(ICollection<Post>),
            propertyInfo: typeof(Category).GetProperty("Posts",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Category).GetField("<Posts>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

        runtimeForeignKey.AddAnnotation("Relational:Name", "fk_posts_categories_category_id");
        return runtimeForeignKey;
    }

    public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
    {
        runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
        runtimeEntityType.AddAnnotation("Relational:Schema", null);
        runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
        runtimeEntityType.AddAnnotation("Relational:TableName", "posts");
        runtimeEntityType.AddAnnotation("Relational:ViewName", null);
        runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

        Customize(runtimeEntityType);
    }

    static partial void Customize(RuntimeEntityType runtimeEntityType);
}