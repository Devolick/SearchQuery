using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchQuery.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using SearchQuery.NewtonsoftJson;
using System.Globalization;

namespace SearchQuery.NewtonsoftJson.Tests
{
    [TestClass]
    public class DateTimeTests
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

        public void Fn(Values values, int[] ids, Operation operation, Format format)
        {
            var searchQuery = new JSearch
            {
                Format = format,
                Conditions = new Conditions
                {
                    new Condition
                    {
                        Field = nameof(TestEntity.Created),
                        Operation = operation,
                        Values = values
                    }
                }
            };

            var result = entities.Search(searchQuery);

            Assert.IsTrue(ids.All(id => result.Select(s => s.Id).Contains(id)));

            using (var db = new TestDatabase()) {
                db.Database.EnsureCreated();

                var query = db.TestEntities.Search(searchQuery).ToList();

                Assert.IsTrue(ids.All(id => query.Select(s => s.Id).Contains(id)));
            }   
        }
        

        // Operations

        [TestMethod]
        public void StartsWith()
        {
            Fn(new Values { "2024-01" }, new []{ 2 }, Operation.StartsWith, Format.ISODateTime);  
        }

        [TestMethod]
        public void NotStartsWith()
        {
            Fn(new Values { "2024-01" }, new []{ 1,3,4,5,6,7,8,9 }, Operation.NotStartsWith, Format.ISODateTime);   
        }

        [TestMethod]
        public void Contains()
        {
            Fn(new Values { "09-01" }, new []{ 3 }, Operation.Contains, Format.ISODateTime);  
        }

        [TestMethod]
        public void NotContains()
        {
            Fn(new Values { "09-01" }, new []{ 1,2,4,5,6,7,8,9 }, Operation.NotContains, Format.ISODateTime);  
        }

        [TestMethod]
        public void EndsWith()
        {
            Fn(new Values { "44:24" }, new []{ 4 }, Operation.EndsWith, Format.ISODateTime);  
        }

        [TestMethod]
        public void NotEndsWith()
        {
            Fn(new Values { "44:24" }, new []{ 1,2,3,5,6,7,8,9 }, Operation.NotEndsWith, Format.ISODateTime);
        }

        [TestMethod]
        public void Equal()
        {
            Fn(new Values { DateTime.Parse("2024-02-04T00:44:59") }, new []{ 7 }, Operation.Equal, Format.ISODateTime);
        }
        
        [TestMethod]
        public void EqualAsString()
        {
            Fn(new Values { "2024-02-04T00:44:59" }, new []{ 7 }, Operation.Equal, Format.ISODateTime);
        }

        [TestMethod]
        public void NotEqual()
        {
            Fn(new Values { DateTime.Parse("2024-02-04T00:44:59") }, new []{ 1,2,3,4,5,6,8,9 }, Operation.NotEqual, Format.ISODateTime);
        }

        [TestMethod]
        public void NotEqualAsString()
        {
            Fn(new Values { "2024-02-04T00:44:59" }, new []{ 1,2,3,4,5,6,8,9 }, Operation.NotEqual, Format.ISODateTime);
        }

        [TestMethod]
        public void Between()
        {
            Fn(new Values { DateTime.Parse("2024-02-04T00:44:59"), DateTime.Parse("2024-04-27T20:44:24") }, 
                new []{ 5,7 }, Operation.Between, Format.ISODateTime);
        }

        [TestMethod]
        public void BetweenAsString()
        {
            Fn(new Values { "2024-02-04T00:44:59", "2024-04-27T20:44:24" }, 
                new []{ 5,7 }, Operation.Between, Format.ISODateTime);
        }

        [TestMethod]
        public void NotBetween()
        {
            Fn(new Values { DateTime.Parse("2024-02-04T00:44:59"), DateTime.Parse("2024-04-27T20:44:24") }, 
                new []{ 1,2,3,4,6,8,9 }, Operation.NotBetween, Format.ISODateTime);
        }

        [TestMethod]
        public void NotBetweenAsString()
        {
            Fn(new Values { "2024-02-04T00:44:59", "2024-04-27T20:44:24" }, 
                new []{ 1,2,3,4,6,8,9 }, Operation.NotBetween, Format.ISODateTime);
        }


        [TestMethod]
        public void GreaterThan()
        {
            Fn(new Values { DateTime.Parse("2024-04-24T01:06:27") }, 
                new []{ 1,3,4,6,8 }, Operation.GreaterThan, Format.ISODateTime);
        }

        [TestMethod]
        public void GreaterThanAsString()
        {
            Fn(new Values { "2024-04-24T01:06:27" }, 
                new []{ 1,3,4,6,8 }, Operation.GreaterThan, Format.ISODateTime);
        }


        [TestMethod]
        public void GreaterThanOrEqual()
        {
            Fn(new Values { DateTime.Parse("2024-04-27T20:44:24") }, 
                new []{ 1,3,4,6,8, }, Operation.GreaterThanOrEqual, Format.ISODateTime);
        }

        [TestMethod]
        public void GreaterThanOrEqualAsString()
        {
            Fn(new Values { "2024-04-27T20:44:24" }, 
                new []{ 1,3,4,6,8 }, Operation.GreaterThanOrEqual, Format.ISODateTime);
        }

        [TestMethod]
        public void LessThan()
        {
            Fn(new Values { DateTime.Parse("2024-04-27T20:44:24") }, 
                new []{ 2,5,7 }, Operation.LessThan, Format.ISODateTime);
        }

        [TestMethod]
        public void LessThanAsString()
        {
            Fn(new Values { "2024-04-27T20:44:24" }, 
                new []{ 2,5,7 }, Operation.LessThan, Format.ISODateTime);
        }


        [TestMethod]
        public void LessThanOrEqual()
        {
            Fn(new Values { DateTime.Parse("2024-06-08T23:25:50") }, 
                new []{ 2,5,6,7,9 }, Operation.LessThanOrEqual, Format.ISODateTime);
        }
        
        [TestMethod]
        public void LessThanOrEqualAsString()
        {
            Fn(new Values { "2024-06-08T23:25:50" }, 
                new []{ 2,5,6,7,9 }, Operation.LessThanOrEqual, Format.ISODateTime);
        }
        

        // DateOnly Formats

        [TestMethod]
        public void DateOnly()
        {
            Fn(new Values { "2023-10-12" }, new []{ 9 }, Operation.Equal, Format.DateOnly);  
        }
        [TestMethod]
        public void TimeOnly()
        {
            Fn(new Values { "14:15:33" }, new []{ 2 }, Operation.Equal, Format.TimeOnly);    
        }
        [TestMethod]
        public void ISODateTime()
        {
            Fn(new Values { "2024-01-29T14:15:33" }, new []{ 2 }, Operation.Equal, Format.ISODateTime);    
        }
        [TestMethod]
        public void ISODateTimeWithOffset()
        {
            Fn(new Values { "2024-01-29T14:15:33+02:00" }, new []{ 2 }, Operation.Equal, Format.ISODateTimeWithOffset);    
        }
        [TestMethod]
        public void ISODateTimeUTC()
        {
            Fn(new Values { "2024-01-29T14:15:33Z" }, new []{ 2 }, Operation.Equal, Format.ISODateTimeUTC);    
        }
        [TestMethod]
        public void ISODateTimeWithMillisecondsUTC()
        {
            Fn(new Values { "2024-01-29T14:15:33.000Z" }, new []{ 2 }, Operation.Equal, Format.ISODateTimeWithMillisecondsUTC);    
        }
        
        // DateOnly Formats

        [TestMethod]
        public void DateDays()
        {
            Fn(new Values { "12" }, new []{ 9 }, Operation.Equal, Format.DateDays);  
        }
        [TestMethod]
        public void DateMonths()
        {
            Fn(new Values { "10" }, new []{ 9 }, Operation.Equal, Format.DateMonths);  
        }
        [TestMethod]
        public void DateYears()
        {
            Fn(new Values { "2023" }, new []{ 9 }, Operation.Equal, Format.DateYears);  
        }
        
        // TimeOnly Formats

        [TestMethod]
        public void TimeOn()
        {
            Fn(new Values { "14:15" }, new []{ 2 }, Operation.Equal, Format.TimeOn);   
        }
        [TestMethod]
        public void TimeFull()
        {
            Fn(new Values { "14:15:33.000" }, new []{ 2 }, Operation.Equal, Format.TimeFull);   
        }
        [TestMethod]
        public void TimeHours()
        {
            Fn(new Values { "14" }, new []{ 2 }, Operation.Equal, Format.TimeHours);   
        }
        [TestMethod]
        public void TimeMinutes()
        {
            Fn(new Values { "15" }, new []{ 2 }, Operation.Equal, Format.TimeMinutes);   
        }
        [TestMethod]
        public void TimeSeconds()
        {
            Fn(new Values { "33" }, new []{ 2 }, Operation.Equal, Format.TimeSeconds);   
        }
        [TestMethod]
        public void TimeMilliseconds()
        {
            Fn(new Values { "000" }, new []{ 2 }, Operation.Equal, Format.TimeMilliseconds);   
        }
        
    }
}