using System.Linq.Expressions;

namespace SearchQuery
{
    internal sealed class Mapper
    {
        private Expression? expression;

        public Mapper()
        {
            expression = null;
        }
        public Mapper(Expression expression)
        {
            this.expression = expression;
        }
        public Mapper(Mapper mapper)
        {
            expression = mapper.expression;
        }

        // Factory

        public static Mapper Parameter<T>(string name = "parameter")
        {
            var expression = Expression.Parameter(typeof(T), name);

            return new Mapper(expression);
        }
        public static Mapper Property(Mapper left, string name = "property")
        {
            var expression = Expression.Property(left.expression, name);

            return new Mapper(expression);
        }
        public static Mapper Constant(object value, Type type)
        {
            var expression = Expression.Constant(value, type);

            return new Mapper(expression);
        }

        // Predicate

        public static Mapper Where<T>(Mapper left, Mapper right)
        {
            var expression = Expression.Lambda<Func<T, bool>>(
                left.expression, (ParameterExpression)right.expression);

            return new Mapper(expression);
        }

        // Aggregate

        public static Mapper OrAggregate(IEnumerable<Mapper> collection)
        {
            var expression = collection
                .Aggregate(default(Expression), (acc, c) =>
                   (acc != null ? Expression.Or(acc, c.expression) : c.expression) ?? 
                        Expression.Constant(true));

            return new Mapper(expression);
        }

        public static Mapper AndAggregate(IEnumerable<Mapper> collection)
        {
            var expression = collection
                .Aggregate(default(Expression), (acc, c) =>
                   (acc != null ? Expression.And(acc, c.expression) : c.expression) ?? 
                        Expression.Constant(true));

            return new Mapper(expression);
        }

        // Condition

        public static Mapper SkipNull(Mapper left, Mapper right)
        {
            var condition = Expression.Equal(left.expression,
                Expression.Constant(null));
            var expression = Expression.Condition(condition, 
                Expression.Constant(false), right.expression);

            return new Mapper(expression);
        }

        public static Mapper WhenNull(Mapper left, Mapper right, Mapper _default)
        {
            var condition = Expression.Equal(left.expression,
                Expression.Constant(null));
            var expression = Expression.Condition(condition,
                Expression.Coalesce(left.expression, _default.expression), right.expression);

            return new Mapper(expression);
        }

        // Comparison

        public static Mapper LessThan(Mapper left, Mapper right)
        {
            var expression = Expression.LessThan(left.expression, right.expression);

            return new Mapper(expression);
        }
        public Mapper LessThan(Mapper right)
        {
            expression = LessThan(this, right).expression;

            return this;
        }
        public static Mapper LessThanOrEqual(Mapper left, Mapper right)
        {
            var expression = Expression.LessThanOrEqual(left.expression, right.expression);

            return new Mapper(expression);
        }
        public Mapper LessThanOrEqual(Mapper right)
        {
            expression = LessThanOrEqual(this, right).expression;

            return this;
        }
        public static Mapper GreaterThan(Mapper left, Mapper right)
        {
            var expression = Expression.GreaterThan(left.expression, right.expression);

            return new Mapper(expression);
        }
        public Mapper GreaterThan(Mapper right)
        {
            expression = GreaterThan(this, right).expression;

            return this;
        }
        public static Mapper GreaterThanOrEqual(Mapper left, Mapper right)
        {
            var expression = Expression.GreaterThanOrEqual(left.expression, right.expression);

            return new Mapper(expression);
        }
        public Mapper GreaterThanOrEqual(Mapper right)
        {
            expression = GreaterThanOrEqual(this, right).expression;

            return this;
        }
        public static Mapper Between(Mapper left, Mapper right)
        {
            var expression = Expression.OrElse(Expression
                .GreaterThanOrEqual(left.expression, right.expression),
                    Expression.LessThanOrEqual(left.expression, right.expression));

            return new Mapper(expression);
        }
        public Mapper Between(Mapper right)
        {
            expression = Between(this, right).expression;

            return this;
        }
        public static Mapper NotBetween(Mapper left, Mapper right)
        {
            left = Between(left, right);
            var expression = Expression.Not(left.expression);

            return new Mapper(expression);
        }
        public Mapper NotBetween(Mapper right)
        {
            expression = NotBetween(this, right).expression;

            return this;
        }
        public static Mapper Contains(Mapper left, Mapper right)
        {
            var expression = Expression.Call(left.expression,
                typeof(string).GetMethod(nameof(string.Contains),
                new[] { typeof(string) }), right.expression);

            return new Mapper(expression);
        }
        public Mapper Contains(Mapper right)
        {
            expression = Contains(this, right).expression;

            return this;
        }
        public static Mapper NotContains(Mapper left, Mapper right)
        {
            left = Contains(left, right);
            var expression = Expression.Not(left.expression);

            return new Mapper(expression);
        }
        public Mapper NotContains(Mapper right)
        {
            expression = NotContains(this, right).expression;

            return this;
        }
        public static Mapper StartsWith(Mapper left, Mapper right)
        {
            var expression = Expression.Call(left.expression,
                typeof(string).GetMethod(nameof(string.StartsWith),
                new[] { typeof(string) }), right.expression);

            return new Mapper(expression);
        }
        public Mapper StartsWith(Mapper right)
        {
            expression = StartsWith(this, right).expression;

            return this;
        }
        public static Mapper NotStartsWith(Mapper left, Mapper right)
        {
            left = StartsWith(left, right);
            var expression = Expression.Not(left.expression);

            return new Mapper(expression); ;
        }
        public Mapper NotStartsWith(Mapper right)
        {
            expression = NotStartsWith(this, right).expression;

            return this;
        }
        public static Mapper EndsWith(Mapper left, Mapper right)
        {
            var expression = Expression.Call(left.expression,
                typeof(string).GetMethod(nameof(string.EndsWith),
                new[] { typeof(string) }), right.expression);

            return new Mapper(expression);
        }
        public Mapper EndsWith(Mapper right)
        {
            expression = EndsWith(this, right).expression;  

            return this;
        }
        public static Mapper NotEndsWith(Mapper left, Mapper right)
        {
            left = EndsWith(left, right);
            var expression = Expression.Not(left.expression);

            return new Mapper(expression);
        }
        public Mapper NotEndsWith(Mapper right)
        {
            expression = NotEndsWith(this, right).expression;

            return this;
        }
        public static Mapper Equal(Mapper left, Mapper right)
        {
            var expression = Expression.Equal(left.expression, right.expression);

            return new Mapper(expression);
        }
        public Mapper Equal(Mapper right)
        {
            expression = Equal(this, right).expression;

            return this;
        }
        public static Mapper NotEqual(Mapper left, Mapper right)
        {
            left = Equal(left, right);
            var expression = Expression.Not(left.expression);

            return new Mapper(expression);
        }
        public Mapper NotEqual(Mapper right)
        {
            expression = NotEqual(this, right).expression;

            return this;
        }
        public static Mapper CompareTo(Mapper left, Mapper right)
        {
            var expression = Expression.Call(left.expression,
                typeof(string).GetMethod(nameof(string.CompareTo),
                new[] { typeof(string) }), right.expression);

            return new Mapper(expression);
        }
        public Mapper CompareTo(Mapper right)
        {
            expression = CompareTo(this, right).expression;

            return this;
        }

        // Transform

        public static Mapper AsString(Mapper left)
        {
            var expression = Expression.Call(left.expression,
                typeof(object).GetMethod(nameof(object.ToString), Type.EmptyTypes));

            return new Mapper(expression);
        }
        public Mapper AsString()
        {
            expression = Expression.Call(expression,
                typeof(object).GetMethod(nameof(object.ToString), Type.EmptyTypes));

            return this;
        }
        public static Mapper ToLower(Mapper left)
        {
            var expression = Expression.Call(left.expression,
                typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes));

            return new Mapper(expression);
        }
        public Mapper ToLower()
        {
            expression = ToLower(this).expression;

            return this;
        }
        public static Mapper ToUpper(Mapper left)
        {
            var expression = Expression.Call(left.expression,
                typeof(string).GetMethod(nameof(string.ToUpper), Type.EmptyTypes));

            return new Mapper(expression);
        }
        public Mapper ToUpper()
        {
            expression = ToUpper(this).expression;

            return this;
        }

        // Cast

        public static explicit operator Expression?(Mapper map) => map.expression;
    }
}
