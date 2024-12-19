using System;

namespace SearchQuery.Tests
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? Payment { get; set; }
        public DateTime? Created { get; set; }
        public bool? Active { get; set; }
        public string? Description { get; set; }
    }

}