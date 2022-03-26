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
                        Operation = SearchOperation.StartsWith,
                        Values = new SearchCollection
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
                        Operation = SearchOperation.NotStartsWith,
                        Values = new SearchCollection
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
                        Operation = SearchOperation.EndsWith,
                        Values = new SearchCollection
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
                        Operation = SearchOperation.NotEndsWith,
                        Values = new SearchCollection
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
                        Operation = SearchOperation.Contains,
                        Values = new SearchCollection
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
                        Operation = SearchOperation.NotContains,
                        Values = new SearchCollection
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
                        Operation = SearchOperation.Equal,
                        Values = new SearchCollection
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
                        Operation = SearchOperation.NotEqual,
                        Values = new SearchCollection
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
                        Operation = SearchOperation.Equal,
                        Values = new SearchCollection { null }
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
                        Operation = SearchOperation.NotEqual,
                        Values = new SearchCollection { null }
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
                        Operation = SearchOperation.LessThan,
                        Values = new SearchCollection { "Dzmitry Dym" }
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
                        Operation = SearchOperation.LessThanOrEqual,
                        Values = new SearchCollection { "Dzmitry Dym" }
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
                        Operation = SearchOperation.GreaterThan,
                        Values = new SearchCollection { "Dzmitry Dym" }
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
                        Operation = SearchOperation.GreaterThanOrEqual,
                        Values = new SearchCollection { "Dzmitry Dym" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav" || a.UserName == "Dzmitry Dym"));
        }


    }
}