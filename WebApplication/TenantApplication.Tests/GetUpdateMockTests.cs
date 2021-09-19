using AutoMapper;
using Microsoft.Extensions.Localization;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantApplication.Services;

namespace TenantApplication.Tests
{
    class GetUpdateMockTests
    {
        private ITenantsService _tenantsService;
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
            _tenantsService = Substitute.For<ITenantsService>();

            var config = new MapperConfiguration(options => options.AddProfile(new MappingProfile()));
            _mapper = new Mapper(config);
        }

        [Test]
        public async Task GetRootTest()
        {
            var rootTenant = _mapper.Map<Models.Tenant>(_structure);
            _tenantsService.GetRootTenant()
                .Returns(Task.Run(() => rootTenant));

            var actual = await _tenantsService.GetRootTenant();

            Assert.AreEqual(_structure.Name, actual.Name);

            Assert.AreEqual(_structure.TenantWorkflow.Workflow.Name,
                actual.TenantWorkflow.Workflow.Name);

            Assert.AreEqual(_structure.ChildTenants[0].Name,
                actual.ChildTenants[0].Name);

            Assert.AreEqual(_structure.ChildTenants[0].ChildTenants[0].Name,
                actual.ChildTenants[0].ChildTenants[0].Name);
        }

        [Test] 
        public async Task UpdateTenantTest()
        {
            _structure.ChildTenants.Add(new ViewModels.Tenant
            {
                Name = "T4"
            });
            var rootTenant = _mapper.Map<Models.Tenant>(_structure);
            _tenantsService.UpdateTenant(rootTenant).Returns(Task.Run(()=> rootTenant));
            
            var expected = _mapper.Map<Models.Tenant>(_structure);
            var actual = await _tenantsService.UpdateTenant(rootTenant);

            Assert.AreEqual(expected.ChildTenants[2].Name, actual.ChildTenants[2].Name);
        }
    }
}
