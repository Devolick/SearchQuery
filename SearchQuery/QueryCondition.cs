namespace SearchQuery
{
    public class QueryCondition
    {
        public string Field { get; set; }
        public QueryOperation Operation { get; set; }
        public QueryCollection Values { get; set; }
        public QueryCase Case { get; set; }
        public bool OrElse { get; set; }
        public ConditionCollection Conditions { get; set; }

        public QueryCondition()
        {
            Operation = QueryOperation.Equal;
            Values = new QueryCollection();
            Case = QueryCase.Default;
            Conditions = new ConditionCollection();
        }
    }
}
