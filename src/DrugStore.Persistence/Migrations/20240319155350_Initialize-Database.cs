using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable


namespace DrugStore.Persistence.Migrations;

/// <inheritdoc />
public partial class InitializeDatabase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_name =
                    table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                concurrency_stamp = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table => { table.PrimaryKey("pk_asp_net_roles", x => x.id); });

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                full_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_user_name =
                    table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_email =
                    table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                password_hash = table.Column<string>(type: "text", nullable: true),
                security_stamp = table.Column<string>(type: "text", nullable: true),
                concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                phone_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                access_failed_count = table.Column<int>(type: "integer", nullable: false),
                address = table.Column<string>(type: "jsonb", nullable: true)
            },
            constraints: table => { table.PrimaryKey("pk_asp_net_users", x => x.id); });

        migrationBuilder.CreateTable(
            name: "categories",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 3, 19, 15, 53, 49, 759, DateTimeKind.Utc).AddTicks(4385)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 3, 19, 15, 53, 49, 759, DateTimeKind.Utc).AddTicks(4770)),
                version = table.Column<Guid>(type: "uuid", nullable: false,
                    defaultValue: new Guid("7acc41fc-5235-4bce-a907-95202fdb8c8d"))
            },
            constraints: table => { table.PrimaryKey("pk_categories", x => x.id); });

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                role_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
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
                user_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
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
                user_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()")
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
                user_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                role_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()")
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
                user_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
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
                id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                customer_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(5157)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(5586)),
                version = table.Column<Guid>(type: "uuid", nullable: false,
                    defaultValue: new Guid("55bd4af7-e338-4953-a6af-54a8d8b37fc5"))
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_orders", x => x.id);
                table.ForeignKey(
                    name: "fk_orders_asp_net_users_customer_id",
                    column: x => x.customer_id,
                    principalTable: "AspNetUsers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "products",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                product_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                detail = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                status = table.Column<int>(type: "integer", nullable: true),
                quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                category_id = table.Column<Guid>(type: "uuid", nullable: true),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 3, 19, 15, 53, 49, 764, DateTimeKind.Utc).AddTicks(8399)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 3, 19, 15, 53, 49, 764, DateTimeKind.Utc).AddTicks(8902)),
                version = table.Column<Guid>(type: "uuid", nullable: false,
                    defaultValue: new Guid("b1758b8b-491d-41bc-822b-7a177175f684")),
                image = table.Column<string>(type: "jsonb", nullable: true),
                price = table.Column<string>(type: "jsonb", nullable: true)
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
                product_id = table.Column<Guid>(type: "uuid", nullable: false),
                order_id = table.Column<Guid>(type: "uuid", nullable: false),
                price = table.Column<decimal>(type: "numeric", nullable: false),
                quantity = table.Column<int>(type: "integer", nullable: false),
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 3, 19, 15, 53, 49, 762, DateTimeKind.Utc).AddTicks(4795)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 3, 19, 15, 53, 49, 762, DateTimeKind.Utc).AddTicks(5254)),
                version = table.Column<Guid>(type: "uuid", nullable: false,
                    defaultValue: new Guid("f8928b77-58fd-4bb3-8a98-ee3a6348d937"))
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_order_details", x => new { x.order_id, x.product_id });
                table.ForeignKey(
                    name: "fk_order_details_orders_order_id",
                    column: x => x.order_id,
                    principalTable: "orders",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_order_details_products_product_id",
                    column: x => x.product_id,
                    principalTable: "products",
                    principalColumn: "id");
            });

        migrationBuilder.InsertData(
            table: "categories",
            columns: ["id", "created_date", "description", "name", "version"],
            values: new object[,]
            {
                {
                    new Guid("01c8f6c7-0151-44db-83fd-06f0c5ff381e"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3144),
                    "Medications used to reduce inflammation and alleviate pain.", "Anti-inflammatory Drugs",
                    new Guid("9a170908-f7a2-4a2d-ad4c-cf0e0f8c978c")
                },
                {
                    new Guid("044de969-77fd-43e7-a241-a810de292e3c"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3180),
                    "Medications used to increase urine production and reduce fluid retention.", "Diuretics",
                    new Guid("19c359e3-d0cf-42bb-bbc2-a06ab005c939")
                },
                {
                    new Guid("0f27790a-c754-438c-92bb-a3b1d54340b4"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3201),
                    "Substances that increase alertness, attention, and energy.", "Stimulants",
                    new Guid("046ad6cd-0e00-45b4-8e6c-912bd7be32b7")
                },
                {
                    new Guid("288b5f4c-da89-4e45-adef-77ef3eca356c"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3192),
                    "Substances that promote bowel movements and relieve constipation.", "Laxatives",
                    new Guid("5a1c5847-88b0-4cf5-b3b0-a1939a7f54ef")
                },
                {
                    new Guid("2b4f53b1-add8-4535-9ecb-682f71582cb4"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3215),
                    "Essential nutrients required for various bodily functions.", "Vitamins and Minerals",
                    new Guid("b33892dd-0670-4bfe-ab25-fa2b8870fac6")
                },
                {
                    new Guid("4cc2c3ca-1edb-4216-a0e0-69a1cb8cfb6b"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3142),
                    "Medications used to lower blood pressure.", "Anti hypertensives",
                    new Guid("3a8db586-88d2-4250-8f40-6e4250104392")
                },
                {
                    new Guid("5e9e5dd9-942f-4f07-adc8-4a34e305cc17"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3125),
                    "Medications used to treat fungal infections.", "Antifungals",
                    new Guid("e40546f5-ef00-4ca5-8670-6eb0b8121046")
                },
                {
                    new Guid("6f3181b3-54ff-4353-8f8a-b80636af085b"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3186),
                    "Medications used to suppress the immune system, often used in transplant patients.",
                    "Immunosuppressant", new Guid("1fe26ac5-6cc9-4c7a-a810-9f3e654dded9")
                },
                {
                    new Guid("72540ff6-6945-465d-a567-4ddd7a37c16a"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3198),
                    "Drugs that induce relaxation and sleepiness.", "Sedatives",
                    new Guid("2b45948e-7d48-474b-8f9e-1aff0ca124d6")
                },
                {
                    new Guid("7c549a74-7bd3-4983-b8aa-abef758da155"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3095),
                    "Drugs used to relieve pain without causing loss of consciousness.", "Analgesics",
                    new Guid("98abb4e9-a486-45aa-a663-0d6adc5e016c")
                },
                {
                    new Guid("95b31ea2-724b-4081-beaa-cb0d363f86d2"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3136),
                    "Medications used to prevent or alleviate nausea and vomiting.", "Anti emetics",
                    new Guid("083c1bb5-73de-499d-b8b7-b6111c407563")
                },
                {
                    new Guid("9dc20a7d-2739-4844-8e7f-90f7ce942748"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3195),
                    "Medications used to relax muscles and reduce muscle spasms.", "Muscle Relaxants",
                    new Guid("9a8553ea-bee1-451c-8cb2-aa4054f778a6")
                },
                {
                    new Guid("b6b160f2-b2db-46a6-ba25-180dfaa0997e"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3122),
                    "Medications used to alleviate symptoms of depression.", "Antidepressants",
                    new Guid("9de198f6-5a32-4a27-8af4-3d0f955f5217")
                },
                {
                    new Guid("c67b5e61-680d-4e2a-8db4-05d5040d7b37"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3208),
                    "Medications used to dissolve blood clots.", "Thrombolytic",
                    new Guid("5406ddc4-3f6c-4329-a109-f3cafdea6755")
                },
                {
                    new Guid("ccf23f9f-a2e5-4f5f-8a3b-205c27dc7461"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3183),
                    "Chemical messengers that regulate various bodily functions.", "Hormones",
                    new Guid("97c4a7e8-53e9-4a46-968e-8d92ca2976e5")
                },
                {
                    new Guid("d4bc8e7b-604c-46cd-8a68-0e2143999de9"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3117),
                    "Medications used to treat bacterial infections.", "Antibiotics",
                    new Guid("dd1fc85f-4780-42ec-9f15-63c3cf63093c")
                },
                {
                    new Guid("d5d4e348-060a-4a73-9462-8ca2841680a0"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3212),
                    "Preparations that stimulate the immune system to protect against specific diseases.",
                    "Vaccines", new Guid("04143c74-de2e-4849-b697-a08f57f1573d")
                },
                {
                    new Guid("de686350-57a5-4cb4-917c-8ace222dc878"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3139),
                    "Drugs that block the action of histamine and are used to treat allergic conditions.",
                    "Antihistamines", new Guid("ff609612-4c31-44ca-b263-ee9c4c1d6a66")
                },
                {
                    new Guid("e5d59b35-afa4-4904-8b66-7f587f3279ab"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3128),
                    "Drugs used to treat viral infections.", "Antivirals",
                    new Guid("c477a9e2-0b97-4e51-8d2e-f83cc072f0d3")
                },
                {
                    new Guid("fdbec5f9-05d1-4fc5-81d5-817314a84dec"),
                    new DateTime(2024, 3, 19, 15, 53, 49, 760, DateTimeKind.Utc).AddTicks(3150),
                    "Medications used to relax the muscles in the airways, making breathing easier.",
                    "Bronchiectasis", new Guid("3b1e5ef4-c886-4585-90cf-4bc794531857")
                }
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
            name: "ix_order_details_product_id",
            table: "order_details",
            column: "product_id");

        migrationBuilder.CreateIndex(
            name: "ix_orders_customer_id",
            table: "orders",
            column: "customer_id");

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
            name: "order_details");

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