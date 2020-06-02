using System;
using System.Collections.Generic;
using System.Linq;

namespace Mael.LinqHelper
{
    public static class LeftJoinExtension
    {
        /// <summary>
        /// Correlates the elements of two sequences based on equality of keys. 
        /// Left part will be always return.
        /// </summary>
        /// <typeparam name="TLeft">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <param name="inner">The sequence to join.</param>
        /// <param name="leftKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from an element from the first sequence
        /// and a collection of matching elements from the second sequence.</param>
        /// <returns>An System.Collections.Generic.IQueryable`1 that contains elements of type TResult
        /// that are obtained by performing a left join on two sequences.</returns>
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
    /// <typeparam name="TLeft">The type of the elements of the first sequence.</typeparam>
    /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
    internal class GroupJoinResult<TLeft, TInner>
    {
        internal TLeft Left { get; set; }
        internal IEnumerable<TInner> Inner { get; set; }
    }
}
