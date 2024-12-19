using System;
using Microsoft.EntityFrameworkCore;

namespace SearchQuery.Tests
{
    public class TestDatabase : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyInMemoryDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<TestEntity>().HasData(
            new TestEntity
            {
                Id = 1,
                FullName = "Terese Chestnutt",
                Payment = 4,
                Created = DateTime.Parse("2024-09-15T11:25:12"),
                Active = null,
                Description = "primis in faucibus orci luctus et ultrices posuere cubilia curae mauris"
            },
            new TestEntity
            {
                Id = 2,
                FullName = "Britteny Williams",
                Payment = 83,
                Created = DateTime.Parse("2024-01-29T14:15:33"),
                Active = null,
                Description = null
            },
            new TestEntity
            {
                Id = 3,
                FullName = "Pamela Corrie",
                Payment = 54,
                Created = DateTime.Parse("2024-09-01T10:13:43"),
                Active = null,
                Description = null
            },
            new TestEntity
            {
                Id = 4,
                FullName = "Amie Thewles",
                Payment = 83,
                Created = DateTime.Parse("2024-12-27T20:44:24"),
                Active = true,
                Description = null
            },
            new TestEntity
            {
                Id = 5,
                FullName = "Bucky Ornells",
                Payment = 75,
                Created = DateTime.Parse("2024-04-24T01:06:27"),
                Active = false,
                Description = "platea dictumst aliquam augue quam sollicitudin vitae consectetuer eget rutrum at lorem integer tincidunt ante vel ipsum praesent blandit"
            },
            new TestEntity
            {
                Id = 6,
                FullName = "Ashton Drees",
                Payment = 41,
                Created = DateTime.Parse("2024-06-08T23:25:50"),
                Active = null,
                Description = null
            },
            new TestEntity
            {
                Id = 7,
                FullName = "Ferguson Pietesch",
                Payment = null,
                Created = DateTime.Parse("2024-02-04T00:44:59"),
                Active = null,
                Description = "aliquam erat volutpat in congue etiam justo etiam pretium iaculis justo in hac habitasse platea dictumst"
            },
            new TestEntity
            {
                Id = 8,
                FullName = "Willi Knowles",
                Payment = 67,
                Created = DateTime.Parse("2024-07-12T03:36:18"),
                Active = null,
                Description = null
            },
            new TestEntity
            {
                Id = 9,
                FullName = null,
                Payment = 54,
                Created = DateTime.Parse("2023-10-12T04:41:59"),
                Active = null,
                Description = "consectetuer eget rutrum at lorem integer tincidunt ante vel ipsum praesent blandit lacinia erat vestibulum sed magna at"
            },
            new TestEntity
            {
                Id = 10,
                FullName = "Shelba McFaul",
                Payment = 66,
                Created = null,
                Active = null,
                Description = null
            }
            );
        }
    }

}