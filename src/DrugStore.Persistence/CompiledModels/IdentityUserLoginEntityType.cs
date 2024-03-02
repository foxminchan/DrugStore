﻿// <auto-generated />

using System.Data;
using System.Reflection;
using DrugStore.Domain.IdentityAggregate.Primitives;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#pragma warning disable 219, 612, 618
#nullable disable

namespace DrugStore.Persistence.CompiledModels;

internal partial class IdentityUserLoginEntityType
{
    public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
    {
        var runtimeEntityType = model.AddEntityType(
            "Microsoft.AspNetCore.Identity.IdentityUserLogin<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            typeof(IdentityUserLogin<IdentityId>),
            baseEntityType);

        var loginProvider = runtimeEntityType.AddProperty(
            "LoginProvider",
            typeof(string),
            propertyInfo: typeof(IdentityUserLogin<IdentityId>).GetProperty("LoginProvider",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(IdentityUserLogin<IdentityId>).GetField("<LoginProvider>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            afterSaveBehavior: PropertySaveBehavior.Throw);
        loginProvider.TypeMapping = StringTypeMapping.Default.Clone(
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
        loginProvider.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        loginProvider.AddAnnotation("Relational:ColumnName", "login_provider");

        var providerKey = runtimeEntityType.AddProperty(
            "ProviderKey",
            typeof(string),
            propertyInfo: typeof(IdentityUserLogin<IdentityId>).GetProperty("ProviderKey",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(IdentityUserLogin<IdentityId>).GetField("<ProviderKey>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            afterSaveBehavior: PropertySaveBehavior.Throw);
        providerKey.TypeMapping = StringTypeMapping.Default.Clone(
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
        providerKey.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        providerKey.AddAnnotation("Relational:ColumnName", "provider_key");

        var providerDisplayName = runtimeEntityType.AddProperty(
            "ProviderDisplayName",
            typeof(string),
            propertyInfo: typeof(IdentityUserLogin<IdentityId>).GetProperty("ProviderDisplayName",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(IdentityUserLogin<IdentityId>).GetField("<ProviderDisplayName>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true);
        providerDisplayName.TypeMapping = StringTypeMapping.Default.Clone(
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
        providerDisplayName.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        providerDisplayName.AddAnnotation("Relational:ColumnName", "provider_display_name");

        var userId = runtimeEntityType.AddProperty(
            "UserId",
            typeof(IdentityId),
            propertyInfo: typeof(IdentityUserLogin<IdentityId>).GetProperty("UserId",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(IdentityUserLogin<IdentityId>).GetField("<UserId>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        userId.TypeMapping = GuidTypeMapping.Default.Clone(
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
        userId.SetSentinelFromProviderValue(new Guid("00000000-0000-0000-0000-000000000000"));
        userId.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        userId.AddAnnotation("Relational:ColumnName", "user_id");

        var key = runtimeEntityType.AddKey(
            new[] { loginProvider, providerKey });
        runtimeEntityType.SetPrimaryKey(key);
        key.AddAnnotation("Relational:Name", "pk_asp_net_user_logins");

        var index = runtimeEntityType.AddIndex(
            new[] { userId });
        index.AddAnnotation("Relational:Name", "ix_asp_net_user_logins_user_id");

        return runtimeEntityType;
    }

    public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType,
        RuntimeEntityType principalEntityType)
    {
        var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("UserId") },
            principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
            principalEntityType,
            deleteBehavior: DeleteBehavior.Cascade,
            required: true);

        runtimeForeignKey.AddAnnotation("Relational:Name", "fk_asp_net_user_logins_asp_net_users_user_id");
        return runtimeForeignKey;
    }

    public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
    {
        runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
        runtimeEntityType.AddAnnotation("Relational:Schema", null);
        runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
        runtimeEntityType.AddAnnotation("Relational:TableName", "AspNetUserLogins");
        runtimeEntityType.AddAnnotation("Relational:ViewName", null);
        runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

        Customize(runtimeEntityType);
    }

    static partial void Customize(RuntimeEntityType runtimeEntityType);
}