#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Persistence.Migrations;

/// <inheritdoc />
public partial class CreateCard_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 760, DateTimeKind.Utc).AddTicks(24),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 359, DateTimeKind.Utc).AddTicks(1107));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 759, DateTimeKind.Utc).AddTicks(9454),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 359, DateTimeKind.Utc).AddTicks(48));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "posts",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 757, DateTimeKind.Utc).AddTicks(9257),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 354, DateTimeKind.Utc).AddTicks(8379));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "posts",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 757, DateTimeKind.Utc).AddTicks(8689),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 354, DateTimeKind.Utc).AddTicks(5795));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 751, DateTimeKind.Utc).AddTicks(7649),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 342, DateTimeKind.Utc).AddTicks(2405));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 751, DateTimeKind.Utc).AddTicks(7089),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 342, DateTimeKind.Utc).AddTicks(1955));

        migrationBuilder.AddColumn<Guid>(
            name: "card_id",
            table: "orders",
            type: "uuid",
            nullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 754, DateTimeKind.Utc).AddTicks(5291),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 346, DateTimeKind.Utc).AddTicks(6757));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 754, DateTimeKind.Utc).AddTicks(4776),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 346, DateTimeKind.Utc).AddTicks(6038));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "news",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 749, DateTimeKind.Utc).AddTicks(1844),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 339, DateTimeKind.Utc).AddTicks(4086));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "news",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 749, DateTimeKind.Utc).AddTicks(1310),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 339, DateTimeKind.Utc).AddTicks(3526));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 746, DateTimeKind.Utc).AddTicks(9680),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 336, DateTimeKind.Utc).AddTicks(9966));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 746, DateTimeKind.Utc).AddTicks(9175),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 335, DateTimeKind.Utc).AddTicks(7394));

        migrationBuilder.CreateTable(
            name: "cards",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                number = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                expiry_year = table.Column<int>(type: "integer", nullable: false),
                expiry_month = table.Column<byte>(type: "smallint", nullable: false),
                cvc = table.Column<int>(type: "integer", nullable: false),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 743, DateTimeKind.Utc).AddTicks(8278)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 744, DateTimeKind.Utc).AddTicks(8877)),
                version = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table => { table.PrimaryKey("pk_cards", x => x.id); });

        migrationBuilder.CreateIndex(
            name: "ix_orders_card_id",
            table: "orders",
            column: "card_id");

        migrationBuilder.AddForeignKey(
            name: "fk_orders_cards_card_id",
            table: "orders",
            column: "card_id",
            principalTable: "cards",
            principalColumn: "id",
            onDelete: ReferentialAction.SetNull);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_orders_cards_card_id",
            table: "orders");

        migrationBuilder.DropTable(
            name: "cards");

        migrationBuilder.DropIndex(
            name: "ix_orders_card_id",
            table: "orders");

        migrationBuilder.DropColumn(
            name: "card_id",
            table: "orders");

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 359, DateTimeKind.Utc).AddTicks(1107),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 760, DateTimeKind.Utc).AddTicks(24));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "products",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 359, DateTimeKind.Utc).AddTicks(48),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 759, DateTimeKind.Utc).AddTicks(9454));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "posts",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 354, DateTimeKind.Utc).AddTicks(8379),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 757, DateTimeKind.Utc).AddTicks(9257));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "posts",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 354, DateTimeKind.Utc).AddTicks(5795),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 757, DateTimeKind.Utc).AddTicks(8689));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 342, DateTimeKind.Utc).AddTicks(2405),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 751, DateTimeKind.Utc).AddTicks(7649));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "orders",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 342, DateTimeKind.Utc).AddTicks(1955),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 751, DateTimeKind.Utc).AddTicks(7089));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 346, DateTimeKind.Utc).AddTicks(6757),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 754, DateTimeKind.Utc).AddTicks(5291));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "order_details",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 346, DateTimeKind.Utc).AddTicks(6038),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 754, DateTimeKind.Utc).AddTicks(4776));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "news",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 339, DateTimeKind.Utc).AddTicks(4086),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 749, DateTimeKind.Utc).AddTicks(1844));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "news",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 339, DateTimeKind.Utc).AddTicks(3526),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 749, DateTimeKind.Utc).AddTicks(1310));

        migrationBuilder.AlterColumn<DateTime>(
            name: "update_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: true,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 336, DateTimeKind.Utc).AddTicks(9966),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 746, DateTimeKind.Utc).AddTicks(9680));

        migrationBuilder.AlterColumn<DateTime>(
            name: "created_date",
            table: "categories",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(2024, 3, 2, 17, 2, 24, 335, DateTimeKind.Utc).AddTicks(7394),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldDefaultValue: new DateTime(2024, 3, 3, 5, 45, 10, 746, DateTimeKind.Utc).AddTicks(9175));
    }
}