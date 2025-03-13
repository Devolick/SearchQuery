namespace SearchQuery
{
    /// <summary>
    /// Represents a search query with a specific format.
    /// </summary>
    public class Search : Query
    {
        /// <summary>
        /// Gets or sets the format of the search query.
        /// </summary>
        public Format Format { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Search"/> class with the default format.
        /// </summary>
        public Search() : base()
        { 
            Format = Format.ISODateTime;
        }
    }
}

namespace SearchQuery.NewtonsoftJson
{
    /// <summary>
    /// Represents a JSON serializable search query using Newtonsoft.Json.
    /// </summary>
    [Newtonsoft.Json.JsonConverter(
        typeof(SearchConverter))]
    public class JSearch : Search
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JSearch"/> class.
        /// </summary>
        public JSearch() : base()
        { }

        /// <summary>
        /// Converts the search query to a JSON string.
        /// </summary>
        /// <returns>A JSON string representation of the search query.</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Creates a <see cref="JSearch"/> instance from a JSON string.
        /// </summary>
        /// <param name="json">The JSON string representation of the search query.</param>
        /// <returns>A <see cref="JSearch"/> instance.</returns>
        public static JSearch FromJson(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JSearch>(json);
        }
    }
}

namespace SearchQuery.SystemTextJson
{
    /// <summary>
    /// Represents a JSON serializable search query using System.Text.Json.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(
        typeof(SearchConverter))]
    public class JSearch : Search
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JSearch"/> class.
        /// </summary>
        public JSearch() : base()
        { }

        /// <summary>
        /// Converts the search query to a JSON string.
        /// </summary>
        /// <returns>A JSON string representation of the search query.</returns>
        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Creates a <see cref="JSearch"/> instance from a JSON string.
        /// </summary>
        /// <param name="json">The JSON string representation of the search query.</param>
        /// <returns>A <see cref="JSearch"/> instance.</returns>
        public static JSearch FromJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<JSearch>(json);
        }
    }
}

