using System.Linq.Expressions;
using System.Reflection;

namespace SearchQuery
{
    internal sealed class QueryBuilder
    {
        private readonly Query searchQuery;

        public QueryBuilder(Query searchQuery)
        {
            this.searchQuery = searchQuery;
        }

        public Expression<Func<T, bool>> Build<T>()
        {
            var parameter = Mapper.Parameter<T>();
            var conditions = Condition<T>(searchQuery.Conditions, parameter, searchQuery.OrElse);
            var aggregation = searchQuery.OrElse ? Mapper.OrAggregate(conditions) : 
                Mapper.AndAggregate(conditions);

            var predicate = Mapper.Where<T>(aggregation, parameter);
            var expression = (Expression<Func<T, bool>>)predicate;

            return expression;
        }

        public Func<T, bool> Compile<T>()
        {
            var expression = Build<T>();
            var predicate = expression.Compile();

            return predicate;
        }

        private IEnumerable<Mapper> Condition<T>(IEnumerable<QueryCondition> conditions, Mapper left, bool orElse)
        {
            var values = conditions
                .Select(condition =>
                {
                    var property = Mapper.Property(left, condition.Field);
                    var values = Values<T>(condition.Field, condition.Values, out Type valueType)
                        .Select(value =>
                        {
                            if (IsNumber(valueType))
                            {
                                return Number(property, value, valueType, condition.Operation);
                            }
                            else if (IsDateTime(valueType))
                            {
                                return DateTime(property, value, valueType, condition.Operation);
                            }
                            else if (IsBoolean(valueType))
                            {
                                return Boolean(property, value, valueType, condition.Operation);
                            }

                            return String(property, value, valueType, condition.Operation, condition.Case);
                        });
                    if (orElse)
                    {
                        return Mapper.OrAggregate(values);
                    }
                    else
                    {
                        return Mapper.AndAggregate(values);
                    }
                });

            return values;
        }

        private IEnumerable<Mapper> Values<T>(string field, IEnumerable<object> values, out Type valueType)
        {
            var propertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            var propertyType = typeof(T).GetProperty(field, propertyFlags).PropertyType;
            valueType = propertyType;
            var convertedType = Activator.CreateInstance(typeof(List<>)
                .MakeGenericType(propertyType)) as Type;

            return values.Select(value => Mapper.Constant(value, propertyType));
        }

        private bool IsBoolean(Type? valueType)
        {
            if (Nullable.GetUnderlyingType(valueType) != null)
            {
                valueType = Nullable.GetUnderlyingType(valueType);
                if (valueType == null)
                {
                    return false;
                }
            }

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Boolean:
                    return true;
            }

            return false;

        }
        private Mapper Boolean(Mapper left, Mapper right, Type valueType,
            SearchOperation operation)
        {
            switch (operation)
            {
                case SearchOperation.NotEqual:
                case SearchOperation.NotBetween:
                case SearchOperation.NotContains:
                case SearchOperation.NotStartsWith:
                case SearchOperation.NotEndsWith:           
                    return left.NotEqual(right);
                case SearchOperation.Equal:
                case SearchOperation.LessThan:
                case SearchOperation.LessThanOrEqual:
                case SearchOperation.GreaterThan:
                case SearchOperation.GreaterThanOrEqual:
                case SearchOperation.Between:
                case SearchOperation.Contains:
                case SearchOperation.StartsWith:
                case SearchOperation.EndsWith:
                    return left.Equal(right);

                default: throw new ArgumentException("Uknown boolean operation");
            }

        }

        private bool IsNumber(Type? valueType)
        {
            if (Nullable.GetUnderlyingType(valueType) != null)
            {
                valueType = Nullable.GetUnderlyingType(valueType);
                if (valueType == null)
                {
                    return false;
                }
            }

            switch (Type.GetTypeCode(valueType))
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
        private Mapper Number(Mapper left, Mapper right, Type valueType,
            SearchOperation operation)
        {
            switch (operation)
            {
                case SearchOperation.LessThan:
                    return left.LessThan(right);
                case SearchOperation.LessThanOrEqual:
                    return left.LessThanOrEqual(right);
                case SearchOperation.GreaterThan:
                    return left.GreaterThan(right);
                case SearchOperation.GreaterThanOrEqual:
                    return left.GreaterThanOrEqual(right);
                case SearchOperation.Between:
                    return left.Between(right);
                case SearchOperation.NotBetween:
                    return left.NotBetween(right);
                case SearchOperation.Contains:
                    return left.AsString().Contains(right.AsString());
                case SearchOperation.NotContains:
                    return left.AsString().NotContains(right.AsString());
                case SearchOperation.StartsWith:
                    return left.AsString().StartsWith(right.AsString());
                case SearchOperation.NotStartsWith:
                    return left.AsString().NotStartsWith(right.AsString());
                case SearchOperation.EndsWith:
                    return left.AsString().EndsWith(right.AsString());
                case SearchOperation.NotEndsWith:
                    return left.AsString().NotEndsWith(right.AsString());
                case SearchOperation.NotEqual:
                    return left.NotEqual(right);
                case SearchOperation.Equal:
                    return left.Equal(right);

                default: throw new ArgumentException("Uknown number operation");
            }
        }

        private bool IsDateTime(Type? valueType)
        {
            if (Nullable.GetUnderlyingType(valueType) != null)
            {
                valueType = Nullable.GetUnderlyingType(valueType);
                if (valueType == null)
                {
                    return false;
                }
            }

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.DateTime:
                    return true;
            }

            return false;
        }
        private Mapper DateTime(Mapper left, Mapper right, Type valueType,
            SearchOperation operation)
        {
            switch (operation)
            {
                case SearchOperation.LessThan:
                    return left.LessThan(right);
                case SearchOperation.LessThanOrEqual:
                    return left.LessThanOrEqual(right);
                case SearchOperation.GreaterThan:
                    return left.GreaterThan(right);
                case SearchOperation.GreaterThanOrEqual:
                    return left.GreaterThanOrEqual(right);
                case SearchOperation.Between:
                    return left.Between(right);
                case SearchOperation.NotBetween:
                    return left.NotBetween(right);
                case SearchOperation.Contains:
                    return left.AsString().Contains(right.AsString());
                case SearchOperation.NotContains:
                    return left.AsString().NotContains(right.AsString());
                case SearchOperation.StartsWith:
                    return left.AsString().StartsWith(right.AsString());
                case SearchOperation.NotStartsWith:
                    return left.AsString().NotStartsWith(right.AsString());
                case SearchOperation.EndsWith:
                    return left.AsString().EndsWith(right.AsString());
                case SearchOperation.NotEndsWith:
                    return left.AsString().NotEndsWith(right.AsString());
                case SearchOperation.NotEqual:
                    return left.NotEqual(right);
                case SearchOperation.Equal:
                    return left.Equal(right);

                default: throw new ArgumentException("Uknown datetime operation");
            }
        }

        private bool IsString(Type? valueType)
        {
            if (Nullable.GetUnderlyingType(valueType) != null)
            {
                valueType = Nullable.GetUnderlyingType(valueType);
                if (valueType == null)
                {
                    return false;
                }
            }

            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.String:
                    return true;
            }

            return false;
        }
        private Mapper String(Mapper left, Mapper right, Type valueType,
            SearchOperation operation, SearchCase searchCase)
        {
            var _default = Mapper.Constant(string.Empty, valueType);  
            
            if (!IsString(valueType))
            {
                var _defaultNull = Mapper.Constant(null, valueType);
                left = Mapper.WhenNull(left, Mapper.AsString(left), _defaultNull);
                right = Mapper.WhenNull(right, Mapper.AsString(right), _defaultNull);
            }

            switch (operation)
            {
                case SearchOperation.LessThan:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .LessThan(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .LessThan(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.CompareTo(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .LessThan(Mapper.Constant(0, typeof(int)));
                case SearchOperation.LessThanOrEqual:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .LessThanOrEqual(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .LessThanOrEqual(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.CompareTo(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .LessThanOrEqual(Mapper.Constant(0, typeof(int)));
                case SearchOperation.GreaterThan:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default), 
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .GreaterThan(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .GreaterThan(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.CompareTo(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .GreaterThan(Mapper.Constant(0, typeof(int)));
                case SearchOperation.GreaterThanOrEqual:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .GreaterThanOrEqual(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .GreaterThanOrEqual(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.CompareTo(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .GreaterThanOrEqual(Mapper.Constant(0, typeof(int)));
                case SearchOperation.Between:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .Between(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .Between(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.Between(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                case SearchOperation.NotBetween:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .NotBetween(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .NotBetween(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.NotBetween(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                case SearchOperation.Contains:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.Contains(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.Contains(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.Contains(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case SearchOperation.NotContains:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.NotContains(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.NotContains(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.NotContains(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case SearchOperation.StartsWith:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.StartsWith(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.StartsWith(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.StartsWith(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case SearchOperation.NotStartsWith:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.NotStartsWith(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.NotStartsWith(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.NotStartsWith(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case SearchOperation.EndsWith:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.EndsWith(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.EndsWith(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.EndsWith(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case SearchOperation.NotEndsWith:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.NotEndsWith(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.NotEndsWith(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.NotEndsWith(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case SearchOperation.Equal:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.Equal(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.Equal(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.Equal(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                case SearchOperation.NotEqual:
                    if (searchCase == SearchCase.Lower)
                    {
                        return Mapper.NotEqual(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == SearchCase.Upper)
                    {
                        return Mapper.NotEqual(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.NotEqual(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));

                default: throw new ArgumentException("Uknown string operation");
            }
        }
    }
}
