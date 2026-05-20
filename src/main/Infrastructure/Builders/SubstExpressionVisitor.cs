using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Builders;

[ExcludeFromCodeCoverage]
public class SubstExpressionVisitor : ExpressionVisitor
{
    public Dictionary<Expression, Expression> Subst { get; } = [];

    protected override Expression VisitParameter(
        ParameterExpression node)
    {
        return Subst.GetValueOrDefault(node, node);
    }
}
