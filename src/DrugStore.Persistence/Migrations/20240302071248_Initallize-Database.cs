#nullable disable

using DrugStore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DrugStore.Persistence.Migrations;

/// <inheritdoc />
public partial class InitallizeDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_order_details_orders_order_id",
            table: "order_details");

        migrationBuilder.DropForeignKey(
            name: "fk_product_images_products_product_id",
            table: "product_images");

        migrationBuilder.DropPrimaryKey(
            name: "pk_product_images",
            table: "product_images");

        migrationBuilder.RenameTable(
            name: "product_images",
            newName: "product_image");

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 130, DateTimeKind.Utc).AddTicks(2774),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(8110));

        migrationBuilder.AlterColumn<string>(
            name: "price",
            table: "products",
            type: "jsonb",
            nullable: true,
            oldClrType: typeof(ProductPrice),
            oldType: "jsonb");

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 130, DateTimeKind.Utc).AddTicks(2181),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(7609));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "posts",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 129, DateTimeKind.Utc).AddTicks(2981),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(4147));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "posts",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 129, DateTimeKind.Utc).AddTicks(2383),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(3766));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 126, DateTimeKind.Utc).AddTicks(9430),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(5760));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 126, DateTimeKind.Utc).AddTicks(8787),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(5341));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 127, DateTimeKind.Utc).AddTicks(9907),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(9682));

        migrationBuilder.AlterColumn<Guid>(
            name: "product_id",
            table: "order_details",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.AlterColumn<Guid>(
            name: "order_id",
            table: "order_details",
            type: "uuid",
            nullable: true,
            oldClrType: typeof(Guid),
            oldType: "uuid");

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 127, DateTimeKind.Utc).AddTicks(9191),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(9088));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "news",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 125, DateTimeKind.Utc).AddTicks(3331),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(1835));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "news",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 125, DateTimeKind.Utc).AddTicks(2594),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(1506));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 124, DateTimeKind.Utc).AddTicks(618),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 399, DateTimeKind.Utc).AddTicks(9762));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 122, DateTimeKind.Utc).AddTicks(7982),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 399, DateTimeKind.Utc).AddTicks(9376));

        migrationBuilder.AddColumn<int>(
                name: "id",
                table: "product_image",
                type: "integer",
                nullable: false,
                defaultValue: 0)
            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AddPrimaryKey(
            name: "pk_product_image",
            table: "product_image",
            columns: ["product_id", "id"]);

        migrationBuilder.AddForeignKey(
            name: "fk_order_details_orders_order_id",
            table: "order_details",
            column: "order_id",
            principalTable: "orders",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_product_image_products_product_id",
            table: "product_image",
            column: "product_id",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_order_details_orders_order_id",
            table: "order_details");

        migrationBuilder.DropForeignKey(
            name: "fk_product_image_products_product_id",
            table: "product_image");

        migrationBuilder.DropPrimaryKey(
            name: "pk_product_image",
            table: "product_image");

        migrationBuilder.DropColumn(
            name: "id",
            table: "product_image");

        migrationBuilder.RenameTable(
            name: "product_image",
            newName: "product_images");

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(8110),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 130, DateTimeKind.Utc).AddTicks(2774));

        migrationBuilder.AlterColumn<ProductPrice>(
            name: "price",
            table: "products",
            type: "jsonb",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "jsonb",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(7609),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 130, DateTimeKind.Utc).AddTicks(2181));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "posts",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(4147),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 129, DateTimeKind.Utc).AddTicks(2981));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "posts",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(3766),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 129, DateTimeKind.Utc).AddTicks(2383));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(5760),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 126, DateTimeKind.Utc).AddTicks(9430));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(5341),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 126, DateTimeKind.Utc).AddTicks(8787));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(9682),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 127, DateTimeKind.Utc).AddTicks(9907));

        migrationBuilder.AlterColumn<Guid>(
            name: "product_id",
            table: "order_details",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);

        migrationBuilder.AlterColumn<Guid>(
            name: "order_id",
            table: "order_details",
            type: "uuid",
            nullable: false,
            defaultValue: Guid.Empty,
            oldClrType: typeof(Guid),
            oldType: "uuid",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(9088),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 127, DateTimeKind.Utc).AddTicks(9191));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "news",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(1835),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 125, DateTimeKind.Utc).AddTicks(3331));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "news",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(1506),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 125, DateTimeKind.Utc).AddTicks(2594));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 399, DateTimeKind.Utc).AddTicks(9762),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 124, DateTimeKind.Utc).AddTicks(618));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 399, DateTimeKind.Utc).AddTicks(9376),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 7, 12, 48, 122, DateTimeKind.Utc).AddTicks(7982));

        migrationBuilder.AddPrimaryKey(
            name: "pk_product_images",
            table: "product_images",
            column: "product_id");

        migrationBuilder.AddForeignKey(
            name: "fk_order_details_orders_order_id",
            table: "order_details",
            column: "order_id",
            principalTable: "orders",
            principalColumn: "id");

        migrationBuilder.AddForeignKey(
            name: "fk_product_images_products_product_id",
            table: "product_images",
            column: "product_id",
            principalTable: "products",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}