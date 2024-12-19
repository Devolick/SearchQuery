using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchQuery.Tests;
using System.Collections.Generic;
using System.Linq;
using SearchQuery.NewtonsoftJson;
using Newtonsoft.Json;

namespace SearchQuery.NewtonsoftJson.Tests
{
    [TestClass]
    public class GeneralTests
    {
        public static TaskObject taskObject;
        public static List<TestEntity> entities;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            taskObject = new TaskObject {
                Tags = new List<string> { "test", "next" }
            };
            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                entities = db.TestEntities.ToList();
            } 
        }

        [ClassCleanup]
        public static void Cleanup()
        {

        }

        // Operations

        [TestMethod]
        public void EmptyQuery()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                { }
            };
            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() != 0);
        }

        [TestMethod]
        public void IsColletion()
        {
            var isColletion = taskObject.Tags.GetType().IsColletion();
            Assert.IsTrue(isColletion);
        }
        [TestMethod]
        public void CaseUpper()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions {
                    new Condition
                    {
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.StartsWith,
                        Values = new Values
                        {
                            "terese"
                        },
                        Incase = Case.Upper
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1 }.All(id => query.Select(s => s.Id).Contains(id)));
            } 
        }
        
        [TestMethod]
        public void CaseLower()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions {
                    new Condition
                    {
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.StartsWith,
                        Values = new Values
                        {
                            "AMIE"
                        },
                        Incase = Case.Lower
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 4 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 4 }.All(id => query.Select(s => s.Id).Contains(id)));
            } 
        }

        [TestMethod]
        public void Conditions()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions {
                    new Condition
                    {
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.StartsWith,
                        Values = new Values
                        {
                            "Britteny"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 2 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 2 }.All(id => query.Select(s => s.Id).Contains(id)));
            }   
        }

        [TestMethod]
        public void Queries()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions {
                    new Query {
                        Conditions = new Conditions {
                            new Condition
                            {
                                Field = nameof(TestEntity.FullName),
                                Operation = Operation.StartsWith,
                                Values = new Values
                                {
                                    "Britteny"
                                }
                            }
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 2 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 2 }.All(id => query.Select(s => s.Id).Contains(id)));
            }  
        }

        [TestMethod]
        public void SerializeObject()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions {
                    new Query {
                        Conditions = new Conditions {
                            new Condition
                            {
                                Field = nameof(TestEntity.FullName),
                                Operation = Operation.StartsWith,
                                Values = new Values
                                {
                                    "Britteny"
                                }
                            }
                        }
                    }
                }
            };
            var result = searchQuery.ToJson();

            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void DeserializeObject()
        {
            var jsonSearchQuery = "{\"Operator\":0,\"Conditions\":[{\"Operator\":0,\"Conditions\":[{\"Field\":\"FullName\",\"Operation\":8,\"Values\":[\"Britteny\"],\"Case\":0}]}]}";

            var searchQuery = JSearch.FromJson(jsonSearchQuery);
            var query = (Query)searchQuery.Conditions[0];
            var condition = (Condition)query.Conditions[0];

            Assert.IsTrue(condition.Field == "FullName");
        }

        [TestMethod]
        public void SerializeInParams()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions {
                    new Query {
                        Conditions = new Conditions {
                            new Condition
                            {
                                Field = nameof(TestEntity.FullName),
                                Operation = Operation.StartsWith,
                                Values = new Values
                                {
                                    "Britteny"
                                }
                            }
                        }
                    }
                }
            };
            var pagingParams = new PagingParams {
                Search = searchQuery,
                Offset = 0,
                Limit = 100
            };
            var result = JsonConvert.SerializeObject(pagingParams);

            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void DeserializeInParams()
        {
            var jsonPagingParams = "{\"Search\":{\"Operator\":0,\"Conditions\":[{\"Operator\":0,\"Conditions\":[{\"Field\":\"FullName\",\"Operation\":8,\"Values\":[\"Britteny\"],\"Case\":0}]}]},\"Offset\":0,\"Limit\":100}";

            var pagingParams = JsonConvert.DeserializeObject<PagingParams>(jsonPagingParams);
            var query = (Query)pagingParams!.Search.Conditions[0];
            var condition = (Condition)query.Conditions[0];

            Assert.IsTrue(condition.Field == "FullName");
        }

        [TestMethod]
        public void DeserializeComplexConditions()
        {
            var jsonPagingParams = @"
            {
                ""Search"": {
                    ""Operator"": 0,
                    ""Conditions"": [
                        {
                            ""Field"": ""fullName"",
                            ""Operation"": 0,
                            ""Values"": [
                                ""Dennis Ritchie""
                            ]
                        },
                        {
                            ""Field"": ""created"",
                            ""Operation"": 12,
                            ""Values"": [
                                ""1990-12-31"",
                                ""2025-12-31""
                            ]
                        },
                        {
                            ""Operator"": 1,
                            ""Conditions"": [
                                {
                                    ""Field"": ""fullName"",
                                    ""Operation"": 8,
                                    ""Values"": [
                                        ""Dennis""
                                    ],
                                    ""Case"": 1
                                },
                                {
                                    ""Field"": ""fullName"",
                                    ""Operation"": 10,
                                    ""Values"": [
                                        ""Ritchie""
                                    ],
                                    ""Case"": 2
                                }
                            ]
                        }
                    ]
                },
                ""Offset"": 0,
                ""Limit"": 10
            }";

            var pagingParams = JsonConvert.DeserializeObject<PagingParams>(jsonPagingParams);

            Assert.IsTrue(pagingParams!.Search.Conditions[0].GetType() == typeof(Condition));
            Assert.IsTrue(pagingParams!.Search.Conditions[1].GetType() == typeof(Condition));
            Assert.IsTrue(pagingParams!.Search.Conditions[2].GetType() == typeof(Query));
        }
 
    }
}