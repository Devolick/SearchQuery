namespace SearchQuery
{
    /// <summary>
    /// Represents a condition in a search query.
    /// </summary>
    public class Condition : ISearch
    {
        /// <summary>
        /// Gets or sets the field for the condition.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the operation for the condition.
        /// </summary>
        public Operation Operation { get; set; }

        /// <summary>
        /// Gets or sets the values for the condition.
        /// </summary>
        public Values Values { get; set; }

        /// <summary>
        /// Gets or sets the case sensitivity for the condition.
        /// </summary>
        public Case Incase { get; set; }

        /// <summary>
        /// Gets or sets the format for the condition.
        /// </summary>
        public Format? Format { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Condition"/> class with default values.
        /// </summary>
        public Condition()
        {
            Operation = Operation.Equal;
            Values = new Values();
            Incase = Case.Default;
        }
    }
}
