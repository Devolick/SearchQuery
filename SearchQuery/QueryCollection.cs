namespace SearchQuery
{
    public class QueryCollection<T>: List<T>
    {
        public QueryCollection()
        { }
    }

    public class QueryCollection : QueryCollection<object>
    {
        public QueryCollection()
        { }
    }
}
