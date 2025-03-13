using System.Globalization;
using System.Linq.Expressions;
using Microsoft.VisualBasic;

namespace SearchQuery
{
    /// <summary>
    /// Provides extension methods for creating and manipulating expressions.
    /// </summary>
    internal static class Expressions
    {
        /// <summary>
        /// Creates an expression that checks if any of the given expressions are equal to null.
        /// </summary>
        /// <param name="values">The collection of expressions to check.</param>
        /// <param name="next">The expression to return if any value is null.</param>
        /// <param name="result">The expression to return if no value is null.</param>
        /// <returns>An expression that checks if any value is null.</returns>
        internal static Expression AnyEqualNulls(this IEnumerable<Expression> values, 
            Expression next, Expression result)
        {
            var expressions = values.Select((value) => 
                Expression.Equal(value, Expression.Constant(null)));

            return Expression.Condition(expressions.Aggregate(Expression.OrElse), next, result);
        }

        /// <summary>
        /// Converts the given expression to the specified type.
        /// </summary>
        /// <param name="left">The expression to convert.</param>
        /// <param name="type">The target type.</param>
        /// <returns>The converted expression.</returns>
        internal static Expression ConvertInto(this Expression left, Type type)
        {
            var expression = Expression.Convert(left, type);

            return expression;
        }

        /// <summary>
        /// Converts the given object to an expression of the specified type.
        /// </summary>
        /// <param name="left">The object to convert.</param>
        /// <param name="type">The target type.</param>
        /// <returns>The converted expression.</returns>
        internal static Expression ConvertInto(this object left, Type type)
        {
            var value = Expression.Constant(left);
            var expression = Expression.Convert(value, type);

            return expression;
        }

        /// <summary>
        /// Converts the given expression to a nullable type.
        /// </summary>
        /// <param name="left">The expression to convert.</param>
        /// <returns>The converted expression.</returns>
        internal static Expression IntoNullable(this Expression left)
        {
            return left.ConvertInto(left.Type.InNull());
        }

        /// <summary>
        /// Converts the given object to a nullable type.
        /// </summary>
        /// <param name="left">The object to convert.</param>
        /// <returns>The converted expression.</returns>
        internal static Expression IntoNullable(this object left)
        {
            var value = Expression.Constant(left);

            return value.ConvertInto(value.Type.InNull());
        }

        /// <summary>
        /// Converts the given expression to a string type with optional case sensitivity.
        /// </summary>
        /// <param name="left">The expression to convert.</param>
        /// <param name="case">The case sensitivity option.</param>
        /// <returns>The converted expression.</returns>
        internal static Expression IntoString(this Expression left, Case @case = Case.Default)
        {
            var parameterType = left.Type;
            if (parameterType == null) {
                return left;
            }

            if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                parameterType = Nullable.GetUnderlyingType(parameterType);
            }
            var toString = typeof(Convert).GetMethod(nameof(Convert.ToString), new[] { parameterType });
            var argument = parameterType.IsValueType ? Expression.Convert(left, parameterType) : left;
            var expression = Expression.Call(null, toString, argument);

            if (@case == Case.Upper) {
                return expression.ToUpper();
            } else if (@case == Case.Lower) {
                return expression.ToLower();
            } else {
                return expression;
            }
        }

        /// <summary>
        /// Converts the given expression to a string type with optional case sensitivity.
        /// </summary>
        /// <param name="left">The expression to convert.</param>
        /// <param name="case">The case sensitivity option.</param>
        /// <returns>The converted expression.</returns>
        internal static Expression IntoString(this object left, Case @case = Case.Default)
        {
            var value = Expression.Constant(left);
            
            return value.IntoString(@case);
        }

        /// <summary>
        /// Checks if the given object is a string and can be parsed as a date.
        /// </summary>
        /// <param name="left">The object to check.</param>
        /// <param name="format">The date format to use.</param>
        /// <returns>true if the object is a string and can be parsed as a date; otherwise, false.</returns>
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

        /// <summary>
        /// Converts the given object to a date expression using the specified format.
        /// </summary>
        /// <param name="left">The object to convert.</param>
        /// <param name="format">The date format to use.</param>
        /// <returns>The converted date expression.</returns>
        internal static Expression IntoDate(this object left, Format format)
        {
            if (left.IsString()) {
                var expression = Expression.Constant(DateTime.ParseExact((string)left, 
                    new [] {Formats.Values[format]}, CultureInfo.InvariantCulture));

                return expression;
            }

            return Expression.Constant(left);
        }

        /// <summary>
        /// Converts the given expression to a date string using the specified format.
        /// </summary>
        /// <param name="left">The expression to convert.</param>
        /// <param name="format">The date format to use.</param>
        /// <returns>The converted date string expression.</returns>
        internal static Expression IntoDateString(this Expression left, Format format)
        {
            if (left.IsDate())
            {
                var targetType = left.Type;
                if (targetType == typeof(DateTime?))
                {
                    left = Expression.Convert(left, typeof(DateTime));
                }

                var expression = Expression.Call(
                    left,
                    typeof(DateTime).GetMethod(nameof(DateTime.ToString), new[] { typeof(string) }),
                    Expression.Constant(Formats.Values[format])
                );

                return expression;
            }

            return left;
        }

        /// <summary>
        /// Converts the given expression to lowercase.
        /// </summary>
        /// <param name="left">The expression to convert.</param>
        /// <returns>The converted expression.</returns>
        internal static Expression ToLower(this Expression left)
        {
            var expression = Expression.Call(left,
                typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes));

            return expression;
        }

        /// <summary>
        /// Converts the given expression to uppercase.
        /// </summary>
        /// <param name="left">The expression to convert.</param>
        /// <returns>The converted expression.</returns>
        internal static Expression ToUpper(this Expression left)
        {
            var expression = Expression.Call(left,
                typeof(string).GetMethod(nameof(string.ToUpper), Type.EmptyTypes));

            return expression;
        }
    }
}
