﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using DrugStore.Domain.Identity;
using DrugStore.Domain.Order;
using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

#pragma warning disable 219, 612, 618
#nullable disable

namespace DrugStore.Persistence.CompileModels
{
    internal partial class OrderEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "DrugStore.Domain.Order.Order",
                typeof(Order),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(Guid),
                propertyInfo: typeof(EntityBase).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EntityBase).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
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

            var code = runtimeEntityType.AddProperty(
                "Code",
                typeof(string),
                propertyInfo: typeof(Order).GetProperty("Code", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Order).GetField("<Code>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
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
            code.TypeMapping = ((NpgsqlStringTypeMapping)code.TypeMapping).Clone(npgsqlDbType: NpgsqlTypes.NpgsqlDbType.Varchar);
        code.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        code.AddAnnotation("Relational:ColumnName", "code");

        var createdDate = runtimeEntityType.AddProperty(
            "CreatedDate",
            typeof(DateTime),
            propertyInfo: typeof(AuditableEntityBase).GetProperty("CreatedDate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(AuditableEntityBase).GetField("<CreatedDate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
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
        createdDate.AddAnnotation("Relational:DefaultValue", new DateTime(2024, 2, 24, 15, 4, 3, 114, DateTimeKind.Utc).AddTicks(7978));

        var customerId = runtimeEntityType.AddProperty(
            "CustomerId",
            typeof(Guid?),
            propertyInfo: typeof(Order).GetProperty("CustomerId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<CustomerId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true);
        customerId.TypeMapping = GuidTypeMapping.Default.Clone(
            comparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) => v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            keyComparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) => v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            providerValueComparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) => v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "uuid"));
        customerId.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        customerId.AddAnnotation("Relational:ColumnName", "customer_id");

        var paymentMethod = runtimeEntityType.AddProperty(
            "PaymentMethod",
            typeof(PaymentMethod),
            propertyInfo: typeof(Order).GetProperty("PaymentMethod", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<PaymentMethod>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        paymentMethod.TypeMapping = IntTypeMapping.Default.Clone(
            comparer: new ValueComparer<PaymentMethod>(
                (PaymentMethod v1, PaymentMethod v2) => object.Equals((object)v1, (object)v2),
                (PaymentMethod v) => v.GetHashCode(),
                (PaymentMethod v) => v),
            keyComparer: new ValueComparer<PaymentMethod>(
                (PaymentMethod v1, PaymentMethod v2) => object.Equals((object)v1, (object)v2),
                (PaymentMethod v) => v.GetHashCode(),
                (PaymentMethod v) => v),
            providerValueComparer: new ValueComparer<int>(
                (int v1, int v2) => v1 == v2,
                (int v) => v,
                (int v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "integer"),
            converter: new ValueConverter<PaymentMethod, int>(
                (PaymentMethod value) => (int)value,
                (int value) => (PaymentMethod)value),
            jsonValueReaderWriter: new JsonConvertedValueReaderWriter<PaymentMethod, int>(
                JsonInt32ReaderWriter.Instance,
                new ValueConverter<PaymentMethod, int>(
                    (PaymentMethod value) => (int)value,
                    (int value) => (PaymentMethod)value)));
        paymentMethod.SetSentinelFromProviderValue(0);
        paymentMethod.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        paymentMethod.AddAnnotation("Relational:ColumnName", "payment_method");

        var status = runtimeEntityType.AddProperty(
            "Status",
            typeof(OrderStatus),
            propertyInfo: typeof(Order).GetProperty("Status", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<Status>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        status.TypeMapping = IntTypeMapping.Default.Clone(
            comparer: new ValueComparer<OrderStatus>(
                (OrderStatus v1, OrderStatus v2) => object.Equals((object)v1, (object)v2),
                (OrderStatus v) => v.GetHashCode(),
                (OrderStatus v) => v),
            keyComparer: new ValueComparer<OrderStatus>(
                (OrderStatus v1, OrderStatus v2) => object.Equals((object)v1, (object)v2),
                (OrderStatus v) => v.GetHashCode(),
                (OrderStatus v) => v),
            providerValueComparer: new ValueComparer<int>(
                (int v1, int v2) => v1 == v2,
                (int v) => v,
                (int v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "integer"),
            converter: new ValueConverter<OrderStatus, int>(
                (OrderStatus value) => (int)value,
                (int value) => (OrderStatus)value),
            jsonValueReaderWriter: new JsonConvertedValueReaderWriter<OrderStatus, int>(
                JsonInt32ReaderWriter.Instance,
                new ValueConverter<OrderStatus, int>(
                    (OrderStatus value) => (int)value,
                    (int value) => (OrderStatus)value)));
        status.SetSentinelFromProviderValue(0);
        status.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        status.AddAnnotation("Relational:ColumnName", "status");

        var updateDate = runtimeEntityType.AddProperty(
            "UpdateDate",
            typeof(DateTime?),
            propertyInfo: typeof(AuditableEntityBase).GetProperty("UpdateDate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(AuditableEntityBase).GetField("<UpdateDate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
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
        updateDate.AddAnnotation("Relational:DefaultValue", new DateTime(2024, 2, 24, 15, 4, 3, 114, DateTimeKind.Utc).AddTicks(8262));

        var version = runtimeEntityType.AddProperty(
            "Version",
            typeof(Guid),
            propertyInfo: typeof(AuditableEntityBase).GetProperty("Version", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(AuditableEntityBase).GetField("<Version>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
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

    public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
    {
        var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("CustomerId") },
            principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
            principalEntityType,
            deleteBehavior: DeleteBehavior.SetNull);

        var customer = declaringEntityType.AddNavigation("Customer",
            runtimeForeignKey,
            onDependent: true,
            typeof(ApplicationUser),
            propertyInfo: typeof(Order).GetProperty("Customer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<Customer>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

        var orders = principalEntityType.AddNavigation("Orders",
            runtimeForeignKey,
            onDependent: false,
            typeof(ICollection<Order>),
            propertyInfo: typeof(ApplicationUser).GetProperty("Orders", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(ApplicationUser).GetField("<Orders>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

        runtimeForeignKey.AddAnnotation("Relational:Name", "fk_orders_users_customer_id");
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
}
