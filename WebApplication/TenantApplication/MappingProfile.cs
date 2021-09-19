using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenantApplication
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Tenant, ViewModels.Tenant>();
            CreateMap<ViewModels.Tenant, Models.Tenant>();

            CreateMap<Models.Workflow, ViewModels.Workflow>();
            CreateMap<ViewModels.Workflow, Models.Workflow>();

            CreateMap<Models.TenantWorkflow, ViewModels.TenantWorkflow>();
            CreateMap<ViewModels.TenantWorkflow, Models.TenantWorkflow>();
        }
    }
}
