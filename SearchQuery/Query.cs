namespace SearchQuery
{
    /// <summary>
    /// Represents a base search query with an operator and conditions.
    /// </summary>
    public class Query : ISearch
    {
        /// <summary>
        /// Gets or sets the operator for the search query.
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// Gets or sets the conditions for the search query.
        /// </summary>
        public Conditions Conditions { get; set; }
   
        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class with default values.
        /// </summary>
        public Query()
        {
            Operator = Operator.And;
            Conditions = new Conditions();
        }
    }
}
