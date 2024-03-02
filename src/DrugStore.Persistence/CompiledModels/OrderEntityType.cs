﻿// <auto-generated />

using System.Reflection;
using Ardalis.SmartEnum.EFCore;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Enums;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
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

internal partial class OrderEntityType
{
    public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
    {
        var runtimeEntityType = model.AddEntityType(
            "DrugStore.Domain.OrderAggregate.Order",
            typeof(Order),
            baseEntityType);

        var id = runtimeEntityType.AddProperty(
            "Id",
            typeof(OrderId),
            propertyInfo: typeof(Order).GetProperty("Id",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<Id>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            valueGenerated: ValueGenerated.OnAdd,
            afterSaveBehavior: PropertySaveBehavior.Throw);
        id.TypeMapping = GuidTypeMapping.Default.Clone(
            comparer: new ValueComparer<OrderId>(
                (OrderId v1, OrderId v2) => v1.Equals(v2),
                (OrderId v) => v.GetHashCode(),
                (OrderId v) => v),
            keyComparer: new ValueComparer<OrderId>(
                (OrderId v1, OrderId v2) => v1.Equals(v2),
                (OrderId v) => v.GetHashCode(),
                (OrderId v) => v),
            providerValueComparer: new ValueComparer<Guid>(
                (Guid v1, Guid v2) => v1 == v2,
                (Guid v) => v.GetHashCode(),
                (Guid v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "uuid"),
            converter: new ValueConverter<OrderId, Guid>(
                (OrderId id) => id.Value,
                (Guid value) => new OrderId()),
            jsonValueReaderWriter: new JsonConvertedValueReaderWriter<OrderId, Guid>(
                JsonGuidReaderWriter.Instance,
                new ValueConverter<OrderId, Guid>(
                    (OrderId id) => id.Value,
                    (Guid value) => new OrderId())));
        id.SetSentinelFromProviderValue(new Guid("00000000-0000-0000-0000-000000000000"));
        id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        id.AddAnnotation("Relational:ColumnName", "id");
        id.AddAnnotation("Relational:DefaultValueSql", "uuid_generate_v4()");

        var code = runtimeEntityType.AddProperty(
            "Code",
            typeof(string),
            propertyInfo: typeof(Order).GetProperty("Code",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<Code>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true,
            maxLength: 20);
        code.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
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
                storeTypeName: "character varying(20)",
                size: 20));
        code.TypeMapping = ((NpgsqlStringTypeMapping)code.TypeMapping).Clone(npgsqlDbType: NpgsqlDbType.Varchar);
        code.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        code.AddAnnotation("Relational:ColumnName", "code");

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
            new DateTime(2024, 3, 2, 17, 0, 46, 75, DateTimeKind.Utc).AddTicks(7207));

        var customerId = runtimeEntityType.AddProperty(
            "CustomerId",
            typeof(IdentityId?),
            propertyInfo: typeof(Order).GetProperty("CustomerId",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<CustomerId>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true);
        customerId.TypeMapping = GuidTypeMapping.Default.Clone(
            comparer: new ValueComparer<IdentityId?>(
                (Nullable<IdentityId> v1, Nullable<IdentityId> v2) =>
                    v1.HasValue && v2.HasValue && ((IdentityId)v1).Equals((IdentityId)v2) ||
                    !v1.HasValue && !v2.HasValue,
                (Nullable<IdentityId> v) => v.HasValue ? ((IdentityId)v).GetHashCode() : 0,
                (Nullable<IdentityId> v) =>
                    v.HasValue ? (Nullable<IdentityId>)(IdentityId)v : default(Nullable<IdentityId>)),
            keyComparer: new ValueComparer<IdentityId?>(
                (Nullable<IdentityId> v1, Nullable<IdentityId> v2) =>
                    v1.HasValue && v2.HasValue && ((IdentityId)v1).Equals((IdentityId)v2) ||
                    !v1.HasValue && !v2.HasValue,
                (Nullable<IdentityId> v) => v.HasValue ? ((IdentityId)v).GetHashCode() : 0,
                (Nullable<IdentityId> v) =>
                    v.HasValue ? (Nullable<IdentityId>)(IdentityId)v : default(Nullable<IdentityId>)),
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
        customerId.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        customerId.AddAnnotation("Relational:ColumnName", "customer_id");

        var paymentMethod = runtimeEntityType.AddProperty(
            "PaymentMethod",
            typeof(PaymentMethod),
            propertyInfo: typeof(Order).GetProperty("PaymentMethod",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<PaymentMethod>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true);
        paymentMethod.TypeMapping = IntTypeMapping.Default.Clone(
            comparer: new ValueComparer<PaymentMethod>(
                (PaymentMethod v1, PaymentMethod v2) => Equals(v1, v2),
                (PaymentMethod v) => v.GetHashCode(),
                (PaymentMethod v) => v),
            keyComparer: new ValueComparer<PaymentMethod>(
                (PaymentMethod v1, PaymentMethod v2) => Equals(v1, v2),
                (PaymentMethod v) => v.GetHashCode(),
                (PaymentMethod v) => v),
            providerValueComparer: new ValueComparer<int>(
                (int v1, int v2) => v1 == v2,
                (int v) => v,
                (int v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "integer"),
            converter: new ValueConverter<PaymentMethod, int>(
                (PaymentMethod item) => item.Value,
                (int key) => SmartEnumConverter<PaymentMethod, int>.GetFromValue(key)),
            jsonValueReaderWriter: new JsonConvertedValueReaderWriter<PaymentMethod, int>(
                JsonInt32ReaderWriter.Instance,
                new ValueConverter<PaymentMethod, int>(
                    (PaymentMethod item) => item.Value,
                    (int key) => SmartEnumConverter<PaymentMethod, int>.GetFromValue(key))));
        paymentMethod.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        paymentMethod.AddAnnotation("Relational:ColumnName", "payment_method");

        var status = runtimeEntityType.AddProperty(
            "Status",
            typeof(OrderStatus),
            propertyInfo: typeof(Order).GetProperty("Status",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<Status>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true);
        status.TypeMapping = IntTypeMapping.Default.Clone(
            comparer: new ValueComparer<OrderStatus>(
                (OrderStatus v1, OrderStatus v2) => Equals(v1, v2),
                (OrderStatus v) => v.GetHashCode(),
                (OrderStatus v) => v),
            keyComparer: new ValueComparer<OrderStatus>(
                (OrderStatus v1, OrderStatus v2) => Equals(v1, v2),
                (OrderStatus v) => v.GetHashCode(),
                (OrderStatus v) => v),
            providerValueComparer: new ValueComparer<int>(
                (int v1, int v2) => v1 == v2,
                (int v) => v,
                (int v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "integer"),
            converter: new ValueConverter<OrderStatus, int>(
                (OrderStatus item) => item.Value,
                (int key) => SmartEnumConverter<OrderStatus, int>.GetFromValue(key)),
            jsonValueReaderWriter: new JsonConvertedValueReaderWriter<OrderStatus, int>(
                JsonInt32ReaderWriter.Instance,
                new ValueConverter<OrderStatus, int>(
                    (OrderStatus item) => item.Value,
                    (int key) => SmartEnumConverter<OrderStatus, int>.GetFromValue(key))));
        status.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        status.AddAnnotation("Relational:ColumnName", "status");

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
            new DateTime(2024, 3, 2, 17, 0, 46, 75, DateTimeKind.Utc).AddTicks(7535));

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
        key.AddAnnotation("Relational:Name", "pk_orders");

        var index = runtimeEntityType.AddIndex(
            new[] { customerId });
        index.AddAnnotation("Relational:Name", "ix_orders_customer_id");

        return runtimeEntityType;
    }

    public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType,
        RuntimeEntityType principalEntityType)
    {
        var runtimeForeignKey = declaringEntityType.AddForeignKey(
            new[] { declaringEntityType.FindProperty("CustomerId") },
            principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
            principalEntityType,
            deleteBehavior: DeleteBehavior.SetNull);

        var customer = declaringEntityType.AddNavigation("Customer",
            runtimeForeignKey,
            onDependent: true,
            typeof(ApplicationUser),
            propertyInfo: typeof(Order).GetProperty("Customer",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<Customer>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

        var orders = principalEntityType.AddNavigation("Orders",
            runtimeForeignKey,
            onDependent: false,
            typeof(ICollection<Order>),
            propertyInfo: typeof(ApplicationUser).GetProperty("Orders",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(ApplicationUser).GetField("<Orders>k__BackingField",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

        runtimeForeignKey.AddAnnotation("Relational:Name", "fk_orders_asp_net_users_customer_id");
        return runtimeForeignKey;
    }

    public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
    {
        runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
        runtimeEntityType.AddAnnotation("Relational:Schema", null);
        runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
        runtimeEntityType.AddAnnotation("Relational:TableName", "orders");
        runtimeEntityType.AddAnnotation("Relational:ViewName", null);
        runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

        Customize(runtimeEntityType);
    }

    static partial void Customize(RuntimeEntityType runtimeEntityType);
}