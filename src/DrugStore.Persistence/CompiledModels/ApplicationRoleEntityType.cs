﻿// <auto-generated />

using System.Data;
using System.Reflection;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using NpgsqlTypes;

#pragma warning disable 219, 612, 618
#nullable disable

namespace DrugStore.Persistence.CompiledModels;

internal partial class ApplicationRoleEntityType
{
    public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
    {
        var runtimeEntityType = model.AddEntityType(
            "DrugStore.Domain.IdentityAggregate.ApplicationRole",
            typeof(ApplicationRole),
            baseEntityType);

        var id = runtimeEntityType.AddProperty(
            "Id",
            typeof(IdentityId),
            propertyInfo: typeof(IdentityRole<IdentityId>).GetProperty("Id",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(IdentityRole<IdentityId>).GetField("<Id>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            afterSaveBehavior: PropertySaveBehavior.Throw);
        id.TypeMapping = GuidTypeMapping.Default.Clone(
            comparer: new ValueComparer<IdentityId>(
                (IdentityId v1, IdentityId v2) => v1.Equals(v2),
                (IdentityId v) => v.GetHashCode(),
                (IdentityId v) => v),
            keyComparer: new ValueComparer<IdentityId>(
                (IdentityId v1, IdentityId v2) => v1.Equals(v2),
                (IdentityId v) => v.GetHashCode(),
                (IdentityId v) => v),
            providerValueComparer: new ValueComparer<Guid>(
                (Guid v1, Guid v2) => v1 == v2,
                (Guid v) => v.GetHashCode(),
                (Guid v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "uuid"),
            converter: new ValueConverter<IdentityId, Guid>(
                (IdentityId c) => c.Value,
                (Guid c) => new IdentityId(c)),
            jsonValueReaderWriter: new JsonConvertedValueReaderWriter<IdentityId, Guid>(
                JsonGuidReaderWriter.Instance,
                new ValueConverter<IdentityId, Guid>(
                    (IdentityId c) => c.Value,
                    (Guid c) => new IdentityId(c))));
        id.SetSentinelFromProviderValue(new Guid("00000000-0000-0000-0000-000000000000"));
        id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        id.AddAnnotation("Relational:ColumnName", "id");

        var concurrencyStamp = runtimeEntityType.AddProperty(
            "ConcurrencyStamp",
            typeof(string),
            propertyInfo: typeof(IdentityRole<IdentityId>).GetProperty("ConcurrencyStamp",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(IdentityRole<IdentityId>).GetField("<ConcurrencyStamp>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true,
            concurrencyToken: true);
        concurrencyStamp.TypeMapping = StringTypeMapping.Default.Clone(
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
                dbType: DbType.String));
        concurrencyStamp.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        concurrencyStamp.AddAnnotation("Relational:ColumnName", "concurrency_stamp");

        var name = runtimeEntityType.AddProperty(
            "Name",
            typeof(string),
            propertyInfo: typeof(IdentityRole<IdentityId>).GetProperty("Name",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(IdentityRole<IdentityId>).GetField("<Name>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true,
            maxLength: 256);
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
                storeTypeName: "character varying(256)",
                size: 256));
        name.TypeMapping = ((NpgsqlStringTypeMapping)name.TypeMapping).Clone(npgsqlDbType: NpgsqlDbType.Varchar);
        name.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        name.AddAnnotation("Relational:ColumnName", "name");

        var normalizedName = runtimeEntityType.AddProperty(
            "NormalizedName",
            typeof(string),
            propertyInfo: typeof(IdentityRole<IdentityId>).GetProperty("NormalizedName",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(IdentityRole<IdentityId>).GetField("<NormalizedName>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true,
            maxLength: 256);
        normalizedName.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
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
                storeTypeName: "character varying(256)",
                size: 256));
        normalizedName.TypeMapping =
            ((NpgsqlStringTypeMapping)normalizedName.TypeMapping).Clone(npgsqlDbType: NpgsqlDbType.Varchar);
        normalizedName.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        normalizedName.AddAnnotation("Relational:ColumnName", "normalized_name");

        var key = runtimeEntityType.AddKey(
            new[] { id });
        runtimeEntityType.SetPrimaryKey(key);
        key.AddAnnotation("Relational:Name", "pk_asp_net_roles");

        var index = runtimeEntityType.AddIndex(
            new[] { normalizedName },
            unique: true);
        index.AddAnnotation("Relational:Name", "RoleNameIndex");

        return runtimeEntityType;
    }

    public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
    {
        runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
        runtimeEntityType.AddAnnotation("Relational:Schema", null);
        runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
        runtimeEntityType.AddAnnotation("Relational:TableName", "AspNetRoles");
        runtimeEntityType.AddAnnotation("Relational:ViewName", null);
        runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

        Customize(runtimeEntityType);
    }

    static partial void Customize(RuntimeEntityType runtimeEntityType);
}