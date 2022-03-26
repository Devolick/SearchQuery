using Microsoft.EntityFrameworkCore;

namespace SearchQuery.Tests
{
    public class TestDatabase: DbContext
    {
        public DbSet<TestObject> Tests { get; set; }

        public TestDatabase(DbContextOptions options)
            :base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestObject>()
                .HasKey(t => t.Id);
        }

    }

}