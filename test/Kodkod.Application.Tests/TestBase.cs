using System;
using System.Collections.Generic;
using Kodkod.Core.Entities;
using Kodkod.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Kodkod.Application.Tests
{
    public class TestBase
    {
        public KodkodDbContext GetEmptyDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KodkodDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            var inMemoryContext = new KodkodDbContext(optionsBuilder.Options);

            return inMemoryContext;
        }

        public KodkodDbContext GetInitializedDbContext()
        {
            var inMemoryContext = GetEmptyDbContext();

            var testUsers = new List<ApplicationUser>
            {
                new ApplicationUser {UserName = "A"},
                new ApplicationUser {UserName = "B"},
                new ApplicationUser {UserName = "C"},
                new ApplicationUser {UserName = "D"},
                new ApplicationUser {UserName = "E"},
                new ApplicationUser {UserName = "F"}
            };

            inMemoryContext.AddRange(testUsers);
            inMemoryContext.SaveChanges();

            return inMemoryContext;
        }
    }
}
