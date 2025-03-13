# SearchQuery

SearchQuery is a library that extends LINQ search over a database using Entity Framework. It is not tied to any specific database or storage engine and is instead backed by your existing code and data. Search can be done in two interfaces: `IQueryable` and `IEnumerable`.

> The main task is to make the request between Frontend - Backend more flexible and easier to search data.

## Installation

### .NET

To install the SearchQuery package, run the following command:

```sh
dotnet add package SearchQuery
```

### JavaScript/TypeScript

To install the SearchQuery package, run the following command:

```sh
npm install jsearchquery
```

## Requirements

- Minimal version: .NET 6.0

## Important Notes

- Ensure to follow valid JSON format and type for request condition field type.

## Types

A SearchQuery object type at some point those fields have to resolve to some concrete data.

| SearchQuery JS/TS | SearchQuery .NET | .NET                                                                             |
| ----------------- | ---------------- | -------------------------------------------------------------------------------- |
| string            | string           | string                                                                           |
| Date, string      | string           | DateTime, string                                                                 |
| number            | number           | char, byte, sbyte, short, ushort, int, uint, long, ulong, float, double, decimal |
| boolean           | boolean          | boolean                                                                          |
| null/undefined    | null             | null                                                                             |

## Examples

### JavaScript/TypeScript

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
          incase: Case.Lower,
        },
        {
          field: "fullName",
          operation: Operation.EndsWith,
          values: ["Ritchie"],
          incase: Case.Upper,
        },
      ],
    },
  ],
});
```

### C#

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
                    Incase = Case.Lower
                },
                new Condition
                {
                    Field = nameof(TestEntity.FullName),
                    Operation = Operation.EndsWith,
                    Values = new Values
                    {
                        "Ritchie"
                    },
                    Incase = Case.Upper
                }
            }
        }
    }
};
using (var db = new Database()) {
    var query = db.Entities.Search(searchQuery).ToList();
}
```

## API

### Namespace: `SearchQuery`

#### Class: `Extensions`

##### Search Methods

**Search for IEnumerable**

```csharp
public static IEnumerable<T> Search<T>(this IEnumerable<T> set, Search query) where T : class;
public static IEnumerable<T> Search<T>(this IEnumerable<T> set, Search query, int pageNumber, int pageSize) where T : class;
```

- **Description**: Filters an `IEnumerable` collection based on the provided `Search` query object.
- **Parameters**:
  - `set`: The collection to search.
  - `query`: The search query.
  - `pageNumber`: (Optional) The page number for paginated results.
  - `pageSize`: (Optional) The size of each page.
- **Returns**: Filtered `IEnumerable`.

**Search for IQueryable**

```csharp
public static IQueryable<T> Search<T>(this IQueryable<T> set, Search query) where T : class;
public static IQueryable<T> Search<T>(this IQueryable<T> set, Search query, int pageNumber, int pageSize) where T : class;
```

- **Description**: Filters an `IQueryable` collection based on the provided `Search` query object.
- **Parameters**: Same as above.
- **Returns**: Filtered `IQueryable`.

##### Type Analysis Methods

**`InType`**

```csharp
public static Type? InType(this Type? valueType, bool isCollection = false);
```

- **Description**: Extracts the underlying type, especially for nullable or generic collection types.

**`IsNull`**

```csharp
public static bool IsNull(this Type? valueType);
```

- **Description**: Determines if the given type is null.

**`InNull`**

```csharp
public static Type? InNull(this Type? valueType);
```

- **Description**: Converts a type to a nullable type if applicable.

**`IsType`**

```csharp
public static Types IsType(this Type type, bool isCollection = false);
```

- **Description**: Maps the type to a specific `Types` enum value (e.g., `String`, `Number`, `Boolean`, `Date`, `Null`).

**`IsCollection`**

```csharp
public static bool IsColletion(this Type type);
```

- **Description**: Determines if the type represents a collection.

**Type Checking Methods**

```csharp
public static bool IsBoolean(this Type? valueType, bool isCollection = false);
public static bool IsNumber(this Type? valueType, bool isCollection = false);
public static bool IsDate(this Type? valueType, bool isCollection = false);
public static bool IsString(this Type? valueType, bool isCollection = false);
```

- **Description**: Determines if the type represents a specific data type or a collection.

### Search Class

_Inheritance_ Query Class

| Property | Type   | Default     | Description                                     |
| -------- | ------ | ----------- | ----------------------------------------------- |
| Format   | Format | ISODateTime | ISO Formats, DateOnly Formats, TimeOnly Formats |

### Query Class

_Inheritance_ ISearch Interface

| Property   | Type       | Default          | Description                                 |
| ---------- | ---------- | ---------------- | ------------------------------------------- |
| Operator   | Operator   | And              | And, Or                                     |
| Conditions | Conditions | new Conditions() | Inherit from List with ISearch generic type |

### Condition Class

_Inheritance_ ISearch Interface

| Property  | Type      | Default         | Description                                                                                            |
| --------- | --------- | --------------- | ------------------------------------------------------------------------------------------------------ |
| Field     | string    | ""              | Entity property name                                                                                   |
| Operation | Operation | Operation.Equal | Action operation                                                                                       |
| Values    | Values    | new Values()    | Inherit from List with object generic type                                                             |
| Incase    | Case      | Case.Default    | Transform string for Contains, NotContains, StartsWith, NotStartsWith, EndsWith, NotEndsWith operation |
| Format    | Format    | ISODateTime     | ISO Formats, DateOnly Formats, TimeOnly Formats                                                        |

### Conditions Class

_Inheritance_ Base is generic List<ISearch>

### Namespace: `SearchQuery.NewtonsoftJson`, `SearchQuery.SystemTextJson`

#### Class: `Extensions`

##### JSON Search Methods

**Convert JSON to Search Query**

```csharp
public static JSearch ToSearch(this string json);
```

- **Description**: Converts a JSON string to a `JSearch` object.

**Search for IEnumerable**

```csharp
public static IEnumerable<T> Search<T>(this IEnumerable<T> set, string query) where T : class;
public static IEnumerable<T> Search<T>(this IEnumerable<T> set, string query, int pageNumber, int pageSize) where T : class;
```

- **Description**: Filters an `IEnumerable` collection using a JSON query string.

**Search for IQueryable**

```csharp
public static IQueryable<T> Search<T>(this IQueryable<T> set, string query) where T : class;
public static IQueryable<T> Search<T>(this IQueryable<T> set, string query, int pageNumber, int pageSize) where T : class;
```

- **Description**: Filters an `IQueryable` collection using a JSON query string.

### Class: `JSearch`

#### Attributes

- **JsonConverter**
  ```csharp
  [System.Text.Json.Serialization.JsonConverter(typeof(SearchConverter))]
  ```
  - **Description**: Indicates that the `JSearch` class uses a custom JSON converter for serialization and deserialization.

#### Methods

1. **`ToJson`**

   ```csharp
   public string ToJson();
   ```

   - **Description**: Serializes the `JSearch` object into a JSON string using System.Text.Json.

2. **`FromJson`**
   ```csharp
   public static JSearch FromJson(string json);
   ```
   - **Description**: Deserializes a JSON string into a `JSearch` object.

#### Constructor

- **`JSearch()`**
  ```csharp
  public JSearch() : base()
  ```
  - **Description**: Initializes a new instance of the `JSearch` class.

### Operation Enum

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

### Case Enum

| Key     | Value |
| ------- | ----- |
| Default | 0     |
| Lower   | 1     |
| Upper   | 2     |

### Operator Enum

| Key | Value |
| --- | ----- |
| And | 0     |
| Or  | 1     |

### Types Enum

| Key     | Value |
| ------- | ----- |
| Null    | 0     |
| String  | 1     |
| Number  | 2     |
| Boolean | 3     |
| Date    | 4     |

### Format Enum

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
