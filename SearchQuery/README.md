# SearchQuery

SearchQuery is a library that extends LINQ-based search capabilities over a database using Entity Framework. It is designed to be database-agnostic and integrates seamlessly with your existing code and data. The library supports flexible and efficient search operations through `IQueryable` and `IEnumerable` interfaces.

## Key Features

- **Flexible Search**: Supports complex search queries with logical operators (`And`, `Or`) and multiple conditions.
- **Database Agnostic**: Works with any database supported by Entity Framework.
- **JSON Integration**: Allows search queries to be defined in JSON format for easier frontend-backend communication.
- **Pagination**: Built-in support for paginated results.
- **Type Safety**: Handles various data types like strings, numbers, dates, and booleans.

---

## Installation

### .NET

Install the SearchQuery package via NuGet:

```sh
dotnet add package SearchQuery
```

### JavaScript/TypeScript

Install the SearchQuery package via npm:

```sh
npm install jsearchquery
```

---

## Requirements

- **.NET**: Minimum version required is .NET 6.0.

---

## Usage Examples

### JavaScript/TypeScript

```typescript
import { SearchQuery, Operator, Operation, Case } from "jsearchquery";

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
using SearchQuery;

var searchQuery = new Search
{
    Operator = Operator.And,
    Conditions = new Conditions {
        new Condition
        {
            Field = nameof(TestEntity.FullName),
            Operation = Operation.Equal,
            Values = new Values { "Dennis Ritchie" }
        },
        new Condition
        {
            Field = nameof(TestEntity.Created),
            Operation = Operation.Between,
            Values = new Values { "1990-12-31", "2025-12-31" }
        },
        new Query {
            Operator = Operator.Or,
            Conditions = new Conditions {
                new Condition
                {
                    Field = nameof(TestEntity.FullName),
                    Operation = Operation.StartsWith,
                    Values = new Values { "Dennis" },
                    Incase = Case.Lower
                },
                new Condition
                {
                    Field = nameof(TestEntity.FullName),
                    Operation = Operation.EndsWith,
                    Values = new Values { "Ritchie" },
                    Incase = Case.Upper
                }
            }
        }
    }
};

using (var db = new Database())
{
    var results = db.Entities.Search(searchQuery).ToList();
}
```

---

## API Overview

### Namespace: `SearchQuery`

#### Search Methods

**For `IEnumerable`**

```csharp
public static IEnumerable<T> Search<T>(this IEnumerable<T> set, Search query) where T : class;
public static IEnumerable<T> Search<T>(this IEnumerable<T> set, Search query, int pageNumber, int pageSize) where T : class;
```

**For `IQueryable`**

```csharp
public static IQueryable<T> Search<T>(this IQueryable<T> set, Search query) where T : class;
public static IQueryable<T> Search<T>(this IQueryable<T> set, Search query, int pageNumber, int pageSize) where T : class;
```

#### JSON Search Methods

**Convert JSON to Search Query**

```csharp
public static JSearch ToSearch(this string json);
```

**Search Using JSON**

```csharp
public static IEnumerable<T> Search<T>(this IEnumerable<T> set, string query) where T : class;
public static IQueryable<T> Search<T>(this IQueryable<T> set, string query) where T : class;
```

---

## Data Types Mapping

| JavaScript/TypeScript | .NET Type       | Supported .NET Types                                                             |
| --------------------- | --------------- | -------------------------------------------------------------------------------- |
| `string`              | `string`        | `string`                                                                         |
| `Date`, `string`      | `string`        | `DateTime`, `string`                                                             |
| `number`              | `number`        | `char`, `byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `float`, `double`, `decimal` |
| `boolean`             | `boolean`       | `bool`                                                                          |
| `null`/`undefined`    | `null`          | `null`                                                                          |

---

## Enum Definitions

### `Operation`

Defines the operations that can be applied to fields.

| Operation            | Description                     |
| -------------------- | ------------------------------- |
| `Equal`              | Checks for equality.            |
| `NotEqual`           | Checks for inequality.          |
| `LessThan`           | Checks if less than.            |
| `GreaterThan`        | Checks if greater than.         |
| `Contains`           | Checks if contains a value.     |
| `Between`            | Checks if within a range.       |

### `Operator`

Defines logical operators for combining conditions.

| Operator | Description |
| -------- | ----------- |
| `And`    | Logical AND |
| `Or`     | Logical OR  |

### `Case`

Defines case sensitivity for string operations.

| Case     | Description          |
| -------- | -------------------- |
| `Default`| Case-insensitive.    |
| `Lower`  | Converts to lowercase. |
| `Upper`  | Converts to uppercase. |

---

## Notes

- Ensure JSON queries follow valid JSON format.
- Use appropriate data types for fields to avoid runtime errors.
- Pagination parameters (`pageNumber`, `pageSize`) are optional.

---

## License

This project is licensed under the MIT License.
