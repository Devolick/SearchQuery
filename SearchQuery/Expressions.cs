using System.Globalization;
using System.Linq.Expressions;

namespace SearchQuery
{
    internal static class Expressions
    {
        internal static Expression AnyEqualNulls(this IEnumerable<Expression> values, 
            Expression next, Expression result)
        {
            var expressions = values.Select((value) => 
                Expression.Equal(value, Expression.Constant(null)));

            return Expression.Condition(expressions.Aggregate(Expression.OrElse), next, result);
        }

        internal static Expression ConvertInto(this Expression left, Type type)
        {
            var expression = Expression.Convert(left, type);

            return expression;
        }
        internal static Expression ConvertInto(this object left, Type type)
        {
            var value = Expression.Constant(left);
            var expression = Expression.Convert(value, type);

            return expression;
        }

        internal static Expression IntoNullable(this Expression left)
        {
            return left.ConvertInto(left.Type.InNull());
        }

        internal static Expression IntoNullable(this object left)
        {
            var value = Expression.Constant(left);

            return value.ConvertInto(value.Type.InNull());
        }

        internal static Expression IntoString(this Expression left, Case @case = Case.Default)
        {
            var expression = Expression.Call(left,
                typeof(object).GetMethod(nameof(object.ToString), Type.EmptyTypes));

            if (@case == Case.Upper) {
                return expression.ToUpper();
            } else if (@case == Case.Lower) {
                return expression.ToLower();
            } else {
                return expression;
            }
        }

        internal static Expression IntoString(this object left, Case @case = Case.Default)
        {
            var value = Expression.Constant(left);
            
            return value.IntoString(@case);
        }

        internal static bool CanDate(this object left, Format format)
        {
            if (left.IsString()) {
                if (DateTime.TryParseExact((string)left, 
                        new [] {Formats.Values[format]}, 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _)) {
                    return true;
                }
            }

            return false;
        }

        internal static Expression IntoDate(this object left, Format format)
        {
            if (left.IsString()) {
                var expression = Expression.Constant(DateTime.ParseExact((string)left, 
                    new [] {Formats.Values[format]}, CultureInfo.InvariantCulture));

                return expression;
            }

            return Expression.Constant(left);
        }

        internal static Expression IntoDateString(this Expression left, Format format)
        {
            if (left.IsDate()) {
                var expression = Expression.Call(left.ConvertInto(typeof(DateTime)),
                    typeof(DateTime).GetMethod(nameof(DateTime.ToString), 
                        new[] { typeof(string) }), 
                        Expression.Constant(Formats.Values[format]));

                return expression;
            }

            return left;
        }

        internal static Expression ToLower(this Expression left)
        {
            var expression = Expression.Call(left,
                typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes));

            return expression;
        }
        
        internal static Expression ToUpper(this Expression left)
        {
            var expression = Expression.Call(left,
                typeof(string).GetMethod(nameof(string.ToUpper), Type.EmptyTypes));

            return expression;
        }
    }
}
