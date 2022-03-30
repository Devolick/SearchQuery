using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchQuery.Tests
{
    [TestClass]
    public class StringEFTests
    {
        public static TestDatabase model;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var builder = new DbContextOptionsBuilder<TestDatabase>();
            builder.UseInMemoryDatabase("SearchQueryDatabase");
            model = new TestDatabase(builder.Options);
            var entities = new List<TestObject>
            {
                new TestObject
                {
                    Id = 1,
                    UserName = "Dzmitry Dym"
                },
                new TestObject
                {
                    Id = 2,
                    UserName = "Nadia Krav"
                },
                new TestObject
                {
                    Id = 3,
                    UserName = "Alex Man"
                },
                new TestObject
                {
                    Id = 4,
                    UserName = null
                }
            };
            model.Tests.AddRange(entities);
            model.SaveChanges();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            model.Dispose();
        }

        [TestMethod]
        public void StartsWith()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.StartsWith,
                        Values = new QueryCollection
                        {
                            "Alex"
                        }
                    }
                }
            };

            var result = model.Tests.Search(searchQuery).ToList();

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Alex Man"));
        }

    }
}