using System;
using System.Collections.Generic;
using System.Linq;

namespace Mael.LinqHelper
{
    public static class LeftJoinExtension
    {
        /// <summary>
        ///     Correlates the elements of two sequences based on matching keys. TODO
        /// </summary>
        /// <typeparam name="TLeft">TODO</typeparam>
        /// <typeparam name="TInner">TODO</typeparam>
        /// <typeparam name="TKey">TODO</typeparam>
        /// <typeparam name="TResult">TODO</typeparam>
        /// <param name="query">TODO</param>
        /// <param name="inner">TODO</param>
        /// <param name="leftKeySelector">TODO</param>
        /// <param name="innerKeySelector"TODO></param>
        /// <param name="resultSelector">TODO</param>
        /// <returns>TODO</returns>
        public static IQueryable<TResult> LeftJoin<TLeft, TInner, TKey, TResult>(this IQueryable<TLeft> query,
                                                                                 IEnumerable<TInner> inner,
                                                                                 Func<TLeft, TKey> leftKeySelector,
                                                                                 Func<TInner, TKey> innerKeySelector,
                                                                                 Func<TLeft, TInner, TResult> resultSelector)
        {
            var grouped = query.GroupJoin(inner,
                                          leftKeySelector,
                                          innerKeySelector,
                                          (l, i) => new GroupJoinResult<TLeft, TInner> { Left = l, Inner = i })
                               .AsQueryable();


            var result = grouped.SelectMany(j => j.Inner.DefaultIfEmpty(),
                                            (l, i) => resultSelector(l.Left, i));

            return result;
        }
    }

    /// <summary>
    /// GroupJoin output result
    /// </summary>
    /// <typeparam name="TLeft">TODO</typeparam>
    /// <typeparam name="TInner">TODO</typeparam>
    internal class GroupJoinResult<TLeft, TInner>
    {
        internal TLeft Left { get; set; }
        internal IEnumerable<TInner> Inner { get; set; }
    }
}
