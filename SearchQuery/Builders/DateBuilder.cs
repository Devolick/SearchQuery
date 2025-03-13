using System.Linq.Expressions;
using System.Net;

namespace SearchQuery.Builder
{
    internal static class DateBuilder
    {
        internal static Expression Date(this MemberExpression left, 
            IEnumerable<object> values, Operation operation, Format format) { 
            switch (operation)
            {
                case Operation.StartsWith:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsString()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        var expression = Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.StartsWith),
                            new[] { typeof(string) }), Expression.Constant(right).IntoDateString(format));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
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

                        var expression = Expression.Not(Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.StartsWith),
                            new[] { typeof(string) }), Expression.Constant(right).IntoDateString(format)));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
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
                        
                        var expression = Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.Contains),
                            new[] { typeof(string) }), Expression.Constant(right).IntoDateString(format));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
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
                        
                        var expression = Expression.Not(Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.Contains),
                            new[] { typeof(string) }), Expression.Constant(right).IntoDateString(format)));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
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

                            var expression = Expression.Call(left.IntoDateString(format),
                                typeof(string).GetMethod(nameof(string.EndsWith),
                                new[] { typeof(string) }), Expression.Constant(right).IntoDateString(format));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
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
                        
                        var expression = Expression.Not(Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.EndsWith),
                            new[] { typeof(string) }), Expression.Constant(right).IntoDateString(format)));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.Equal:
                {
                    var operations = values.Select((right) => {
                        if (!right.CanDate(format) && !right.IsDate()) {
                            throw new ArgumentException("Invalid values type");
                        }

                        if (left.Type.IsNull() || right.GetType().IsNull())
                        {
                            return Expression.Equal(Expression.Constant(left.Type.IsNull() && right.GetType().IsNull()), Expression.Constant(true));
                        } else {
                            var expression = Expression.Equal(left.IntoDateString(format), 
                                Expression.Constant(right).IntoDateString(format));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                        }
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.NotEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.CanDate(format) && !right.IsDate()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        var expression = Expression.NotEqual(left.IntoDateString(format), 
                            Expression.Constant(right).IntoDateString(format));

                        return (new Expression[] { left.IntoNullable(), right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.GreaterThan:
                {
                    var operations = values.Select((right) => {
                        if (!right.CanDate(format) && !right.IsDate()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        if (left.Type.IsNull() || right.GetType().IsNull())
                        {
                            return Expression.Equal(Expression.Constant(
                                left.Type.IsNull() && right.GetType().IsNull()), 
                                Expression.Constant(true));
                        } else {
                            var expression = Expression.GreaterThan(
                                Expression.Call(left.IntoDateString(format),
                                    typeof(string).GetMethod(nameof(string.CompareTo),
                                        new[] { typeof(string) }), 
                                            Expression.Constant(right).IntoDateString(format)),
                                Expression.Constant(0));

                            return new Expression[] { left.IntoNullable(), right.IntoNullable()}
                                .AnyEqualNulls(Expression.Constant(false), expression);
                        }
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.GreaterThanOrEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.CanDate(format) && !right.IsDate()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        if (left.Type.IsNull() || right.GetType().IsNull())
                        {
                            return Expression.Equal(Expression.Constant(
                                left.Type.IsNull() && right.GetType().IsNull()), 
                                Expression.Constant(true));
                        } else {
                            var expression = Expression.GreaterThanOrEqual(
                                Expression.Call(left.IntoDateString(format),
                                    typeof(string).GetMethod(nameof(string.CompareTo),
                                        new[] { typeof(string) }), 
                                            Expression.Constant(right).IntoDateString(format)),
                                Expression.Constant(0));

                            return new Expression[] { left.IntoNullable(), right.IntoNullable()}
                                .AnyEqualNulls(Expression.Constant(false), expression);
                        }
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.LessThan:
                {
                    var operations = values.Select((right) => {
                        if (!right.CanDate(format) && !right.IsDate()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        if (left.Type.IsNull() || right.GetType().IsNull())
                        {
                            return Expression.Equal(Expression.Constant(
                                left.Type.IsNull() && right.GetType().IsNull()), 
                                Expression.Constant(true));
                        } else {
                            var expression = Expression.LessThan(
                                Expression.Call(left.IntoDateString(format),
                                    typeof(string).GetMethod(nameof(string.CompareTo),
                                        new[] { typeof(string) }), 
                                            Expression.Constant(right).IntoDateString(format)),
                                Expression.Constant(0));

                            return new Expression[] { left.IntoNullable(), right.IntoNullable()}
                                .AnyEqualNulls(Expression.Constant(false), expression);
                        }
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.LessThanOrEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.CanDate(format) && !right.IsDate()) {
                            throw new ArgumentException("Invalid values type");
                        }
                        
                        if (left.Type.IsNull() || right.GetType().IsNull())
                        {
                            return Expression.Equal(Expression.Constant(
                                left.Type.IsNull() && right.GetType().IsNull()), 
                                Expression.Constant(true));
                        } else {
                            var expression = Expression.LessThanOrEqual(
                                Expression.Call(left.IntoDateString(format),
                                    typeof(string).GetMethod(nameof(string.CompareTo),
                                        new[] { typeof(string) }), 
                                            Expression.Constant(right).IntoDateString(format)),
                                Expression.Constant(0));

                            return new Expression[] { left.IntoNullable(), right.IntoNullable()}
                                .AnyEqualNulls(Expression.Constant(false), expression);
                        }
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.Between:
                {
                    var min = values.ElementAt(0);
                    var max = values.ElementAt(1);

                    if ((!min.CanDate(format) && !min.IsDate()) || (!max.CanDate(format) && !max.IsDate())) {
                        throw new ArgumentException("Invalid values type");
                    }

                    var greaterThanOrEqual = Expression.GreaterThanOrEqual(
                        Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    Expression.Constant(min).IntoDateString(format)),
                        Expression.Constant(0));
                    var lessThanOrEqual = Expression.LessThanOrEqual(
                        Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    Expression.Constant(max).IntoDateString(format)),
                        Expression.Constant(0));

                    var expression = Expression.And(greaterThanOrEqual, lessThanOrEqual);

                    return (new Expression[] { left.IntoNullable(), min.IntoNullable(), max.IntoNullable()})
                        .AnyEqualNulls(Expression.Constant(false), expression);
                }
                case Operation.NotBetween:
                {
                    var min = values.ElementAt(0);
                    var max = values.ElementAt(1);

                    if ((!min.CanDate(format) && !min.IsDate()) || (!max.CanDate(format) && !max.IsDate())) {
                        throw new ArgumentException("Invalid values type");
                    }

                    var greaterThanOrEqual = Expression.GreaterThanOrEqual(
                        Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    Expression.Constant(min).IntoDateString(format)),
                        Expression.Constant(0));
                    var lessThanOrEqual = Expression.LessThanOrEqual(
                        Expression.Call(left.IntoDateString(format),
                            typeof(string).GetMethod(nameof(string.CompareTo),
                                new[] { typeof(string) }), 
                                    Expression.Constant(max).IntoDateString(format)),
                        Expression.Constant(0));

                    var expression = Expression.Not(Expression.And(greaterThanOrEqual, lessThanOrEqual));

                    return (new Expression[] { left.IntoNullable(), min.IntoNullable(), max.IntoNullable()})
                        .AnyEqualNulls(Expression.Constant(false), expression);
                }
                
                default: throw new ArgumentException("Uknown Date operation");
            }
        }
    }
}
