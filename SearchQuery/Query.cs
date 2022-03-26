namespace SearchQuery
{
    public class Query
    {
        public DateTime? Timestamp { get; set; }
        public DateTime? Bookmark { get; set; }
        public List<QueryCondition> Conditions { get; set; }
        public bool OrElse { get; set; }
        public bool Fuzzy { get; set; }

        public Query()
        {
            Conditions = new List<QueryCondition>();
        }

        public IQueryable<T> ToQuery<T>(IQueryable<T> set)
            where T : class
        {
            var searchBuilder = new QueryBuilder(this);
            var predicate = searchBuilder.Build<T>();
            var result = set.Where(predicate);

            return result;
        }
        public IEnumerable<T> ToQuery<T>(IEnumerable<T> set)
            where T : class
        {
            var searchBuilder = new QueryBuilder(this);
            var predicate = searchBuilder.Compile<T>();
            var result = set.Where(predicate);

            return result;
        }
    }
}
