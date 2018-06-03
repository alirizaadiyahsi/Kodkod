using System;
using Kodkod.Core.Roles;
using Kodkod.Web.Core.ActionFilters;
using Microsoft.AspNetCore.Mvc.Filters;
using NSubstitute;
using Xunit;

namespace Kodkod.Web.Api.Tests
{
    public class ActionFiltersTest : ApiTestBase
    {
        [Fact]
        public void TestKodkodDbContextActionFilter()
        {
            var kodkodDbContextActionFilter = new KodkodDbContextActionFilter(KodkodInMemoryContext);
            var actionExecutingContext = Substitute.For<ActionExecutedContext>();
            var testRole = new Role { Name = "test_role", Id = Guid.NewGuid() };

            KodkodInMemoryContext.Roles.Add(testRole);
            kodkodDbContextActionFilter.OnActionExecuted(actionExecutingContext);

            var insertedTestRole = KodkodInMemoryContext.Roles.Find(testRole.Id);
            Assert.Equal(testRole.Name, insertedTestRole.Name);
        }
    }
}
