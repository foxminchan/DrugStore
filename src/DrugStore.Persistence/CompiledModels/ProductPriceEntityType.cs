﻿// <auto-generated />
using System;
using System.Reflection;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;
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

namespace DrugStore.Persistence.CompiledModels
{
    internal partial class ProductPriceEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "DrugStore.Domain.ProductAggregate.ValueObjects.ProductPrice",
                typeof(ProductPrice),
                baseEntityType);

            var productId = runtimeEntityType.AddProperty(
                "ProductId",
                typeof(ProductId),
                afterSaveBehavior: PropertySaveBehavior.Throw);
            productId.TypeMapping = GuidTypeMapping.Default.Clone(
                comparer: new ValueComparer<ProductId>(
                    (ProductId v1, ProductId v2) => v1.Equals(v2),
                    (ProductId v) => v.GetHashCode(),
                    (ProductId v) => v),
                keyComparer: new ValueComparer<ProductId>(
                    (ProductId v1, ProductId v2) => v1.Equals(v2),
                    (ProductId v) => v.GetHashCode(),
                    (ProductId v) => v),
                providerValueComparer: new ValueComparer<Guid>(
                    (Guid v1, Guid v2) => v1 == v2,
                    (Guid v) => v.GetHashCode(),
                    (Guid v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "uuid"),
                converter: new ValueConverter<ProductId, Guid>(
                    (ProductId id) => id.Value,
                    (Guid value) => new ProductId(value)),
                jsonValueReaderWriter: new JsonConvertedValueReaderWriter<ProductId, Guid>(
                    JsonGuidReaderWriter.Instance,
                    new ValueConverter<ProductId, Guid>(
                        (ProductId id) => id.Value,
                        (Guid value) => new ProductId(value))));
            productId.SetSentinelFromProviderValue(new Guid("00000000-0000-0000-0000-000000000000"));
            productId.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var price = runtimeEntityType.AddProperty(
                "Price",
                typeof(decimal),
                propertyInfo: typeof(ProductPrice).GetProperty("Price", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(ProductPrice).GetField("<Price>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: 0m);
            price.TypeMapping = NpgsqlDecimalTypeMapping.Default.Clone(
                comparer: new ValueComparer<decimal>(
                    (decimal v1, decimal v2) => v1 == v2,
                    (decimal v) => v.GetHashCode(),
                    (decimal v) => v),
                keyComparer: new ValueComparer<decimal>(
                    (decimal v1, decimal v2) => v1 == v2,
                    (decimal v) => v.GetHashCode(),
                    (decimal v) => v),
                providerValueComparer: new ValueComparer<decimal>(
                    (decimal v1, decimal v2) => v1 == v2,
                    (decimal v) => v.GetHashCode(),
                    (decimal v) => v));
            price.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var priceSale = runtimeEntityType.AddProperty(
                "PriceSale",
                typeof(decimal?),
                propertyInfo: typeof(ProductPrice).GetProperty("PriceSale", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(ProductPrice).GetField("<PriceSale>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            priceSale.TypeMapping = NpgsqlDecimalTypeMapping.Default.Clone(
                comparer: new ValueComparer<decimal?>(
                    (Nullable<decimal> v1, Nullable<decimal> v2) => v1.HasValue && v2.HasValue && (decimal)v1 == (decimal)v2 || !v1.HasValue && !v2.HasValue,
                    (Nullable<decimal> v) => v.HasValue ? ((decimal)v).GetHashCode() : 0,
                    (Nullable<decimal> v) => v.HasValue ? (Nullable<decimal>)(decimal)v : default(Nullable<decimal>)),
                keyComparer: new ValueComparer<decimal?>(
                    (Nullable<decimal> v1, Nullable<decimal> v2) => v1.HasValue && v2.HasValue && (decimal)v1 == (decimal)v2 || !v1.HasValue && !v2.HasValue,
                    (Nullable<decimal> v) => v.HasValue ? ((decimal)v).GetHashCode() : 0,
                    (Nullable<decimal> v) => v.HasValue ? (Nullable<decimal>)(decimal)v : default(Nullable<decimal>)),
                providerValueComparer: new ValueComparer<decimal?>(
                    (Nullable<decimal> v1, Nullable<decimal> v2) => v1.HasValue && v2.HasValue && (decimal)v1 == (decimal)v2 || !v1.HasValue && !v2.HasValue,
                    (Nullable<decimal> v) => v.HasValue ? ((decimal)v).GetHashCode() : 0,
                    (Nullable<decimal> v) => v.HasValue ? (Nullable<decimal>)(decimal)v : default(Nullable<decimal>)));
            priceSale.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var key = runtimeEntityType.AddKey(
                new[] { productId });
            runtimeEntityType.SetPrimaryKey(key);

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("ProductId") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                unique: true,
                required: true,
                ownership: true);

            var price = principalEntityType.AddNavigation("Price",
                runtimeForeignKey,
                onDependent: false,
                typeof(ProductPrice),
                propertyInfo: typeof(Product).GetProperty("Price", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Product).GetField("<Price>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                eagerLoaded: true);

            runtimeForeignKey.AddAnnotation("Relational:Name", "fk_products_products_id");
            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:ContainerColumnName", "price");
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "products");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
