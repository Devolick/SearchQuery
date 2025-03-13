using System.Collections;
using System.Linq.Expressions;
using SearchQuery.Builder;

namespace SearchQuery
{
    /// <summary>
    /// Provides extension methods for searching collections and queryable sets.
    /// </summary>
    public static class Extensions
    {
        public static Func<T, bool> Compile<T>(this Search? query) {
            var searchBuilder = new SearchBuilder(query ?? new Search());
            var predicate = searchBuilder.Compile<T>();

            return predicate;
        }

        /// <summary>
        /// Searches the given enumerable set based on the specified search query.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The enumerable set to search.</param>
        /// <param name="query">The search query.</param>
        /// <returns>An enumerable set of elements that match the search query.</returns>
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            Search? query)
            where T : class
        {
            var predicate = Compile<T>(query);
            var result = set.Where(predicate);

            return result;
        }

        /// <summary>
        /// Searches the given enumerable set based on the specified search query with pagination.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The enumerable set to search.</param>
        /// <param name="query">The search query.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>An enumerable set of elements that match the search query with pagination.</returns>
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            Search? query, int pageNumber, int pageSize)
            where T : class
        {
            return set.Search(query ?? new Search())
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static Type? InType(this Type? valueType, bool isCollection = false)
        {
            if (isCollection && valueType != null && valueType.IsGenericType && 
                typeof(IEnumerable<>).IsAssignableFrom(valueType.GetGenericTypeDefinition())) {
                return valueType.GetGenericArguments()[0];   
            }

            return Nullable.GetUnderlyingType(valueType) ?? valueType;                
        }

        public static bool IsNull(this Type? valueType)
        {
            return valueType == null;
        }
        public static Type? InNull(this Type? valueType)
        {
            valueType = InType(valueType) ?? valueType;
            if (valueType != null && valueType.IsValueType)
            {
                return typeof(Nullable<>).MakeGenericType(valueType);
            } 

            return valueType;                
        }

        public static bool IsBoolean(this Type? valueType, bool isCollection = false)
        {
            switch (Type.GetTypeCode(InType(valueType, isCollection)))
            {
                case TypeCode.Boolean:
                    return true;
            }

            return false;

        }
        public static bool IsBoolean(this Expression left, bool isCollection = false) {
            return IsBoolean(left.Type, isCollection);
        }
        public static bool IsBoolean(this object left, bool isCollection = false) {
            return IsBoolean(left.GetType(), isCollection);
        }

        public static bool IsEnum(this Type? valueType, bool isCollection = false)
        {
            var type = InType(valueType, isCollection);

            if (type == null)
                return false;

            type = Nullable.GetUnderlyingType(type) ?? type;
            if ((type.IsEnum || (type?.IsEnum ?? false)))
                return true;

            return false;
        }
        public static bool IsEnum(this Expression left, bool isCollection = false) {
            return IsEnum(left.Type, isCollection);
        }
        public static bool IsEnum(this object left, bool isCollection = false) {
            return IsEnum(left.GetType(), isCollection);
        }

        public static bool IsNumber(this Type? valueType, bool isCollection = false)
        {
            switch (Type.GetTypeCode(InType(valueType, isCollection)))
            {
                case TypeCode.Char:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return true;
            }

            return false;
        }
        public static bool IsNumber(this Expression left, bool isCollection = false) {
            return IsNumber(left.Type, isCollection);
        }
        public static bool IsNumber(this object left, bool isCollection = false) {
            return IsNumber(left.GetType(), isCollection);
        }

        public static bool IsDate(this Type? valueType, bool isCollection = false)
        {
            var type = InType(valueType, isCollection);

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime:
                    return true;
            }

            return false;
        }
        public static bool IsDate(this Expression left, bool isCollection = false) {
            return IsDate(left.Type, isCollection);
        }
        public static bool IsDate(this object left, bool isCollection = false) {
            return IsDate(left.GetType(), isCollection);
        }

        public static bool IsString(this Type? valueType, bool isCollection = false)
        {
            switch (Type.GetTypeCode(InType(valueType, isCollection)))
            {
                case TypeCode.String:
                    return true;
            }

            return false;
        }
        public static bool IsString(this Expression left, bool isCollection = false) {
            return IsString(left.Type, isCollection);
        }
        public static bool IsString(this object left, bool isCollection = false) {
            return IsString(left.GetType(), isCollection);
        }

        public static bool IsColletion(this Type type) {
            if (type != null && type.IsGenericType && type.GetInterfaces()
                .Contains(typeof(IEnumerable))) {
                return true; 
            }

            return false;
        }

        public static Types IsType(this Type type, bool isCollection = false) {
            if (IsString(type, isCollection)) {
                return Types.String;
            } else if (IsNumber(type, isCollection)) {
                return Types.Number;
            } else if (IsBoolean(type, isCollection)) {
                return Types.Boolean;
            } else if (IsDate(type, isCollection)) {
                return Types.Date;
            } else if (IsEnum(type, isCollection)) {
                return Types.Enum;
            } else if (IsNull(type)) {
                return Types.Null;
            }

            throw new Exception("Invalid query type");
        }

    
    }
}

namespace SearchQuery.EntityFrameworkCore
{
    /// <summary>
    /// Provides extension methods for searching collections and queryable sets.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Searches the given queryable set based on the specified search query.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The queryable set to search.</param>
        /// <param name="query">The search query.</param>
        /// <returns>A queryable set of elements that match the search query.</returns>
        public static IQueryable<T> Search<T>(this IQueryable<T> set,
            Search? query)
            where T : class
        {
            var searchBuilder = new SearchBuilder(query ?? new Search());
            var predicate = searchBuilder.Build<T>();
            var result = set.Where(predicate);

            return result;
        }

        /// <summary>
        /// Searches the given queryable set based on the specified search query with pagination.
        /// </summary>
        /// <typeparam name="T">The type of elements in the set.</typeparam>
        /// <param name="set">The queryable set to search.</param>
        /// <param name="query">The search query.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>A queryable set of elements that match the search query with pagination.</returns>
        public static IQueryable<T> Search<T>(this IQueryable<T> set, 
            Search? query, int pageNumber, int pageSize)
            where T: class
        {
            return set.Search(query ?? new Search())
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

    }
}


namespace SearchQuery.NewtonsoftJson
{
    /// <summary>
    /// Provides extension methods for searching collections and queryable sets using Newtonsoft.Json.
    /// </summary>
    public static class Extensions
    {
        public static JSearch ToSearch(this string json) {
            return JSearch.FromJson(json);
        }
    }
}

namespace SearchQuery.SystemTextJson
{
    /// <summary>
    /// Provides extension methods for searching collections and queryable sets using System.Text.Json.
    /// </summary>
    public static class Extensions
    {
        public static JSearch ToSearch(this string json) {
            return JSearch.FromJson(json);
        }
    }
}
