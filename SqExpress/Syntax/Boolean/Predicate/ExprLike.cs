﻿using SqExpress.Syntax.Value;

namespace SqExpress.Syntax.Boolean.Predicate
{
    public class ExprLike : ExprPredicate
    {
        public ExprLike(ExprValue test, ExprStringLiteral pattern)
        {
            this.Test = test;
            this.Pattern = pattern;
        }

        public ExprValue Test { get; }

        public ExprStringLiteral Pattern { get; }

        public override TRes Accept<TRes, TArg>(IExprVisitor<TRes, TArg> visitor, TArg arg)
            => visitor.VisitExprLike(this, arg);
    }
}