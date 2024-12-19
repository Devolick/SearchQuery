namespace SearchQuery
{
    public class Search : Query
    {
        public Format Format { get; set; }

        public Search() : base()
        { 
            Format = Format.ISODateTime;
        }
    }
}

namespace SearchQuery.NewtonsoftJson
{
    [Newtonsoft.Json.JsonConverter(
        typeof(SearchConverter))]
    public class JSearch : Search
    {
        public JSearch() : base()
        { }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static JSearch FromJson(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JSearch>(json);
        }
    }
}

namespace SearchQuery.SystemTextJson
{
    [System.Text.Json.Serialization.JsonConverter(
        typeof(SearchConverter))]
    public class JSearch : Search
    {
        public JSearch() : base()
        { }

        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }

        public static JSearch FromJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<JSearch>(json);
        }
    }
}

