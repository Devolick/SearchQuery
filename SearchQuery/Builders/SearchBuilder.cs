using System.Linq.Expressions;
using System.Net;

namespace SearchQuery.Builder
{
    internal sealed class SearchBuilder
    {
        private readonly Search search;

        public SearchBuilder(Search search)
        {
            this.search = search;
        }

        public Func<T, bool> Compile<T>()
        {
            var expression = Build<T>();
            var predicate = expression.Compile();

            return predicate;
        }

        public Expression<Func<T, bool>> Build<T>()
        {
            var parameter = Expression.Parameter(typeof(T), "parameter");
            var queries = Next(search, parameter);

            if (queries == null || queries.Type.IsNull()) {
                var expression = Expression.Lambda<Func<T, bool>>(
                    Expression.Constant(true), parameter);

                return expression;
            } else {
                var expression = Expression.Lambda<Func<T, bool>>(queries, parameter);

                return expression;
            }
        }

        public Expression Next(ISearch next, ParameterExpression parameter) {
            if (typeof(Search).IsAssignableFrom(next.GetType()) || 
                typeof(Query).IsAssignableFrom(next.GetType())) {
                var query = (Query)next;
                var expressions = query.Conditions.Select((item) => {
                    if (item.GetType() == typeof(Query)) {
                        var query = (Query)item;
                        var expression = Next(query, parameter);
    
                        return expression;
                    } else {
                        var expression = Next(item, parameter);

                        return expression;
                    }
                })!;

                if (expressions.Count() < 1) {
                    if (typeof(Query).IsAssignableFrom(next.GetType()))
                    {
                        return Expression.Constant(true);
                    }
                    
                    return Expression.Call(
                        typeof(Enumerable), 
                        nameof(Enumerable.Empty),
                        new Type[] { typeof(Expression) } 
                    );
                }

                var aggregation = query.Operator == Operator.Or ? 
                    expressions.Aggregate(Expression.OrElse) : 
                    expressions.Aggregate(Expression.AndAlso);

                return aggregation;
            } else if (typeof(Condition).IsAssignableFrom(next.GetType())) {
                var condition = (Condition)next;
                var expression = Condition(condition, parameter);
                
                return expression;
            }

            throw new ArgumentException("Uknown condition type");
        }

        private Expression Condition(Condition condition, 
            ParameterExpression parameter)
        {
            var property = Expression.Property(parameter, condition.Field);
            var operation = Operation(condition.Operation, property, 
                condition.Values, condition.Incase, condition.Format);

            return operation;
        }

        private Expression Operation(Operation operation,
            MemberExpression left, IEnumerable<object> values, 
            Case queryCase, Format? format) {
            if (left.IsString()) {
                return left.String(values, operation, queryCase);
            } else if (left.IsNumber()) {
                return left.Number(values, operation);
            } else if (left.IsBoolean()) {
                return left.Boolean(values, operation);
            } else if (left.IsDate()) {
                return left.Date(values, operation, format ?? search.Format);
            }

            throw new ArgumentException("Uknown Operation argument type");
        }
    }
}
