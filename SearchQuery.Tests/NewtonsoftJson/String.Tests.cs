using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchQuery.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using SearchQuery.NewtonsoftJson;

namespace SearchQuery.NewtonsoftJson.Tests
{
    [TestClass]
    public class StringTests
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
        public void NotStartsWith()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.NotStartsWith,
                        Values = new Values
                        {
                            "Britteny"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,3,4,5,6,7,8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,3,4,5,6,7,8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.Contains,
                        Values = new Values
                        {
                            "el"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 3,5,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 3,5,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.NotContains,
                        Values = new Values
                        {
                            "el"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,4,6,7,8 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,4,6,7,8 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.EndsWith,
                        Values = new Values
                        {
                            "es"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 4, 6, 8 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 4, 6, 8 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.NotEndsWith,
                        Values = new Values
                        {
                            "es"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,5,7,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,5,7,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.Equal,
                        Values = new Values
                        {
                            "Ferguson Pietesch"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 7 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 7 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.NotEqual,
                        Values = new Values
                        {
                            "Ferguson Pietesch"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,4,5,6,8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,4,5,6,8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.Between,
                        Values = new Values
                        {
                            "Ashton",
                            "Willi"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,5,6,7,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,5,6,7,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.NotBetween,
                        Values = new Values
                        {
                            "Ashton",
                            "Willi"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 4,8 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 4,8 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.GreaterThan,
                        Values = new Values
                        {
                            "Ashton Drees"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,5,7,8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,5,7,8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.GreaterThanOrEqual,
                        Values = new Values
                        {
                            "Ashton Drees"
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,5,6,7,8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,5,6,7,8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.LessThan,
                        Values = new Values
                        {
                            "Willi Knowles"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,4,5,6,7,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,4,5,6,7,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.LessThanOrEqual,
                        Values = new Values
                        {
                            "Willi Knowles"
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,4,5,6,7,8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,4,5,6,7,8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
            } 
        }

        [TestMethod]
        public void NumberArgumentException()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.Equal,
                        Values = new Values
                        {
                            66
                        }
                    }
                }
            };

            Assert.ThrowsException<ArgumentException>(() => {
                var result = entities.Search(searchQuery);
            });
            Assert.ThrowsException<ArgumentException>(() => {
                using (var db = new TestDatabase()) {
                    db.Database.EnsureCreated();

                    var query = db.TestEntities.Search(searchQuery).ToList();
                }   
            });
        }

        [TestMethod]
        public void BooleanArgumentException()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.FullName),
                        Operation = Operation.Equal,
                        Values = new Values
                        {
                            true
                        }
                    }
                }
            };

            Assert.ThrowsException<ArgumentException>(() => {
                var result = entities.Search(searchQuery);
            });
            Assert.ThrowsException<ArgumentException>(() => {
                using (var db = new TestDatabase()) {
                    db.Database.EnsureCreated();

                    var query = db.TestEntities.Search(searchQuery).ToList();
                }   
            });
        }


    }
}