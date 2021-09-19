using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using TenantApplication.Services;

namespace TenantApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITenantsService _tenantsService;

        public TenantsController(ITenantsService tenantsService, IMapper mapper)
        {
            _tenantsService = tenantsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ViewModels.Tenant>> GetTenants()
        {
            var root = await _tenantsService.GetRootTenant();

            if (root == null)
                return BadRequest();

            return _mapper.Map<ViewModels.Tenant>(root);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ViewModels.Tenant tenantViewModel)
        {
            if (tenantViewModel == null)
                return BadRequest();

            var tenant = _mapper.Map<Models.Tenant>(tenantViewModel);
            var updatedTenant = await _tenantsService.UpdateTenant(tenant);

            return Ok(updatedTenant);
        }
    }
}
