using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SearchQuery.Tests
{
    [TestClass]
    public class StringTests
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
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.StartsWith,
                        Values = new QueryCollection
                        {
                            "Alex"
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
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotStartsWith,
                        Values = new QueryCollection
                        {
                            "Alex"
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
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
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.GreaterThanOrEqual,
                        Values = new QueryCollection { "Dzmitry Dym" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav" || a.UserName == "Dzmitry Dym"));
        }

        // Lower


        [TestMethod]
        public void StartsWithLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.StartsWith,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection
                        {
                            "AlEx"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Alex Man"));

        }

        [TestMethod]
        public void NotStartsWithLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotStartsWith,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection
                        {
                            "Alex"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Alex Man"));
        }

        [TestMethod]
        public void EndsWithLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.EndsWith,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection
                        {
                            "DYm"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Dzmitry Dym"));
        }

        [TestMethod]
        public void NotEndsWithLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotEndsWith,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection
                        {
                            "DYm"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Dzmitry Dym"));
        }

        [TestMethod]
        public void ContainsLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.Contains,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection
                        {
                            "Y"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Dzmitry Dym"));
        }

        [TestMethod]
        public void NotContainsLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotContains,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection
                        {
                            "Y"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Dzmitry Dym"));
        }

        [TestMethod]
        public void EqualLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.Equal,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection
                        {
                            "Nadia KRAV"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav"));
        }

        [TestMethod]
        public void NotEqualLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotEqual,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection
                        {
                            "Nadia KRAV"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Nadia Krav"));
        }

        [TestMethod]
        public void NullLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.Equal,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection { null }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == null));
        }

        [TestMethod]
        public void NotNullLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotEqual,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection { null }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != null));
        }

        [TestMethod]
        public void LessThanLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.LessThan,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection { "Dzmitry DYM" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(a => a.UserName == "Alex Man" || a.UserName == null));
        }

        [TestMethod]
        public void LessThanOrEqualLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.LessThanOrEqual,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection { "Dzmitry DYM" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Nadia Krav"));
        }

        [TestMethod]
        public void GreaterThanLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.GreaterThan,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection { "Dzmitry DYM" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav"));
        }

        [TestMethod]
        public void GreaterThanOrEqualLower()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.GreaterThanOrEqual,
                        Case = QueryCase.Lower,
                        Values = new QueryCollection { "Dzmitry DYM" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav" || a.UserName == "Dzmitry Dym"));
        }

        // Upper

        [TestMethod]
        public void StartsWithUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.StartsWith,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection
                        {
                            "AlEx"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Alex Man"));

        }

        [TestMethod]
        public void NotStartsWithUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotStartsWith,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection
                        {
                            "Alex"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Alex Man"));
        }

        [TestMethod]
        public void EndsWithUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.EndsWith,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection
                        {
                            "DYm"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Dzmitry Dym"));
        }

        [TestMethod]
        public void NotEndsWithUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotEndsWith,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection
                        {
                            "DYm"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Dzmitry Dym"));
        }

        [TestMethod]
        public void ContainsUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.Contains,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection
                        {
                            "Y"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Dzmitry Dym"));
        }

        [TestMethod]
        public void NotContainsUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotContains,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection
                        {
                            "Y"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Dzmitry Dym"));
        }

        [TestMethod]
        public void EqualUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.Equal,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection
                        {
                            "Nadia KRAV"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav"));
        }

        [TestMethod]
        public void NotEqualUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotEqual,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection
                        {
                            "Nadia KRAV"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Nadia Krav"));
        }

        [TestMethod]
        public void NullUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.Equal,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection { null }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == null));
        }

        [TestMethod]
        public void NotNullUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.NotEqual,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection { null }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != null));
        }

        [TestMethod]
        public void LessThanUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.LessThan,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection { "Dzmitry DYM" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(a => a.UserName == "Alex Man" || a.UserName == null));
        }

        [TestMethod]
        public void LessThanOrEqualUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.LessThanOrEqual,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection { "Dzmitry DYM" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.Any(a => a.UserName != "Nadia Krav"));
        }

        [TestMethod]
        public void GreaterThanUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.GreaterThan,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection { "Dzmitry DYM" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav"));
        }

        [TestMethod]
        public void GreaterThanOrEqualUpper()
        {
            var searchQuery = new Query
            {
                Conditions = new List<QueryCondition>
                {
                    new QueryCondition
                    {
                        Field = nameof(TestObject.UserName),
                        Operation = QueryOperation.GreaterThanOrEqual,
                        Case = QueryCase.Upper,
                        Values = new QueryCollection { "Dzmitry DYM" }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.Any(a => a.UserName == "Nadia Krav" || a.UserName == "Dzmitry Dym"));
        }
    }
}