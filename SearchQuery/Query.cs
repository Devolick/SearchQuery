namespace SearchQuery
{
    public class Query
    {
        public DateTime? Timestamp { get; set; }
        public DateTime? Bookmark { get; set; }
        public ConditionCollection Conditions { get; set; }
        public bool OrElse { get; set; }

        public Query()
        {
            Conditions = new ConditionCollection();
        }
    }
}
