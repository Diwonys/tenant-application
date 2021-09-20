using Microsoft.EntityFrameworkCore;
using NSubstitute.Routing.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using TenantApplication.Models;

namespace TenantApplication.Services
{
    public class TenantsService : ITenantsService
    {
        private TenantsContext _tenantsContext;

        public TenantsService(TenantsContext tenantsContext)
        {
            _tenantsContext = tenantsContext;
            //InitializeDB().Wait();
        }

        public async Task<Tenant> GetRootTenant()
        {
            var tenants = await _tenantsContext.Tenants
                .Include(e => e.TenantWorkflow)
                .ThenInclude(e => e.Workflow)
                .Include(e => e.ChildTenants)
                .ToListAsync();

            return tenants.FirstOrDefault(e => e.ParentTenantId == null);

        } 

        public async Task<Tenant> UpdateTenant(Tenant tenant)
        {
            _tenantsContext.Tenants.Update(tenant);
            await _tenantsContext.SaveChangesAsync();

            return tenant;
        }

        private async Task InitializeDB()
        {
            if (!_tenantsContext.Tenants.Any())
            {
                var workflow1 = new Models.Workflow
                {
                    Name = "w1"
                };
                var tenant1 = new Models.Tenant
                {
                    Name = "t1",
                    ParentTenant = null,
                    TenantWorkflow = new()
                    {
                        Workflow = workflow1
                    }
                };
                var tenant2 = new Models.Tenant
                {
                    Name = "t2",
                    ParentTenant = tenant1,
                    TenantWorkflow = new()
                    {
                        Workflow = workflow1
                    }
                };
                var tenant3 = new Models.Tenant
                {
                    Name = "t3",
                    ParentTenant = tenant1
                };
                var tenant4 = new Models.Tenant
                {
                    Name = "t4",
                    ParentTenant = tenant3
                };

                _tenantsContext.Add(tenant1);
                _tenantsContext.Add(tenant2);
                _tenantsContext.Add(tenant3);
                _tenantsContext.Add(tenant4);

                await _tenantsContext.SaveChangesAsync();
            }
        }
    }

}