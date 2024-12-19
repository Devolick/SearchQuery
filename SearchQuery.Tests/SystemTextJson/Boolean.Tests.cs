using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchQuery.Tests;
using System.Collections.Generic;
using System.Linq;

namespace SearchQuery.SystemTextJson.Tests
{

    [TestClass]
    public class BooleanTests
    {
        public static List<TestEntity> entities;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
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
        public void StartsWith()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.StartsWith,
                        Values = new Values
                        {
                            true
                        }
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
        public void NotStartsWith()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.NotStartsWith,
                        Values = new Values
                        {
                            true
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 5 }.All(id => query.Select(s => s.Id).Contains(id)));
            }    
        }

        [TestMethod]
        public void Contains()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.Contains,
                        Values = new Values
                        {
                            false
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 5 }.All(id => query.Select(s => s.Id).Contains(id)));
            }  
        }

        [TestMethod]
        public void NotContains()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.NotContains,
                        Values = new Values
                        {
                            false
                        }
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
        public void EndsWith()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.EndsWith,
                        Values = new Values
                        {
                            true
                        }
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
        public void NotEndsWith()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.NotEndsWith,
                        Values = new Values
                        {
                            true
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 5 }.All(id => query.Select(s => s.Id).Contains(id)));
            }  
        }

        [TestMethod]
        public void Equal()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.Equal,
                        Values = new Values
                        {
                            true
                        }
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
        public void NotEqual()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.NotEqual,
                        Values = new Values
                        {
                            true
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 5 }.All(id => query.Select(s => s.Id).Contains(id)));
            }  
        }

        [TestMethod]
        public void Between()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.Between,
                        Values = new Values
                        {
                            false, true
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 4,5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 4,5 }.All(id => query.Select(s => s.Id).Contains(id)));
            }  
        }

        [TestMethod]
        public void NotBetween()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.NotBetween,
                        Values = new Values
                        {
                            false, false
                        }
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
        public void GreaterThan()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.GreaterThan,
                        Values = new Values
                        {
                            false
                        }
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
        public void GreaterThanOrEqual()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.GreaterThanOrEqual,
                        Values = new Values
                        {
                            false
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 4,5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 4,5 }.All(id => query.Select(s => s.Id).Contains(id)));
            }     
        }

        [TestMethod]
        public void LessThan()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.LessThan,
                        Values = new Values
                        {  
                            true
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 5 }.All(id => query.Select(s => s.Id).Contains(id)));
            }   
        }

        [TestMethod]
        public void LessThanOrEqual()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Active),
                        Operation = Operation.LessThanOrEqual,
                        Values = new Values
                        {
                            true
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 4,5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 4,5 }.All(id => query.Select(s => s.Id).Contains(id)));
            } 
        }
        


    }
}