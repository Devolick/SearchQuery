using System.Linq.Expressions;
using System.Net;

namespace SearchQuery.Builder
{
    internal static class NumberBuilder
    {
        internal static Expression Number(this MemberExpression left, IEnumerable<object> values, Operation operation) {
            switch (operation)
            {
                case SearchQuery.Operation.StartsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.StartsWith),
                            new[] { typeof(string) }), right.IntoString());

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.NotStartsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Not(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.StartsWith),
                            new[] { typeof(string) }), right.IntoString()));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.Contains:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.Contains),
                            new[] { typeof(string) }), right.IntoString());

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.NotContains:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Not(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.Contains),
                            new[] { typeof(string) }), right.IntoString()));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.EndsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.EndsWith),
                            new[] { typeof(string) }), right.IntoString());

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.NotEndsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Not(Expression.Call(left.IntoString(),
                            typeof(string).GetMethod(nameof(string.EndsWith),
                            new[] { typeof(string) }), right.IntoString()));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.Equal:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.Equal(left.IntoNullable(),
                            right.ConvertInto(left.IntoNullable().Type));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.NotEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.NotEqual(left.IntoNullable(), 
                            right.ConvertInto(left.IntoNullable().Type));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.GreaterThan:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.GreaterThan(left.IntoNullable(), 
                            right.ConvertInto(left.IntoNullable().Type));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.GreaterThanOrEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.GreaterThanOrEqual(left.IntoNullable(), 
                            right.ConvertInto(left.IntoNullable().Type));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.LessThan:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.LessThan(left.IntoNullable(), 
                            right.ConvertInto(left.IntoNullable().Type));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.LessThanOrEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsNumber()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.LessThanOrEqual(left.IntoNullable(), 
                            right.ConvertInto(left.IntoNullable().Type));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case SearchQuery.Operation.Between:
                {
                    var min = values.ElementAt(0);
                    var max = values.ElementAt(1);

                    if (!min.IsNumber() || !max.IsNumber()) {
                        throw new ArgumentException("Invalid values type");
                    }

                    var greaterThanOrEqual = Expression.GreaterThanOrEqual(left.IntoNullable(), 
                        min.ConvertInto(left.IntoNullable().Type));
                    var lessThanOrEqual = Expression.LessThanOrEqual(left.IntoNullable(), 
                        max.ConvertInto(left.IntoNullable().Type));

                    var expression = Expression.And(greaterThanOrEqual, lessThanOrEqual);

                    return (new Expression[] { left.IntoNullable(), min.IntoNullable(), max.IntoNullable()})
                        .AnyEqualNulls(Expression.Constant(false), expression);
                }
                case SearchQuery.Operation.NotBetween:
                {
                    var min = values.ElementAt(0);
                    var max = values.ElementAt(1);

                    if (!min.IsNumber() || !max.IsNumber()) {
                        throw new ArgumentException("Invalid values type");
                    }

                    var greaterThanOrEqual = Expression.GreaterThanOrEqual(left.IntoNullable(), 
                        min.ConvertInto(left.IntoNullable().Type));
                    var lessThanOrEqual = Expression.LessThanOrEqual(left.IntoNullable(), 
                        max.ConvertInto(left.IntoNullable().Type));

                    var expression = Expression.Not(Expression.And(greaterThanOrEqual, lessThanOrEqual));

                    return (new Expression[] { left.IntoNullable(), min.IntoNullable(), max.IntoNullable()})
                        .AnyEqualNulls(Expression.Constant(false), expression);
                }
                
                default: throw new ArgumentException("Uknown Number operation");
            }
        }
        
    }
}
