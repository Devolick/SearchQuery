namespace SearchQuery
{
    public class Query : ISearch
    {
        public Operator Operator { get; set; }
        public Conditions Conditions { get; set; }
   
        public Query()
        {
            Operator = Operator.And;
            Conditions = new Conditions();
        }
    }
}
