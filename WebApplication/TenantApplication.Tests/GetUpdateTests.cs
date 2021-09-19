using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TenantApplication.Services;

namespace TenantApplication.Tests
{
    public class Tests
    {
        private TenantsService _tenantsService;
        private Models.TenantsContext _tenantsContext;
        private Mapper _mapper;

        private ViewModels.Tenant _structure = new ViewModels.Tenant
        {
            ParentTenant = null,
            Name = "T1",
            TenantWorkflow = new ViewModels.TenantWorkflow
            {
                Workflow = new ViewModels.Workflow
                {
                    Name = "w1"
                }
            },

            ChildTenants = new List<ViewModels.Tenant>
            {
                new ViewModels.Tenant
                {
                    Name = "T2",
                    ChildTenants = new List<ViewModels.Tenant>
                    {
                        new ViewModels.Tenant
                        {
                            Name = "T5"
                        }
                    }
                },
                new ViewModels.Tenant
                {
                    Name = "T3"
                }
            }
        };

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<Models.TenantsContext>()
                .UseInMemoryDatabase("TenantsTestsDb")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
                .Options;

            var config = new MapperConfiguration(options => options.AddProfile(new MappingProfile()));
            _mapper = new Mapper(config);

            _tenantsContext = new Models.TenantsContext(dbContextOptions);
            _tenantsService = new TenantsService(_tenantsContext);
        }

        [Test, Order(1)]
        public async Task GetRootTest()
        {

            var rootTenant = _mapper.Map<Models.Tenant>(_structure);

            _tenantsContext.Add(rootTenant);
            await _tenantsContext.SaveChangesAsync();

            var actual = await _tenantsService.GetRootTenant();

            Assert.AreEqual(_structure.Name, actual.Name);

            Assert.AreEqual(_structure.TenantWorkflow.Workflow.Name,
                actual.TenantWorkflow.Workflow.Name);

            Assert.AreEqual(_structure.ChildTenants[0].Name,
                actual.ChildTenants[0].Name);

            Assert.AreEqual(_structure.ChildTenants[0].ChildTenants[0].Name,
                actual.ChildTenants[0].ChildTenants[0].Name);
        }

        [Test, Order(2)]
        public async Task UpdateTenantTest()
        {
            var root = await _tenantsService.GetRootTenant();
            _structure = _mapper.Map<ViewModels.Tenant>(root);
            
            _structure.ChildTenants.Add(new ViewModels.Tenant
            {
                Name = "T4"
            });

            var expected = _mapper.Map<Models.Tenant>(_structure);
            await _tenantsService.UpdateTenant(expected);

            var actual = await _tenantsService.GetRootTenant();

            Assert.AreEqual(expected.ChildTenants[2].Name, actual.ChildTenants[2].Name);
        }

        [Test, Order(3)]
        public async Task DeleteBehaviorCascadeTest()
        {
            var root = await _tenantsService.GetRootTenant();

            _tenantsContext.Tenants.Remove(root);
            await _tenantsContext.SaveChangesAsync();

            Assert.IsEmpty(_tenantsContext.Tenants.AsEnumerable());

            Assert.IsEmpty(_tenantsContext.TenantWorkflows.AsEnumerable());
        }
    }
}