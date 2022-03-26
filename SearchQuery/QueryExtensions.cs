namespace SearchQuery
{
    public static class QueryExtensions
    {
        public static IEnumerable<T> Search<T>(this IEnumerable<T> entity,
            Query query)
            where T : class
        {
            return query.ToQuery(entity);
        }
        public static IEnumerable<T> Search<T>(this IEnumerable<T> entity,
            Query query, int pageNumber, int pageSize)
            where T : class
        {
            return entity.Search(query)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> entity,
            Query query)
            where T : class
        {
            return query.ToQuery(entity);
        }
        public static IQueryable<T> Search<T>(this IQueryable<T> entity, 
            Query query, int pageNumber, int pageSize)
            where T: class
        {
            return entity.Search(query)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }


    }
}
