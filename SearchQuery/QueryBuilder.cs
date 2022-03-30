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
                    if (condition.Operation == QueryOperation.Sub)
                    {
                        return null;
                    } else
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
            QueryOperation operation)
        {
            switch (operation)
            {
                case QueryOperation.NotEqual:
                case QueryOperation.NotBetween:
                case QueryOperation.NotContains:
                case QueryOperation.NotStartsWith:
                case QueryOperation.NotEndsWith:           
                    return left.NotEqual(right);
                case QueryOperation.Equal:
                case QueryOperation.LessThan:
                case QueryOperation.LessThanOrEqual:
                case QueryOperation.GreaterThan:
                case QueryOperation.GreaterThanOrEqual:
                case QueryOperation.Between:
                case QueryOperation.Contains:
                case QueryOperation.StartsWith:
                case QueryOperation.EndsWith:
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
            QueryOperation operation)
        {
            switch (operation)
            {
                case QueryOperation.LessThan:
                    return left.LessThan(right);
                case QueryOperation.LessThanOrEqual:
                    return left.LessThanOrEqual(right);
                case QueryOperation.GreaterThan:
                    return left.GreaterThan(right);
                case QueryOperation.GreaterThanOrEqual:
                    return left.GreaterThanOrEqual(right);
                case QueryOperation.Between:
                    return left.Between(right);
                case QueryOperation.NotBetween:
                    return left.NotBetween(right);
                case QueryOperation.Contains:
                    return left.AsString().Contains(right.AsString());
                case QueryOperation.NotContains:
                    return left.AsString().NotContains(right.AsString());
                case QueryOperation.StartsWith:
                    return left.AsString().StartsWith(right.AsString());
                case QueryOperation.NotStartsWith:
                    return left.AsString().NotStartsWith(right.AsString());
                case QueryOperation.EndsWith:
                    return left.AsString().EndsWith(right.AsString());
                case QueryOperation.NotEndsWith:
                    return left.AsString().NotEndsWith(right.AsString());
                case QueryOperation.NotEqual:
                    return left.NotEqual(right);
                case QueryOperation.Equal:
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
            QueryOperation operation)
        {
            switch (operation)
            {
                case QueryOperation.LessThan:
                    return left.LessThan(right);
                case QueryOperation.LessThanOrEqual:
                    return left.LessThanOrEqual(right);
                case QueryOperation.GreaterThan:
                    return left.GreaterThan(right);
                case QueryOperation.GreaterThanOrEqual:
                    return left.GreaterThanOrEqual(right);
                case QueryOperation.Between:
                    return left.Between(right);
                case QueryOperation.NotBetween:
                    return left.NotBetween(right);
                case QueryOperation.Contains:
                    return left.AsString().Contains(right.AsString());
                case QueryOperation.NotContains:
                    return left.AsString().NotContains(right.AsString());
                case QueryOperation.StartsWith:
                    return left.AsString().StartsWith(right.AsString());
                case QueryOperation.NotStartsWith:
                    return left.AsString().NotStartsWith(right.AsString());
                case QueryOperation.EndsWith:
                    return left.AsString().EndsWith(right.AsString());
                case QueryOperation.NotEndsWith:
                    return left.AsString().NotEndsWith(right.AsString());
                case QueryOperation.NotEqual:
                    return left.NotEqual(right);
                case QueryOperation.Equal:
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
            QueryOperation operation, QueryCase searchCase)
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
                case QueryOperation.LessThan:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .LessThan(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .LessThan(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.CompareTo(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .LessThan(Mapper.Constant(0, typeof(int)));
                case QueryOperation.LessThanOrEqual:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .LessThanOrEqual(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .LessThanOrEqual(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.CompareTo(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .LessThanOrEqual(Mapper.Constant(0, typeof(int)));
                case QueryOperation.GreaterThan:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default), 
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .GreaterThan(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .GreaterThan(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.CompareTo(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .GreaterThan(Mapper.Constant(0, typeof(int)));
                case QueryOperation.GreaterThanOrEqual:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .GreaterThanOrEqual(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .GreaterThanOrEqual(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.CompareTo(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .GreaterThanOrEqual(Mapper.Constant(0, typeof(int)));
                case QueryOperation.Between:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .Between(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .Between(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.Between(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                case QueryOperation.NotBetween:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default))
                                .NotBetween(Mapper.Constant(0, typeof(int)));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.CompareTo(
                            Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default))
                                .NotBetween(Mapper.Constant(0, typeof(int)));
                    }
                    return Mapper.NotBetween(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                case QueryOperation.Contains:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.Contains(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.Contains(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.Contains(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case QueryOperation.NotContains:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.NotContains(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.NotContains(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.NotContains(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case QueryOperation.StartsWith:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.StartsWith(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.StartsWith(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.StartsWith(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case QueryOperation.NotStartsWith:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.NotStartsWith(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.NotStartsWith(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.NotStartsWith(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case QueryOperation.EndsWith:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.EndsWith(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.EndsWith(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.EndsWith(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case QueryOperation.NotEndsWith:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.NotEndsWith(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.NotEndsWith(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.NotEndsWith(Mapper.WhenNull(left, left, _default),
                        Mapper.WhenNull(right, right, _default));
                case QueryOperation.Equal:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.Equal(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == QueryCase.Upper)
                    {
                        return Mapper.Equal(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                    }
                    return Mapper.Equal(Mapper.WhenNull(left, Mapper.ToUpper(left), _default),
                            Mapper.WhenNull(right, Mapper.ToUpper(right), _default));
                case QueryOperation.NotEqual:
                    if (searchCase == QueryCase.Lower)
                    {
                        return Mapper.NotEqual(Mapper.WhenNull(left, Mapper.ToLower(left), _default),
                            Mapper.WhenNull(right, Mapper.ToLower(right), _default));
                    }
                    else if (searchCase == QueryCase.Upper)
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
