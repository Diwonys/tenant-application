using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantApplication.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TenantWorkflow TenantWorkflow { get; set; }

        public List<Tenant> ChildTenants { get; set; }
        public Tenant ParentTenant { get; set; }
        public int? ParentTenantId { get; set; }
    }
}
