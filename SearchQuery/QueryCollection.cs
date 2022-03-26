namespace SearchQuery
{
    public class QueryCollection<T>: List<T>
    {
        public QueryCollection()
        { }
    }

    public class SearchCollection : QueryCollection<object>
    {
        public SearchCollection()
        { }
    }
}
