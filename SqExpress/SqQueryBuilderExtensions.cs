﻿using System;
using System.Collections.Generic;
using System.Linq;
using SqExpress.QueryBuilders;
using SqExpress.StatementSyntax;
using SqExpress.Syntax.Boolean.Predicate;
using SqExpress.Syntax.Names;
using SqExpress.Syntax.Select;
using SqExpress.Syntax.Select.SelectItems;
using SqExpress.Syntax.Update;
using SqExpress.Syntax.Value;
using SqExpress.Utils;
using static SqExpress.SqQueryBuilder;

namespace SqExpress
{
    public static class SqQueryBuilderExtensions
    {
        public static ExprColumn WithSource(this ExprColumn column, IExprColumnSource? newColumnSource)
            => new ExprColumn(newColumnSource, column.ColumnName);

        public static ExprColumn WithSource(this ExprColumnName columnName, IExprColumnSource? newColumnSource)
            => new ExprColumn(newColumnSource, columnName);

        public static ExprAliasedColumn As(this ExprColumn column, ExprColumnAlias alias) =>
            new ExprAliasedColumn(column, alias);

        public static ExprAliasedSelecting As(this IExprSelecting value, ExprColumnAlias alias) =>
            new ExprAliasedSelecting(value, alias);

        public static ExprInValues In(this ExprColumn column, ExprValue value, params ExprValue[] rest)
            => new ExprInValues(column, Helpers.Combine(value, rest));

        public static ExprInValues In(this ExprColumn column, IReadOnlyList<ExprValue> items)
            => new ExprInValues(column, items.AssertNotEmpty("'IN' expressions list cannot be empty"));

        public static ExprInSubQuery In(this ExprColumn column, IExprSubQueryFinal subQuery)
            => new ExprInSubQuery(column, subQuery.Done());

        public static ExprInValues In(this ExprColumn column, int value, params int[] rest)
            => new ExprInValues(column, Helpers.Combine(value, rest, i=> Literal(i)));

        public static ExprInValues In(this ExprColumn column, IReadOnlyList<int> items)
            => new ExprInValues(column, items.AssertNotEmpty("'IN' expressions list cannot be empty").SelectToReadOnlyList(i=>Literal(i)));

        public static ExprInValues In(this ExprColumn column, string value, params string[] rest)
            => new ExprInValues(column, Helpers.Combine(value, rest, i=> Literal(i)));

        public static ExprInValues In(this ExprColumn column, IReadOnlyList<string> items)
            => new ExprInValues(column, items.AssertNotEmpty("'IN' expressions list cannot be empty").SelectToReadOnlyList(i=>Literal(i)));

        public static ExprInValues In(this ExprColumn column, Guid value, params Guid[] rest)
            => new ExprInValues(column, Helpers.Combine(value, rest, i=> Literal(i)));

        public static ExprInValues In(this ExprColumn column, IReadOnlyList<Guid> items)
            => new ExprInValues(column, items.AssertNotEmpty("'IN' expressions list cannot be empty").SelectToReadOnlyList(i=>Literal(i)));

        public static ExprLike Like(this StringTableColumn column, string pattern)
            => SqQueryBuilder.Like(column, pattern);

        public static ExprLike Like(this NullableStringTableColumn column, string pattern)
            => SqQueryBuilder.Like(column, pattern);

        public static ExprLike Like(this StringCustomColumn column, string pattern)
            => SqQueryBuilder.Like(column, pattern);

        public static ExprLike Like(this NullableStringCustomColumn column, string pattern)
            => SqQueryBuilder.Like(column, pattern);

        public static IStatement Combine(this IEnumerable<IStatement> statements) =>
            StatementList.Combine(statements is IReadOnlyList<IStatement> l ? l : statements.ToList());

        public static ExprDeleteOutput Output(this ExprDelete exprDelete, ExprAliasedColumn outputColumn, params ExprAliasedColumn[] rest) 
            => new ExprDeleteOutput(exprDelete, Helpers.Combine(outputColumn, rest));
    }
}