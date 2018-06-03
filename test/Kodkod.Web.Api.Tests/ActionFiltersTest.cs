using System;
using System.Collections.Generic;
using Kodkod.Core.Roles;
using Kodkod.Web.Core.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace Kodkod.Web.Api.Tests
{
    public class ActionFiltersTest : ApiTestBase
    {
        [Fact]
        public void TestKodkodDbContextActionFilter()
        {
            var kodkodDbContextActionFilter = new KodkodDbContextActionFilter(KodkodInMemoryContext);
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            var actionExecutedContext = new ActionExecutedContext(actionContext,new List<IFilterMetadata>(), null);
            var testRole = new Role { Name = "test_role", Id = Guid.NewGuid() };

            KodkodInMemoryContext.Roles.Add(testRole);
            kodkodDbContextActionFilter.OnActionExecuted(actionExecutedContext);
            
            var insertedTestRole = KodkodInMemoryContext.Roles.Find(testRole.Id);
            Assert.Equal(testRole.Name, insertedTestRole.Name);
        }
    }
}
