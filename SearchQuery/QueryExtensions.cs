namespace SearchQuery
{
    public static class QueryExtensions
    {
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            Query query)
            where T : class
        {
            var searchBuilder = new QueryBuilder(query);
            var predicate = searchBuilder.Compile<T>();
            var result = set.Where(predicate);

            return result;
        }
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            Query query, int pageNumber, int pageSize)
            where T : class
        {
            return set.Search<T>(query)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> set,
            Query query)
            where T : class
        {
            var searchBuilder = new QueryBuilder(query);
            var predicate = searchBuilder.Build<T>();
            var result = set.Where(predicate);

            return result;
        }
        public static IQueryable<T> Search<T>(this IQueryable<T> set, 
            Query query, int pageNumber, int pageSize)
            where T: class
        {
            return set.Search(query)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }


    }
}
