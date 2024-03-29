﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TenantApplication.Models;

namespace TenantApplication.Migrations
{
    [DbContext(typeof(TenantsContext))]
    partial class TenantsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("TenantApplication.Models.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("ParentTenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentTenantId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("TenantApplication.Models.TenantWorkflow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsSharedWorkflow")
                        .HasColumnType("boolean");

                    b.Property<int?>("TenantId")
                        .HasColumnType("integer");

                    b.Property<int?>("WorkflowId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TenantId")
                        .IsUnique();

                    b.HasIndex("WorkflowId");

                    b.ToTable("TenantWorkflows");
                });

            modelBuilder.Entity("TenantApplication.Models.Workflow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Workflows");
                });

            modelBuilder.Entity("TenantApplication.Models.Tenant", b =>
                {
                    b.HasOne("TenantApplication.Models.Tenant", "ParentTenant")
                        .WithMany("ChildTenants")
                        .HasForeignKey("ParentTenantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ParentTenant");
                });

            modelBuilder.Entity("TenantApplication.Models.TenantWorkflow", b =>
                {
                    b.HasOne("TenantApplication.Models.Tenant", "Tenant")
                        .WithOne("TenantWorkflow")
                        .HasForeignKey("TenantApplication.Models.TenantWorkflow", "TenantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TenantApplication.Models.Workflow", "Workflow")
                        .WithMany()
                        .HasForeignKey("WorkflowId");

                    b.Navigation("Tenant");

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("TenantApplication.Models.Tenant", b =>
                {
                    b.Navigation("ChildTenants");

                    b.Navigation("TenantWorkflow");
                });
#pragma warning restore 612, 618
        }
    }
}
