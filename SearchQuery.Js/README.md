# SearchQuery

SearchQuery is a library that extends LINQ search over a database using Entity Framework.
SearchQuery isn't tied to any specific database or storage engine and is instead backed by your existing code and data. Search can be done in two interfaces IQueryable, IEnumerable.

> The main task is to make the request between Frontend - Backend more flexible and easier to search data.

**Info**
Minimal version is .NET 6.0

> npm install jsearchquery

> dotnet add package SearchQuery

**Warning**
Important to follow valid json format and type for request condition field type.

## Types

A SearchQuery object type at some point those fields have to resolve to some concrete data.

| SearchQuery JS/TS | SearchQuery .NET | .NET                                                                             |
| ----------------- | ---------------- | -------------------------------------------------------------------------------- |
| string            | string           | string                                                                           |
| Date              | string           | DateTime, DateOnly, TimeOnly                                                     |
| number            | number           | char, byte, sbyte, short, ushort, int, uint, long, ulong, float, double, decimal |
| boolean           | boolean          | boolean                                                                          |
| null/undefined    | null             | null                                                                             |

## Examples

JavaScript/TypeScript

```ts
const search = new SearchQuery({
  operator: Operator.And,
  conditions: [
    {
      field: "fullName",
      operation: Operation.Equal,
      values: ["Dennis Ritchie"],
    },
    {
      field: "created",
      // Only two values for Between, NotBetween
      operation: Operation.Between,
      values: ["1990-12-31", "2025-12-31"],
    },
    {
      operator: Operator.Or,
      conditions: [
        {
          field: "fullName",
          operation: Operation.StartsWith,
          values: ["Dennis"],
          case: Case.Lower,
        },
        {
          field: "fullName",
          operation: Operation.EndsWith,
          values: ["Ritchie"],
          case: Case.Upper,
        },
      ],
    },
  ],
});
```

CSharp

```csharp
var searchQuery = new Search
{
    Operator = Operator.And,
    Conditions = new Conditions {
        new Condition
        {
            Field = nameof(TestEntity.FullName),
            Operation = Operation.Equal,
            Values = new Values
            {
                "Dennis Ritchie"
            }
        },
        new Condition
        {
            Field = nameof(TestEntity.Created),
            // Only two values for Between, NotBetween
            Operation = Operation.Between,
            Values = new Values
            {
                "1990-12-31", "2025-12-31"
            }
        },
        new Query {
            Operator = Operator.Or,
            Conditions = new Conditions {
                new Condition
                {
                    Field = nameof(TestEntity.FullName),
                    Operation = Operation.StartsWith,
                    Values = new Values
                    {
                        "Dennis"
                    },
                    Case = Case.Lower
                },
                new Condition
                {
                    Field = nameof(TestEntity.FullName),
                    Operation = Operation.EndsWith,
                    Values = new Values
                    {
                        "Ritchie"
                    },
                    Case = Case.Upper
                }
            }
        }
    }
};
using (var db = new Database()) {
    var query = db.Entities.Search(searchQuery).ToList();
}
```

# API

**Search Class**

_Inheritance_ Query Class

_Methods_

```csharp
public string ToJson();

public static Search FromJson(string json);
```

**Query Class**

_Inheritance_ ISearch Interface

| Property   | Type       | Default          | Description                                     |
| ---------- | ---------- | ---------------- | ----------------------------------------------- |
| Operator   | Operator   | And              | And, Or                                         |
| Conditions | Conditions | new Conditions() | Inherit from List with ISearch generic type     |
| Format     | Format     | ISODateTime      | ISO Formats, DateOnly Formats, TimeOnly Formats |

**Condition Class**

_Inheritance_ ISearch Interface

| Property  | Type      | Default         | Description                                                                                            |
| --------- | --------- | --------------- | ------------------------------------------------------------------------------------------------------ |
| Field     | string    | ""              | Entity property name                                                                                   |
| Operation | Operation | Operation.Equal | Action operation                                                                                       |
| Values    | Values    | new Values()    | Inherit from List with object generic type                                                             |
| Case      | Case      | Case.Default    | Transform string for Contains, NotContains, StartsWith, NotStartsWith, EndsWith, NotEndsWith operation |
| Format    | Format    | ISODateTime     | ISO Formats, DateOnly Formats, TimeOnly Formats                                                        |

**Conditions Class**

_Inheritance_ Base is generic list

**Operation Enum**

| Key                | Value | String  | Date      | Number    | Boolean   | Null    |
| ------------------ | ----- | ------- | --------- | --------- | --------- | ------- |
| Equal              | 0     | By Type | By Type   | By Type   | By Type   | By Type |
| NotEqual           | 1     | By Type | By Type   | By Type   | By Type   | By Type |
| LessThan           | 2     | Compare | By Type   | By Type   | Compare   | Ignore  |
| LessThanOrEqual    | 3     | Compare | By Type   | By Type   | Compare   | Ignore  |
| GreaterThan        | 4     | Compare | By Type   | By Type   | Compare   | Ignore  |
| GreaterThanOrEqual | 5     | Compare | By Type   | By Type   | Compare   | Ignore  |
| Contains           | 6     | By Type | As String | As String | As String | Ignore  |
| NotContains        | 7     | By Type | As String | As String | As String | Ignore  |
| StartsWith         | 8     | By Type | As String | As String | As String | Ignore  |
| NotStartsWith      | 9     | By Type | As String | As String | As String | Ignore  |
| EndsWith           | 10    | By Type | As String | As String | As String | Ignore  |
| NotEndsWith        | 11    | By Type | As String | As String | As String | Ignore  |
| Between            | 12    | Compare | Compare   | By Type   | Compare   | Ignore  |
| NotBetween         | 13    | Compare | Compare   | By Type   | Compare   | Ignore  |

**Case Enum**

| Key     | Value |
| ------- | ----- |
| Default | 0     |
| Lower   | 1     |
| Upper   | 2     |

**Operator Enum**

| Key | Value |
| --- | ----- |
| And | 0     |
| Or  | 1     |

**Types Enum**

| Key     | Value |
| ------- | ----- |
| Null    | 0     |
| String  | 1     |
| Number  | 2     |
| Boolean | 3     |
| Date    | 4     |

**Format Enum**

| Key                            | Value | Format                   |
| ------------------------------ | ----- | ------------------------ |
| DateOnly                       | 1     | yyyy-MM-dd               |
| TimeOnly                       | 2     | HH:mm:ss                 |
| ISODateTime                    | 3     | yyyy-MM-ddTHH:mm:ss      |
| ISODateTimeWithMilliseconds    | 4     | yyyy-MM-ddTHH:mm:ss.fff  |
| ISODateTimeWithOffset          | 5     | yyyy-MM-ddTHH:mm:sszzz   |
| ISODateTimeUTC                 | 6     | yyyy-MM-ddTHH:mm:ssZ     |
| ISODateTimeWithMillisecondsUTC | 7     | yyyy-MM-ddTHH:mm:ss.fffZ |
| DateDay                        | 11    | dd                       |
| DateMonth                      | 12    | MM                       |
| DateYear                       | 13    | yyyy                     |
| TimeOn                         | 21    | HH:mm                    |
| TimeHours                      | 22    | HH                       |
| TimeMinutes                    | 23    | mm                       |
| TimeSeconds                    | 24    | ss                       |
| TimeMilliseconds               | 25    | fff                      |
| TimeFull                       | 26    | HH:mm:ss.fff             |
