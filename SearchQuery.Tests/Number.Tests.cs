using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SearchQuery.Tests
{

    [TestClass]
    public class NumberTests
    {
        public static List<TestObject> entities;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            entities = new List<TestObject>
            {
                new TestObject
                {
                    Id = 1,
                    UserName = "Dzmitry Dym",
                    Payment = 100.111m
                },
                new TestObject
                {
                    Id = 2,
                    UserName = "Nadia Krav",
                    Payment = 50.222m
                },
                new TestObject
                {
                    Id = 3,
                    UserName = "Alex Man",
                    Payment = 10.333m
                },
                new TestObject
                {
                    Id = 4,
                    UserName = null,
                    Payment = null
                }
            };
        }

        [ClassCleanup]
        public static void Cleanup()
        {

        }

        // Operations

        [TestMethod]
        public void StartsWith()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.StartsWith,
                        Values = new QueryCollection
                        {
                            10.333m
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Alex Man"));
        }

        [TestMethod]
        public void NotStartsWith()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.NotStartsWith,
                        Values = new QueryCollection
                        {
                            10.333m
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Alex Man"));
        }

        [TestMethod]
        public void EndsWith()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.EndsWith,
                        Values = new QueryCollection
                        {
                            "Dym"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Dzmitry Dym"));
        }

        [TestMethod]
        public void NotEndsWith()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.NotEndsWith,
                        Values = new QueryCollection
                        {
                            "Dym"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Dzmitry Dym"));
        }

        [TestMethod]
        public void Contains()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.Contains,
                        Values = new QueryCollection
                        {
                            "y"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Dzmitry Dym"));
        }

        [TestMethod]
        public void NotContains()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.NotContains,
                        Values = new QueryCollection
                        {
                            "y"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Dzmitry Dym"));
        }

        [TestMethod]
        public void Equal()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.Equal,
                        Values = new QueryCollection
                        {
                            "Nadia Krav"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav"));
        }

        [TestMethod]
        public void NotEqual()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.NotEqual,
                        Values = new QueryCollection
                        {
                            "Nadia Krav"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Nadia Krav"));
        }

        [TestMethod]
        public void Null()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.Equal,
                        Values = new QueryCollection { null }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == null));
        }

        [TestMethod]
        public void NotNull()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.NotEqual,
                        Values = new QueryCollection { null }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != null));
        }

        [TestMethod]
        public void LessThan()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.LessThan,
                        Values = new QueryCollection { "Dzmitry Dym" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(a => a.UserName == "Alex Man" || a.UserName == null));
        }

        [TestMethod]
        public void LessThanOrEqual()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.LessThanOrEqual,
                        Values = new QueryCollection { "Dzmitry Dym" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Nadia Krav"));
        }

        [TestMethod]
        public void GreaterThan()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.GreaterThan,
                        Values = new QueryCollection { "Dzmitry Dym" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav"));
        }

        [TestMethod]
        public void GreaterThanOrEqual()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.Payment),
                        Operation = QueryOperation.GreaterThanOrEqual,
                        Values = new QueryCollection { "Dzmitry Dym" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav" || a.UserName == "Dzmitry Dym"));
        }


    }
}