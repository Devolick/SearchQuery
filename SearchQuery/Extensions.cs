using System.Collections;
using System.Linq.Expressions;
using SearchQuery.Builder;

namespace SearchQuery
{
    public static class Extensions
    {
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            Search query)
            where T : class
        {
            var searchBuilder = new SearchBuilder(query);
            var predicate = searchBuilder.Compile<T>();
            var result = set.Where(predicate);

            return result;
        }
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            Search query, int pageNumber, int pageSize)
            where T : class
        {
            return set.Search<T>(query)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> set,
            Search query)
            where T : class
        {
            var searchBuilder = new SearchBuilder(query);
            var predicate = searchBuilder.Build<T>();
            var result = set.Where(predicate);

            return result;
        }
        public static IQueryable<T> Search<T>(this IQueryable<T> set, 
            Search query, int pageNumber, int pageSize)
            where T: class
        {
            return set.Search(query)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static Type? InType(this Type? valueType, bool isColletion = false)
        {
            if (isColletion && valueType != null && valueType.IsGenericType && 
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

        public static bool IsBoolean(this Type? valueType, bool isColletion = false)
        {
            switch (Type.GetTypeCode(InType(valueType, isColletion)))
            {
                case TypeCode.Boolean:
                    return true;
            }

            return false;

        }
        public static bool IsBoolean(this Expression left, bool isColletion = false) {
            return IsBoolean(left.Type, isColletion);
        }
        public static bool IsBoolean(this object left, bool isColletion = false) {
            return IsBoolean(left.GetType(), isColletion);
        }

        public static bool IsNumber(this Type? valueType, bool isColletion = false)
        {
            switch (Type.GetTypeCode(InType(valueType, isColletion)))
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
        public static bool IsNumber(this Expression left, bool isColletion = false) {
            return IsNumber(left.Type, isColletion);
        }
        public static bool IsNumber(this object left, bool isColletion = false) {
            return IsNumber(left.GetType(), isColletion);
        }

        public static bool IsDate(this Type? valueType, bool isColletion = false)
        {
            var type = InType(valueType, isColletion);

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime:
                    return true;
            }

            return false;
        }
        public static bool IsDate(this Expression left, bool isColletion = false) {
            return IsDate(left.Type, isColletion);
        }
        public static bool IsDate(this object left, bool isColletion = false) {
            return IsDate(left.GetType(), isColletion);
        }

        public static bool IsString(this Type? valueType, bool isColletion = false)
        {
            switch (Type.GetTypeCode(InType(valueType, isColletion)))
            {
                case TypeCode.String:
                    return true;
            }

            return false;
        }
        public static bool IsString(this Expression left, bool isColletion = false) {
            return IsString(left.Type, isColletion);
        }
        public static bool IsString(this object left, bool isColletion = false) {
            return IsString(left.GetType(), isColletion);
        }

        public static bool IsColletion(this Type type) {
            if (type != null && type.IsGenericType && type.GetInterfaces()
                .Contains(typeof(IEnumerable))) {
                return true; 
            }

            return false;
        }

        public static Types IsType(this Type type, bool isColletion = false) {
            if (IsString(type, isColletion)) {
                return Types.String;
            } else if (IsNumber(type, isColletion)) {
                return Types.Number;
            } else if (IsBoolean(type, isColletion)) {
                return Types.Boolean;
            } else if (IsDate(type, isColletion)) {
                return Types.Date;
            } else if (IsNull(type)) {
                return Types.Null;
            }

            throw new Exception("Invalid query type");
        }

    
    }
}

namespace SearchQuery.NewtonsoftJson {
    public static class Extensions
    {
        public static JSearch ToSearch(this string json) {
            return JSearch.FromJson(json);
        }

        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            string query)
            where T : class
        {
            var searchBuilder = new SearchBuilder(query.ToSearch());
            var predicate = searchBuilder.Compile<T>();
            var result = set.Where(predicate);

            return result;
        }
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            string query, int pageNumber, int pageSize)
            where T : class
        {
            return set.Search<T>(query.ToSearch())
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> set,
            string query)
            where T : class
        {
            var searchBuilder = new SearchBuilder(query.ToSearch());
            var predicate = searchBuilder.Build<T>();
            var result = set.Where(predicate);

            return result;
        }
        public static IQueryable<T> Search<T>(this IQueryable<T> set, 
            string query, int pageNumber, int pageSize)
            where T: class
        {
            return set.Search(query.ToSearch())
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }
    }
}

namespace SearchQuery.SystemTextJson {
    public static class Extensions
    {
        public static JSearch ToSearch(this string json) {
            return JSearch.FromJson(json);
        }
    
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            string query)
            where T : class
        {
            var searchBuilder = new SearchBuilder(query.ToSearch());
            var predicate = searchBuilder.Compile<T>();
            var result = set.Where(predicate);

            return result;
        }
        public static IEnumerable<T> Search<T>(this IEnumerable<T> set,
            string query, int pageNumber, int pageSize)
            where T : class
        {
            return set.Search<T>(query.ToSearch())
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> set,
            string query)
            where T : class
        {
            var searchBuilder = new SearchBuilder(query.ToSearch());
            var predicate = searchBuilder.Build<T>();
            var result = set.Where(predicate);

            return result;
        }
        public static IQueryable<T> Search<T>(this IQueryable<T> set, 
            string query, int pageNumber, int pageSize)
            where T: class
        {
            return set.Search(query.ToSearch())
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }
    }
}
