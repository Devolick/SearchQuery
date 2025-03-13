using System.Linq.Expressions;
using System.Net;

namespace SearchQuery.Builder
{
    internal static class StringBuilder
    {
        internal static Expression String(this MemberExpression left, IEnumerable<object> values, Operation operation,
            Case queryCase) {
            switch (operation)
            {
                case Operation.StartsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.StartsWith),
                            new[] { typeof(string) }), right.IntoString(queryCase));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.NotStartsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Not(Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.StartsWith),
                            new[] { typeof(string) }), right.IntoString(queryCase)));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.Contains:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.Contains),
                            new[] { typeof(string) }), right.IntoString(queryCase));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.NotContains:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Not(Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.Contains),
                            new[] { typeof(string) }), right.IntoString(queryCase)));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.EndsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.EndsWith),
                            new[] { typeof(string) }), right.IntoString(queryCase));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.NotEndsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Not(Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.EndsWith),
                            new[] { typeof(string) }), right.IntoString(queryCase)));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.Equal:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Equal(left.IntoString(queryCase), right.IntoString(queryCase));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.NotEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Not(Expression.Equal(
                            left.IntoString(queryCase), right.IntoString(queryCase)));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.GreaterThan:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Equal(Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    right.IntoString(queryCase)), Expression.Constant(1));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.GreaterThanOrEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Or(
                            Expression.Equal(Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    right.IntoString(queryCase)), Expression.Constant(0)),
                            Expression.Equal(Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    right.IntoString(queryCase)), Expression.Constant(1)));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.LessThan:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Equal(Expression.Call(
                            left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    right.IntoString(queryCase)), Expression.Constant(-1));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.LessThanOrEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Or(
                            Expression.Equal(Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    right.IntoString(queryCase)), Expression.Constant(0)),
                            Expression.Equal(Expression.Call(left.IntoString(queryCase),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    right.IntoString(queryCase)), Expression.Constant(-1)));

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.Between:
                {

                    var min = values.ElementAt(0);
                    var max = values.ElementAt(1);

                    if (!min.IsString() || !max.IsString()) {
                        throw new ArgumentException("Invalid values type");
                    }

                    var greaterThanOrEqual = Expression.Or(
                        Expression.Equal(Expression.Call(left.IntoString(queryCase),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), 
                                min.IntoString(queryCase)), Expression.Constant(0)),
                        Expression.Equal(Expression.Call(left.IntoString(queryCase),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), 
                                min.IntoString(queryCase)), Expression.Constant(1)));

                  var lessThanOrEqual = Expression.Or(
                        Expression.Equal(Expression.Call(left.IntoString(queryCase),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), 
                                max.IntoString(queryCase)), Expression.Constant(0)),
                        Expression.Equal(Expression.Call(left.IntoString(queryCase),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), 
                                max.IntoString(queryCase)), Expression.Constant(-1)));

                    var expression = Expression.And(greaterThanOrEqual, lessThanOrEqual);

                    return (new Expression[]{ left, min.IntoNullable(), max.IntoNullable() })
                        .AnyEqualNulls(Expression.Constant(false), expression);
                }
                case Operation.NotBetween:
                {
                    var min = values.ElementAt(0);
                    var max = values.ElementAt(1);

                    if (!min.IsString() || !max.IsString()) {
                        throw new ArgumentException("Invalid values type");
                    }

                    var greaterThanOrEqual = Expression.Or(
                        Expression.Equal(Expression.Call(left.IntoString(queryCase),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), 
                                min.IntoString(queryCase)), Expression.Constant(0)),
                        Expression.Equal(Expression.Call(left.IntoString(queryCase),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), 
                                min.IntoString(queryCase)), Expression.Constant(1)));

                  var lessThanOrEqual = Expression.Or(
                        Expression.Equal(Expression.Call(left.IntoString(queryCase),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), 
                                max.IntoString(queryCase)), Expression.Constant(0)),
                        Expression.Equal(Expression.Call(left.IntoString(queryCase),
                        typeof(string).GetMethod(nameof(string.CompareTo),
                            new[] { typeof(string) }), 
                                max.IntoString(queryCase)), Expression.Constant(-1)));

                    var expression = Expression.Not(Expression.And(greaterThanOrEqual, lessThanOrEqual));

                    return (new Expression[] { left, min.IntoNullable(), max.IntoNullable()})
                        .AnyEqualNulls(Expression.Constant(false), expression);
                }
                
                default: throw new ArgumentException("Uknown String operation");
            }
        }
    }
}
