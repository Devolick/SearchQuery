using System.Collections.Generic;

namespace SearchQuery.Tests 
{
    public class TaskObject {
        public  List<string> Tags { get; set; }
    }
}

namespace SearchQuery.NewtonsoftJson.Tests
{
    public class PagingParams {
        public JSearch Search { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}

namespace SearchQuery.SystemTextJson.Tests
{
    public class PagingParams {
        public JSearch Search { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}