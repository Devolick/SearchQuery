using System.Linq.Expressions;
using System.Net;

namespace SearchQuery.Builder
{
    internal static class BooleanBuilder
    {
        internal static Expression Boolean(this MemberExpression left, IEnumerable<object> values, Operation operation) {
            switch (operation)
            {
                case SearchQuery.Operation.StartsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString() && !right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.StartsWith),
                            new[] { typeof(string) }), right.IntoString());

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.NotStartsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString() && !right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Not(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.StartsWith),
                            new[] { typeof(string) }), right.IntoString()));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.Contains:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString() && !right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.Contains),
                            new[] { typeof(string) }), right.IntoString());

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.NotContains:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString() && !right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Not(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.Contains),
                            new[] { typeof(string) }), right.IntoString()));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.EndsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString() && !right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.EndsWith),
                            new[] { typeof(string) }), right.IntoString());

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.NotEndsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString() && !right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Not(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.EndsWith),
                            new[] { typeof(string) }), right.IntoString()));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.Equal:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Equal(left.IntoNullable(), right.IntoNullable());

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.NotEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.NotEqual(left.IntoNullable(), right.IntoNullable());

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.GreaterThan:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Equal(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), right.IntoString()), Expression.Constant(1));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.GreaterThanOrEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Or(
                            Expression.Equal(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), right.IntoString()), Expression.Constant(0)),
                            Expression.Equal(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), right.IntoString()), Expression.Constant(1)));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.LessThan:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Equal(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), right.IntoString()), Expression.Constant(-1));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.LessThanOrEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsBoolean()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Or(
                            Expression.Equal(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), right.IntoString()), Expression.Constant(0)),
                            Expression.Equal(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), right.IntoString()), Expression.Constant(-1)));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.Between:
                {
                    var min = values.ElementAt(0);
                    var max = values.ElementAt(1);

                    if (!min.IsBoolean() || !max.IsBoolean()) {
                        throw new ArgumentException("Invalid values type");
                    }

                    var greaterThanOrEqual = Expression.Or(
                        Expression.Equal(Expression.Call(left.IntoString(),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), min.IntoString()), Expression.Constant(0)),
                        Expression.Equal(Expression.Call(left.IntoString(),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), min.IntoString()), Expression.Constant(1)));

                  var lessThanOrEqual = Expression.Or(
                        Expression.Equal(Expression.Call(left.IntoString(),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), max.IntoString()), Expression.Constant(0)),
                        Expression.Equal(Expression.Call(left.IntoString(),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), max.IntoString()), Expression.Constant(-1)));

                    var expression = Expression.And(greaterThanOrEqual, lessThanOrEqual);

                    return (new Expression[] { left.IntoNullable(), min.IntoString(), max.IntoString()})
                        .AnyEqualNulls(Expression.Constant(false), expression);
                }
                case SearchQuery.Operation.NotBetween:
                {
                    var min = values.ElementAt(0);
                    var max = values.ElementAt(1);

                    if (!min.IsBoolean() || !max.IsBoolean()) {
                        throw new ArgumentException("Invalid values type");
                    }

                    var greaterThanOrEqual = Expression.Or(
                        Expression.Equal(Expression.Call(left.IntoString(),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), min.IntoString()), Expression.Constant(0)),
                        Expression.Equal(Expression.Call(left.IntoString(),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), min.IntoString()), Expression.Constant(1)));

                  var lessThanOrEqual = Expression.Or(
                        Expression.Equal(Expression.Call(left.IntoString(),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), max.IntoString()), Expression.Constant(0)),
                        Expression.Equal(Expression.Call(left.IntoString(),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), max.IntoString()), Expression.Constant(-1)));

                    var expression = Expression.Not(Expression.And(greaterThanOrEqual, lessThanOrEqual));

                    return (new Expression[] { left.IntoNullable(), min.IntoString(), max.IntoString()})
                        .AnyEqualNulls(Expression.Constant(false), expression);
                }
                
                default: throw new ArgumentException("Uknown Boolean operation");
            }
        }

    }
}