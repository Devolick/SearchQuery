using System.Linq.Expressions;

namespace SearchQuery.Builder
{
    internal static class EnumBuilder
    {
        internal static Expression Enum(this MemberExpression left, IEnumerable<object> values, Operation operation) {
            switch (operation)
            {
                case Operation.GreaterThan:
                case Operation.GreaterThanOrEqual:
                case Operation.LessThan:
                case Operation.LessThanOrEqual:
                case Operation.Between:
                case Operation.NotBetween:
                case Operation.StartsWith:
                case Operation.NotStartsWith:
                case Operation.Contains:
                case Operation.NotContains:
                case Operation.EndsWith:
                case Operation.NotEndsWith:
                    throw new ArgumentException("Unsupported Enum operation");

                case Operation.Equal:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsEnum()) {
                            throw new ArgumentException("Invalid Enum values type");
                        }

                        var expression = Expression.Equal(left.IntoNullable(), right.IntoNullable());

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }
                case Operation.NotEqual:
                {
                    var operations = values.Select((right) => {
                        if (!right.IsEnum()) {
                            throw new ArgumentException("Invalid Enum values type");
                        }

                        var expression = Expression.NotEqual(left.IntoNullable(), right.IntoNullable());

                        return (new Expression[] { left, right.IntoNullable()})
                            .AnyEqualNulls(Expression.Constant(false), expression);
                    });

                    return operations.Aggregate(Expression.AndAlso);
                }

                default: throw new ArgumentException("Uknown Enum operation");
            }
        }

    }
}