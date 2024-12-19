namespace SearchQuery
{
    public class Condition : ISearch
    {
        public string Field { get; set; }
        public Operation Operation { get; set; }
        public Values Values { get; set; }
        public Case Incase { get; set; }
        public Format? Format { get; set; }

        public Condition()
        {
            Operation = Operation.Equal;
            Values = new Values();
            Incase = Case.Default;
        }
    }
}
