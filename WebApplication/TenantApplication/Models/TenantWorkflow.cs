using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantApplication.Models
{
    public class TenantWorkflow
    {
        public int Id { get; set; }

        public bool IsSharedWorkflow { get; set; }

        public Tenant Tenant { get; set; }
        public int? TenantId { get; set; }

        public Workflow Workflow { get; set; }
        public int? WorkflowId { get; set; }
    }
}
