namespace SearchQuery
{
    public class QueryCondition
    {
        public string Field { get; set; }
        public SearchOperation Operation { get; set; }
        public SearchCollection Values { get; set; }
        public SearchCase Case { get; set; }

        public QueryCondition()
        {
            Operation = SearchOperation.Equal;
            Values = new SearchCollection();
            Case = SearchCase.Default;
        }
    }
}
