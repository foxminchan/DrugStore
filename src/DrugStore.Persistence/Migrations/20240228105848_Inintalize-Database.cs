#nullable disable

using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DrugStore.Persistence.Migrations;

/// <inheritdoc />
public partial class InintalizeDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                concurrency_stamp = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table => table.PrimaryKey("pk_asp_net_roles", x => x.id));

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                full_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                address = table.Column<Address>(type: "jsonb", nullable: true),
                user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_user_name =
                    table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                password_hash = table.Column<string>(type: "text", nullable: true),
                security_stamp = table.Column<string>(type: "text", nullable: true),
                concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                phone_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                access_failed_count = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_asp_net_users", x => x.id));

        migrationBuilder.CreateTable(
            name: "categories",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                link = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 399, DateTimeKind.Utc).AddTicks(9376)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 399, DateTimeKind.Utc).AddTicks(9762)),
                version = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_categories", x => x.id));

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                role_id = table.Column<Guid>(type: "uuid", nullable: false),
                claim_type = table.Column<string>(type: "text", nullable: true),
                claim_value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                table.ForeignKey(
                    name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                    column: x => x.role_id,
                    principalTable: "AspNetRoles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                claim_type = table.Column<string>(type: "text", nullable: true),
                claim_value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                table.ForeignKey(
                    name: "fk_asp_net_user_claims_asp_net_users_user_id",
                    column: x => x.user_id,
                    principalTable: "AspNetUsers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                login_provider = table.Column<string>(type: "text", nullable: false),
                provider_key = table.Column<string>(type: "text", nullable: false),
                provider_display_name = table.Column<string>(type: "text", nullable: true),
                user_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                table.ForeignKey(
                    name: "fk_asp_net_user_logins_asp_net_users_user_id",
                    column: x => x.user_id,
                    principalTable: "AspNetUsers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                role_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                table.ForeignKey(
                    name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                    column: x => x.role_id,
                    principalTable: "AspNetRoles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_asp_net_user_roles_asp_net_users_user_id",
                    column: x => x.user_id,
                    principalTable: "AspNetUsers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                login_provider = table.Column<string>(type: "text", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                table.ForeignKey(
                    name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                    column: x => x.user_id,
                    principalTable: "AspNetUsers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "orders",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                status = table.Column<int>(type: "integer", nullable: false),
                payment_method = table.Column<int>(type: "integer", nullable: false),
                customer_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(5341)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(5760)),
                version = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_orders", x => x.id);
                table.ForeignKey(
                    name: "fk_orders_users_customer_id",
                    column: x => x.customer_id,
                    principalTable: "AspNetUsers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "news",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                detail = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                image = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                category_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(1506)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(1835)),
                version = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_news", x => x.id);
                table.ForeignKey(
                    name: "fk_news_categories_category_id",
                    column: x => x.category_id,
                    principalTable: "categories",
                    principalColumn: "id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "posts",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                detail = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                image = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                category_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(3766)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(4147)),
                version = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_posts", x => x.id);
                table.ForeignKey(
                    name: "fk_posts_categories_category_id",
                    column: x => x.category_id,
                    principalTable: "categories",
                    principalColumn: "id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "products",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                product_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                detail = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                category_id = table.Column<Guid>(type: "uuid", nullable: true),
                price = table.Column<ProductPrice>(type: "jsonb", nullable: false),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(7609)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(8110)),
                version = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_products", x => x.id);
                table.ForeignKey(
                    name: "fk_products_categories_category_id",
                    column: x => x.category_id,
                    principalTable: "categories",
                    principalColumn: "id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "order_details",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                quantity = table.Column<int>(type: "integer", nullable: false),
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                order_id = table.Column<Guid>(type: "uuid", nullable: false),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(9088)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(9682)),
                version = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_details", x => x.id);
                table.ForeignKey(
                    name: "fk_order_details_orders_order_id",
                    column: x => x.order_id,
                    principalTable: "orders",
                    principalColumn: "id");
                table.ForeignKey(
                    name: "fk_order_details_products_product_id",
                    column: x => x.product_id,
                    principalTable: "products",
                    principalColumn: "id");
            });

        migrationBuilder.CreateTable(
            name: "product_images",
            columns: table => new
            {
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                image_url = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                alt = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                is_main = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_product_images", x => x.product_id);
                table.ForeignKey(
                    name: "fk_product_images_products_product_id",
                    column: x => x.product_id,
                    principalTable: "products",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_asp_net_role_claims_role_id",
            table: "AspNetRoleClaims",
            column: "role_id");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "normalized_name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_asp_net_user_claims_user_id",
            table: "AspNetUserClaims",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_asp_net_user_logins_user_id",
            table: "AspNetUserLogins",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_asp_net_user_roles_role_id",
            table: "AspNetUserRoles",
            column: "role_id");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AspNetUsers",
            column: "normalized_email");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AspNetUsers",
            column: "normalized_user_name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_news_category_id",
            table: "news",
            column: "category_id");

        migrationBuilder.CreateIndex(
            name: "ix_order_details_order_id",
            table: "order_details",
            column: "order_id");

        migrationBuilder.CreateIndex(
            name: "ix_order_details_product_id",
            table: "order_details",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_orders_customer_id",
            table: "orders",
            column: "customer_id");

        migrationBuilder.CreateIndex(
            name: "ix_posts_category_id",
            table: "posts",
            column: "category_id");

        migrationBuilder.CreateIndex(
            name: "ix_products_category_id",
            table: "products",
            column: "category_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AspNetRoleClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserLogins");

        migrationBuilder.DropTable(
            name: "AspNetUserRoles");

        migrationBuilder.DropTable(
            name: "AspNetUserTokens");

        migrationBuilder.DropTable(
            name: "news");

        migrationBuilder.DropTable(
            name: "order_details");

        migrationBuilder.DropTable(
            name: "posts");

        migrationBuilder.DropTable(
            name: "product_images");

        migrationBuilder.DropTable(
            name: "AspNetRoles");

        migrationBuilder.DropTable(
            name: "orders");

        migrationBuilder.DropTable(
            name: "products");

        migrationBuilder.DropTable(
            name: "AspNetUsers");

        migrationBuilder.DropTable(
            name: "categories");
    }
}