using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantApplication.Services
{
    public interface ITenantsService
    {
        public Task<Models.Tenant> GetRootTenant();

        public Task<Models.Tenant> UpdateTenant(Models.Tenant tenant);
    }
}
