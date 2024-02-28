﻿// <auto-generated />
using System;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DrugStore.Persistence.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20240228105848_Inintalize-Database")]
partial class InintalizeDatabase
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.2")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("DrugStore.Domain.CategoryAggregate.Category", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 399, DateTimeKind.Utc).AddTicks(9376))
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
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 399, DateTimeKind.Utc).AddTicks(9762))
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
                .HasColumnName("id");

            b.Property<Guid?>("CategoryId")
                .HasColumnType("uuid")
                .HasColumnName("category_id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(1506))
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
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(1835))
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
                .HasColumnName("id");

            b.Property<Guid?>("CategoryId")
                .HasColumnType("uuid")
                .HasColumnName("category_id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(3766))
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
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(4147))
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
                .ValueGeneratedOnAdd()
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
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<int>("AccessFailedCount")
                .HasColumnType("integer")
                .HasColumnName("access_failed_count");

            b.Property<Address>("Address")
                .IsUnicode(true)
                .HasColumnType("jsonb")
                .HasColumnName("address");

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

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.Order", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<string>("Code")
                .HasMaxLength(20)
                .HasColumnType("character varying(20)")
                .HasColumnName("code");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(5341))
                .HasColumnName("created_date");

            b.Property<Guid?>("CustomerId")
                .HasColumnType("uuid")
                .HasColumnName("customer_id");

            b.Property<int>("PaymentMethod")
                .HasColumnType("integer")
                .HasColumnName("payment_method");

            b.Property<int>("Status")
                .HasColumnType("integer")
                .HasColumnName("status");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(5760))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("Id")
                .HasName("pk_orders");

            b.HasIndex("CustomerId")
                .HasDatabaseName("ix_orders_customer_id");

            b.ToTable("orders", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.OrderItem", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(9088))
                .HasColumnName("created_date");

            b.Property<Guid>("OrderId")
                .HasColumnType("uuid")
                .HasColumnName("order_id");

            b.Property<decimal>("Price")
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            b.Property<Guid>("ProductId")
                .HasColumnType("uuid")
                .HasColumnName("product_id");

            b.Property<int>("Quantity")
                .HasColumnType("integer")
                .HasColumnName("quantity");

            b.Property<DateTime?>("UpdateDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 400, DateTimeKind.Utc).AddTicks(9682))
                .HasColumnName("update_date");

            b.Property<Guid>("Version")
                .IsConcurrencyToken()
                .HasColumnType("uuid")
                .HasColumnName("version");

            b.HasKey("Id")
                .HasName("pk_order_details");

            b.HasIndex("OrderId")
                .HasDatabaseName("ix_order_details_order_id");

            b.HasIndex("ProductId")
                .HasDatabaseName("ix_order_details_product_id");

            b.ToTable("order_details", (string)null);
        });

        modelBuilder.Entity("DrugStore.Domain.ProductAggregate.Product", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<Guid?>("CategoryId")
                .HasColumnType("uuid")
                .HasColumnName("category_id");

            b.Property<DateTime>("CreatedDate")
                .ValueGeneratedOnAdd()
                .HasColumnType("timestamp with time zone")
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(7609))
                .HasColumnName("created_date");

            b.Property<string>("Detail")
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("character varying(500)")
                .HasColumnName("detail");

            b.Property<ProductPrice>("Price")
                .IsRequired()
                .IsUnicode(true)
                .HasColumnType("jsonb")
                .HasColumnName("price");

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

            b.Property<int>("Status")
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
                .HasDefaultValue(new DateTime(2024, 2, 28, 10, 58, 48, 401, DateTimeKind.Utc).AddTicks(8110))
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

        modelBuilder.Entity("DrugStore.Domain.ProductAggregate.ProductImage", b =>
        {
            b.Property<Guid>("ProductId")
                .HasColumnType("uuid")
                .HasColumnName("product_id");

            b.Property<string>("Alt")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("alt");

            b.Property<string>("ImageUrl")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("image_url");

            b.Property<bool>("IsMain")
                .HasColumnType("boolean")
                .HasColumnName("is_main");

            b.Property<string>("Title")
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("title");

            b.HasKey("ProductId")
                .HasName("pk_product_images");

            b.ToTable("product_images", (string)null);
        });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
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

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
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

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
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

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
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

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
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

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.Order", b =>
        {
            b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationUser", "Customer")
                .WithMany("Orders")
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_orders_users_customer_id");

            b.Navigation("Customer");
        });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.OrderItem", b =>
        {
            b.HasOne("DrugStore.Domain.OrderAggregate.Order", "Order")
                .WithMany("OrderItems")
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.NoAction)
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

            b.Navigation("Category");
        });

        modelBuilder.Entity("DrugStore.Domain.ProductAggregate.ProductImage", b =>
        {
            b.HasOne("DrugStore.Domain.ProductAggregate.Product", "Product")
                .WithMany("Images")
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("fk_product_images_products_product_id");

            b.Navigation("Product");
        });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
        {
            b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationRole", null)
                .WithMany()
                .HasForeignKey("RoleId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
        });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
        {
            b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationUser", null)
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
        });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
        {
            b.HasOne("DrugStore.Domain.IdentityAggregate.ApplicationUser", null)
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
        });

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
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

        modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
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

        modelBuilder.Entity("DrugStore.Domain.IdentityAggregate.ApplicationUser", b =>
        {
            b.Navigation("Orders");
        });

        modelBuilder.Entity("DrugStore.Domain.OrderAggregate.Order", b =>
        {
            b.Navigation("OrderItems");
        });

        modelBuilder.Entity("DrugStore.Domain.ProductAggregate.Product", b =>
        {
            b.Navigation("Images");

            b.Navigation("OrderItems");
        });
#pragma warning restore 612, 618
    }
}
