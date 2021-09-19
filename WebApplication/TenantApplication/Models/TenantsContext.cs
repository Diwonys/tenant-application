using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TenantApplication.Models
{
    public class TenantsContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<TenantWorkflow> TenantWorkflows { get; set; }

        public TenantsContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>()
                .HasMany(e => e.ChildTenants)
                .WithOne(e => e.ParentTenant)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TenantWorkflow>()
                .HasOne(e => e.Tenant)
                .WithOne(e => e.TenantWorkflow)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
