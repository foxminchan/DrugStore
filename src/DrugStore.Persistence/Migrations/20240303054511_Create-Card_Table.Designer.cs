﻿// <auto-generated />

#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugStore.Persistence.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20240303054511_Create-Card_Table")]
partial class CreateCard_Table
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.2")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("DrugStore.Domain.CategoryAggregate.Category", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 746, DateTimeKind.Utc).AddTicks(9175))
                .HasColumnName("created_date");

            b.Property<string>("Link")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("link");

            b.Property<string>("Title")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)")
                .HasColumnName("title");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 746, DateTimeKind.Utc).AddTicks(9680))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("Id")
                .HasName("pk_categories");

            b.ToTable("categories", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.CategoryAggregate.News", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");

            b.Property<Guid?>("CategoryId")
                .HasColumnType("uuid")
                .HasColumnName("category_id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 749, DateTimeKind.Utc).AddTicks(1310))
                .HasColumnName("created_date");

            b.Property<string>("Detail")
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("character varying(500)")
                .HasColumnName("detail");

            b.Property<string>("Image")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("image");

            b.Property<string>("Title")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)")
                .HasColumnName("title");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 749, DateTimeKind.Utc).AddTicks(1844))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("Id")
                .HasName("pk_news");

            b.HasIndex("CategoryId")
                .HasDatabaseName("ix_news_category_id");

            b.ToTable("news", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.CategoryAggregate.Post", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");

            b.Property<Guid?>("CategoryId")
                .HasColumnType("uuid")
                .HasColumnName("category_id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 757, DateTimeKind.Utc).AddTicks(8689))
                .HasColumnName("created_date");

            b.Property<string>("Detail")
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("character varying(500)")
                .HasColumnName("detail");

            b.Property<string>("Image")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("image");

            b.Property<string>("Title")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)")
                .HasColumnName("title");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 757, DateTimeKind.Utc).AddTicks(9257))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("Id")
                .HasName("pk_posts");

            b.HasIndex("CategoryId")
                .HasDatabaseName("ix_posts_category_id");

            b.ToTable("posts", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.IdentityAggregate.ApplicationRole", b =>
        {
            b.Property<Guid>("Id")
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<string>("ConcurrencyStamp")
                .IsConcurrencyToken()
                .HasColumnType("text")
                .HasColumnName("concurrency_stamp");

            b.Property<string>("Name")
                .HasMaxLength(256)
                .HasColumnType("character varying(256)")
                .HasColumnName("name");

            b.Property<string>("NormalizedName")
                .HasMaxLength(256)
                .HasColumnType("character varying(256)")
                .HasColumnName("normalized_name");

            b.HasKey("Id")
                .HasName("pk_asp_net_roles");

            b.HasIndex("NormalizedName")
                .IsUnique()
                .HasDatabaseName("RoleNameIndex");

            b.ToTable("AspNetRoles", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.IdentityAggregate.ApplicationUser", b =>
        {
            b.Property<Guid>("Id")
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<int>("AccessFailedCount")
                .HasColumnType("integer")
                .HasColumnName("access_failed_count");

            b.Property<string>("ConcurrencyStamp")
                .IsConcurrencyToken()
                .HasColumnType("text")
                .HasColumnName("concurrency_stamp");

            b.Property<string>("Email")
                .HasMaxLength(256)
                .HasColumnType("character varying(256)")
                .HasColumnName("email");

            b.Property<bool>("EmailConfirmed")
                .HasColumnType("boolean")
                .HasColumnName("email_confirmed");

            b.Property<string>("FullName")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)")
                .HasColumnName("full_name");

            b.Property<bool>("LockoutEnabled")
                .HasColumnType("boolean")
                .HasColumnName("lockout_enabled");

            b.Property<DateTimeOffset?>("LockoutEnd")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("lockout_end");

            b.Property<string>("NormalizedEmail")
                .HasMaxLength(256)
                .HasColumnType("character varying(256)")
                .HasColumnName("normalized_email");

            b.Property<string>("NormalizedUserName")
                .HasMaxLength(256)
                .HasColumnType("character varying(256)")
                .HasColumnName("normalized_user_name");

            b.Property<string>("PasswordHash")
                .HasColumnType("text")
                .HasColumnName("password_hash");

            b.Property<string>("PhoneNumber")
                .HasMaxLength(10)
                .HasColumnType("character varying(10)")
                .HasColumnName("phone_number");

            b.Property<bool>("PhoneNumberConfirmed")
                .HasColumnType("boolean")
                .HasColumnName("phone_number_confirmed");

            b.Property<string>("SecurityStamp")
                .HasColumnType("text")
                .HasColumnName("security_stamp");

            b.Property<bool>("TwoFactorEnabled")
                .HasColumnType("boolean")
                .HasColumnName("two_factor_enabled");

            b.Property<string>("UserName")
                .HasMaxLength(256)
                .HasColumnType("character varying(256)")
                .HasColumnName("user_name");

            b.HasKey("Id")
                .HasName("pk_asp_net_users");

            b.HasIndex("NormalizedEmail")
                .HasDatabaseName("EmailIndex");

            b.HasIndex("NormalizedUserName")
                .IsUnique()
                .HasDatabaseName("UserNameIndex");

            b.ToTable("AspNetUsers", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.Card", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 743, DateTimeKind.Utc).AddTicks(8278))
                .HasColumnName("created_date");

            b.Property<int>("Cvc")
                .HasColumnType("integer")
                .HasColumnName("cvc");

            b.Property<byte?>("ExpiryMonth")
                .IsRequired()
                .HasColumnType("smallint")
                .HasColumnName("expiry_month");

            b.Property<int>("ExpiryYear")
                .HasColumnType("integer")
                .HasColumnName("expiry_year");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("character varying(50)")
                .HasColumnName("name");

            b.Property<string>("Number")
                .IsRequired()
                .HasMaxLength(16)
                .HasColumnType("character varying(16)")
                .HasColumnName("number");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 744, DateTimeKind.Utc).AddTicks(8877))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("Id")
                .HasName("pk_cards");

            b.ToTable("cards", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.Order", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");

            b.Property<Guid?>("CardId")
                .HasColumnType("uuid")
                .HasColumnName("card_id");

            b.Property<string>("Code")
                .HasMaxLength(20)
                .HasColumnType("character varying(20)")
                .HasColumnName("code");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 751, DateTimeKind.Utc).AddTicks(7089))
                .HasColumnName("created_date");

            b.Property<Guid?>("CustomerId")
                .HasColumnType("uuid")
                .HasColumnName("customer_id");

            b.Property<int?>("PaymentMethod")
                .HasColumnType("integer")
                .HasColumnName("payment_method");

            b.Property<int?>("Status")
                .HasColumnType("integer")
                .HasColumnName("status");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 751, DateTimeKind.Utc).AddTicks(7649))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("Id")
                .HasName("pk_orders");

            b.HasIndex("CardId")
                .HasDatabaseName("ix_orders_card_id");

            b.HasIndex("CustomerId")
                .HasDatabaseName("ix_orders_customer_id");

            b.ToTable("orders", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.OrderItem", b =>
        {
            b.Property<Guid>("OrderId")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("order_id");

            b.Property<Guid>("ProductId")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("product_id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 754, DateTimeKind.Utc).AddTicks(4776))
                .HasColumnName("created_date");

            b.Property<decimal>("Price")
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            b.Property<int>("Quantity")
                .HasColumnType("integer")
                .HasColumnName("quantity");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 754, DateTimeKind.Utc).AddTicks(5291))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("OrderId", "ProductId")
                .HasName("pk_order_details");

            b.HasIndex("ProductId")
                .HasDatabaseName("ix_order_details_product_id");

            b.ToTable("order_details", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.ProductAggregate.Product", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");

            b.Property<Guid?>("CategoryId")
                .HasColumnType("uuid")
                .HasColumnName("category_id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 759, DateTimeKind.Utc).AddTicks(9454))
                .HasColumnName("created_date");

            b.Property<string>("Detail")
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("character varying(500)")
                .HasColumnName("detail");

            b.Property<string>("ProductCode")
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("character varying(20)")
                .HasColumnName("product_code");

            b.Property<int>("Quantity")
                .ValueGeneratedOnAdd()
                .HasColumnType("integer")
                .HasDefaultValue(0)
                .HasColumnName("quantity");

            b.Property<int?>("Status")
                .HasColumnType("integer")
                .HasColumnName("status");

            b.Property<string>("Title")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("title");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 3, 3, 5, 45, 10, 760, DateTimeKind.Utc).AddTicks(24))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("Id")
                .HasName("pk_products");

            b.HasIndex("CategoryId")
                .HasDatabaseName("ix_products_category_id");

            b.ToTable("products", (string)null);
        });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityRoleClaim<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("id");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("text")
                    .HasColumnName("claim_type");

                b.Property<string>("ClaimValue")
                    .HasColumnType("text")
                    .HasColumnName("claim_value");

                b.Property<Guid>("RoleId")
                    .HasColumnType("uuid")
                    .HasColumnName("role_id");

                b.HasKey("Id")
                    .HasName("pk_asp_net_role_claims");

                b.HasIndex("RoleId")
                    .HasDatabaseName("ix_asp_net_role_claims_role_id");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityUserClaim<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("id");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("text")
                    .HasColumnName("claim_type");

                b.Property<string>("ClaimValue")
                    .HasColumnType("text")
                    .HasColumnName("claim_value");

                b.Property<Guid>("UserId")
                    .HasColumnType("uuid")
                    .HasColumnName("user_id");

                b.HasKey("Id")
                    .HasName("pk_asp_net_user_claims");

                b.HasIndex("UserId")
                    .HasDatabaseName("ix_asp_net_user_claims_user_id");

                b.ToTable("AspNetUserClaims", (string)null);
            });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityUserLogin<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.Property<string>("LoginProvider")
                    .HasColumnType("text")
                    .HasColumnName("login_provider");

                b.Property<string>("ProviderKey")
                    .HasColumnType("text")
                    .HasColumnName("provider_key");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("text")
                    .HasColumnName("provider_display_name");

                b.Property<Guid>("UserId")
                    .HasColumnType("uuid")
                    .HasColumnName("user_id");

                b.HasKey("LoginProvider", "ProviderKey")
                    .HasName("pk_asp_net_user_logins");

                b.HasIndex("UserId")
                    .HasDatabaseName("ix_asp_net_user_logins_user_id");

                b.ToTable("AspNetUserLogins", (string)null);
            });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityUserRole<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.Property<Guid>("UserId")
                    .HasColumnType("uuid")
                    .HasColumnName("user_id");

                b.Property<Guid>("RoleId")
                    .HasColumnType("uuid")
                    .HasColumnName("role_id");

                b.HasKey("UserId", "RoleId")
                    .HasName("pk_asp_net_user_roles");

                b.HasIndex("RoleId")
                    .HasDatabaseName("ix_asp_net_user_roles_role_id");

                b.ToTable("AspNetUserRoles", (string)null);
            });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityUserToken<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.Property<Guid>("UserId")
                    .HasColumnType("uuid")
                    .HasColumnName("user_id");

                b.Property<string>("LoginProvider")
                    .HasColumnType("text")
                    .HasColumnName("login_provider");

                b.Property<string>("Name")
                    .HasColumnType("text")
                    .HasColumnName("name");

                b.Property<string>("Value")
                    .HasColumnType("text")
                    .HasColumnName("value");

                b.HasKey("UserId", "LoginProvider", "Name")
                    .HasName("pk_asp_net_user_tokens");

                b.ToTable("AspNetUserTokens", (string)null);
            });

        modelBuilder.Entity("DrugStore.Domain.CategoryAggregate.News", b =>
        {
            b.HasOne("DrugStore.Domain.CategoryAggregate.Category", "Category")
                .WithMany("News")
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_news_categories_category_id");

            b.Navigation("Category");
        });

        modelBuilder.Entity("DrugStore.Domain.CategoryAggregate.Post", b =>
        {
            b.HasOne("DrugStore.Domain.CategoryAggregate.Category", "Category")
                .WithMany("Posts")
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_posts_categories_category_id");

            b.Navigation("Category");
        });

        modelBuilder.Entity("DrugStore.Domain.IdentityAggregate.ApplicationUser", b =>
        {
            b.OwnsOne("DrugStore.Domain.IdentityAggregate.ValueObjects.Address", "Address", b1 =>
            {
                b1.Property<Guid>("ApplicationUserId")
                    .HasColumnType("uuid");

                b1.Property<string>("City")
                    .IsRequired()
                    .HasColumnType("text");

                b1.Property<string>("Province")
                    .IsRequired()
                    .HasColumnType("text");

                b1.Property<string>("Street")
                    .IsRequired()
                    .HasColumnType("text");

                b1.HasKey("ApplicationUserId");

                b1.ToTable("AspNetUsers");

                b1.ToJson("address");

                b1.WithOwner()
                    .HasForeignKey("ApplicationUserId")
                    .HasConstraintName("fk_users_users_id");
            });

            b.Navigation("Address");
        });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.Order", b =>
        {
            b.HasOne("DrugStore.Domain.OrderAggregate.Card", "Card")
                .WithMany("Orders")
                .HasForeignKey("CardId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_orders_cards_card_id");

            b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationUser", "Customer")
                .WithMany("Orders")
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_orders_asp_net_users_customer_id");

            b.Navigation("Card");

            b.Navigation("Customer");
        });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.OrderItem", b =>
        {
            b.HasOne("DrugStore.Domain.OrderAggregate.Order", "Order")
                .WithMany("OrderItems")
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("fk_order_details_orders_order_id");

            b.HasOne("DrugStore.Domain.ProductAggregate.Product", "Product")
                .WithMany("OrderItems")
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired()
                .HasConstraintName("fk_order_details_products_product_id");

            b.Navigation("Order");

            b.Navigation("Product");
        });

        modelBuilder.Entity("DrugStore.Domain.ProductAggregate.Product", b =>
        {
            b.HasOne("DrugStore.Domain.CategoryAggregate.Category", "Category")
                .WithMany("Products")
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_products_categories_category_id");

            b.OwnsMany("DrugStore.Domain.ProductAggregate.ValueObjects.ProductImage", "Images", b1 =>
            {
                b1.Property<Guid>("ProductId")
                    .HasColumnType("uuid")
                    .HasColumnName("product_id");

                b1.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasColumnName("id");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                b1.Property<string>("Alt")
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)")
                    .HasColumnName("alt");

                b1.Property<string>("ImageUrl")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)")
                    .HasColumnName("image_url");

                b1.Property<bool>("IsMain")
                    .HasColumnType("boolean")
                    .HasColumnName("is_main");

                b1.Property<string>("Title")
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)")
                    .HasColumnName("title");

                b1.HasKey("ProductId", "Id")
                    .HasName("pk_product_image");

                b1.ToTable("product_image", (string)null);

                b1.WithOwner()
                    .HasForeignKey("ProductId")
                    .HasConstraintName("fk_product_image_products_product_id");
            });

            b.OwnsOne("DrugStore.Domain.ProductAggregate.ValueObjects.ProductPrice", "Price", b1 =>
            {
                b1.Property<Guid>("ProductId")
                    .HasColumnType("uuid");

                b1.Property<decimal>("OriginalPrice")
                    .HasColumnType("numeric");

                b1.Property<decimal>("Price")
                    .HasColumnType("numeric");

                b1.Property<decimal?>("PriceSale")
                    .HasColumnType("numeric");

                b1.HasKey("ProductId");

                b1.ToTable("products");

                b1.ToJson("price");

                b1.WithOwner()
                    .HasForeignKey("ProductId")
                    .HasConstraintName("fk_products_products_id");
            });

            b.Navigation("Category");

            b.Navigation("Images");

            b.Navigation("Price");
        });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityRoleClaim<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
            });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityUserClaim<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
            });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityUserLogin<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
            });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityUserRole<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id");

                b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id");
            });

        modelBuilder.Entity(
            "Microsoft.AspNetCore.Identity.IdentityUserToken<DrugStore.Domain.IdentityAggregate.Primitives.IdentityId>",
            b =>
            {
                b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired()
                    .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id");
            });

        modelBuilder.Entity("DrugStore.Domain.CategoryAggregate.Category", b =>
        {
            b.Navigation("News");

            b.Navigation("Posts");

            b.Navigation("Products");
        });

        modelBuilder.Entity("DrugStore.Domain.IdentityAggregate.ApplicationUser", b => { b.Navigation("Orders"); });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.Card", b => { b.Navigation("Orders"); });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.Order", b => { b.Navigation("OrderItems"); });

        modelBuilder.Entity("DrugStore.Domain.ProductAggregate.Product", b => { b.Navigation("OrderItems"); });
#pragma warning restore 612, 618
    }
}