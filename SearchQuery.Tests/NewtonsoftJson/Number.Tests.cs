using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchQuery.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using SearchQuery.NewtonsoftJson;

namespace SearchQuery.NewtonsoftJson.Tests
{

    [TestClass]
    public class NumberTests
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.StartsWith,
                        Values = new Values
                        {
                            5
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 3,9 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 3,9 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.NotStartsWith,
                        Values = new Values
                        {
                            5
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,4,5,6,8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,4,5,6,8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.Contains,
                        Values = new Values
                        {
                            6
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.NotContains,
                        Values = new Values
                        {
                            6
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,4,5,6,9 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,4,5,6,9 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.EndsWith,
                        Values = new Values
                        {
                            4
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,3,9 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,3,9 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.NotEndsWith,
                        Values = new Values
                        {
                            4
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 2,4,5,6,8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 2,4,5,6,8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.Equal,
                        Values = new Values
                        {
                            66
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.NotEqual,
                        Values = new Values
                        {
                            66
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,3,4,5,6,8,9 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,3,4,5,6,8,9 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.Between,
                        Values = new Values
                        {
                            41, 67
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(result.Count() > 0 && result.All(a => new []{ 3,6,8,9,10 }.Contains(a.Id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(result.Count() > 0 && result.All(a => new []{ 3,6,8,9,10 }.Contains(a.Id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.NotBetween,
                        Values = new Values
                        {
                            41, 67
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,2,4,5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,2,4,5 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.GreaterThan,
                        Values = new Values
                        {
                            67
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 2,4,5 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 2,4,5 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.GreaterThanOrEqual,
                        Values = new Values
                        {
                            67
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 2,4,5,8 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 2,4,5,8 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.LessThan,
                        Values = new Values
                        {
                            67
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,3,6,9,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,3,6,9,10 }.All(id => query.Select(s => s.Id).Contains(id)));
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.LessThanOrEqual,
                        Values = new Values
                        {
                            67
                        }
                    }
                }
            };
            
            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 1,3,6,9,8,10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 1,3,6,9,8,10 }.All(id => query.Select(s => s.Id).Contains(id)));
            } 
        }
        
        // Check

        [TestMethod]
        public void ConvertNumber()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.Equal,
                        Values = new Values
                        {
                            66L
                        }
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(new []{ 10 }.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(new []{ 10 }.All(id => query.Select(s => s.Id).Contains(id)));
            }  
        }
        
        [TestMethod]
        public void StringArgumentException()
        {
            var searchQuery = new JSearch
            {
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.Equal,
                        Values = new Values
                        {
                            "66"
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
                        Field = nameof(TestEntity.Payment),
                        Operation = Operation.Equal,
                        Values = new Values
                        {
                            false
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