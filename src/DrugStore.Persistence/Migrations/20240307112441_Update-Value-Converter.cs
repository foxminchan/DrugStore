#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Persistence.Migrations;

/// <inheritdoc />
public partial class UpdateValueConverter : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 605, DateTimeKind.Utc).AddTicks(5272),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 728, DateTimeKind.Utc).AddTicks(1641));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 605, DateTimeKind.Utc).AddTicks(4669),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 728, DateTimeKind.Utc).AddTicks(1149));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 596, DateTimeKind.Utc).AddTicks(4225),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 721, DateTimeKind.Utc).AddTicks(4151));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 596, DateTimeKind.Utc).AddTicks(2106),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 721, DateTimeKind.Utc).AddTicks(3530));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 600, DateTimeKind.Utc).AddTicks(7456),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 725, DateTimeKind.Utc).AddTicks(1843));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 600, DateTimeKind.Utc).AddTicks(6832),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 725, DateTimeKind.Utc).AddTicks(1307));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 594, DateTimeKind.Utc).AddTicks(1202),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 718, DateTimeKind.Utc).AddTicks(8151));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 593, DateTimeKind.Utc).AddTicks(8971),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 718, DateTimeKind.Utc).AddTicks(7664));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "cards",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 591, DateTimeKind.Utc).AddTicks(3618),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 716, DateTimeKind.Utc).AddTicks(5319));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "cards",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 590, DateTimeKind.Utc).AddTicks(4233),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 715, DateTimeKind.Utc).AddTicks(6779));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 728, DateTimeKind.Utc).AddTicks(1641),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 605, DateTimeKind.Utc).AddTicks(5272));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 728, DateTimeKind.Utc).AddTicks(1149),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 605, DateTimeKind.Utc).AddTicks(4669));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 721, DateTimeKind.Utc).AddTicks(4151),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 596, DateTimeKind.Utc).AddTicks(4225));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 721, DateTimeKind.Utc).AddTicks(3530),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 596, DateTimeKind.Utc).AddTicks(2106));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 725, DateTimeKind.Utc).AddTicks(1843),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 600, DateTimeKind.Utc).AddTicks(7456));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 725, DateTimeKind.Utc).AddTicks(1307),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 600, DateTimeKind.Utc).AddTicks(6832));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 718, DateTimeKind.Utc).AddTicks(8151),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 594, DateTimeKind.Utc).AddTicks(1202));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 718, DateTimeKind.Utc).AddTicks(7664),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 593, DateTimeKind.Utc).AddTicks(8971));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "cards",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 716, DateTimeKind.Utc).AddTicks(5319),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 591, DateTimeKind.Utc).AddTicks(3618));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "cards",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 4, 17, 0, 17, 715, DateTimeKind.Utc).AddTicks(6779),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 7, 11, 24, 40, 590, DateTimeKind.Utc).AddTicks(4233));
    }
}