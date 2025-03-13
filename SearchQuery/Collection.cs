namespace SearchQuery
{
    /// <summary>
    /// Represents a collection of values for a search query.
    /// </summary>
    public class Values : List<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Values"/> class.
        /// </summary>
        public Values() : base()
        { }
    }

    /// <summary>
    /// Represents a collection of conditions for a search query.
    /// </summary>
    public class Conditions : List<ISearch>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Conditions"/> class.
        /// </summary>
        public Conditions() : base()
        { }
    }
}
