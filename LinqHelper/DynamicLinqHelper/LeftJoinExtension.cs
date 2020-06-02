using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Mael.DynamicLinqHelper
{
    public static class LeftJoinExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="innerObject"></param>
        /// <param name="outerKey"></param>
        /// <param name="innerKey"></param>
        /// <param name="outerSelectedProperties"></param>
        /// <param name="innerSelectedProperties"></param>
        /// <returns></returns>
        public static IQueryable LeftJoin(this IQueryable query,
                                          IQueryable innerObject,
                                          string outerKey,
                                          string innerKey,
                                          List<string> outerSelectedProperties,
                                          List<string> innerSelectedProperties)
        {
            if (outerSelectedProperties is null)
                throw new ArgumentNullException("outerSelectedProperties cannot be null");

            if (innerSelectedProperties is null)
                throw new ArgumentNullException("innerSelectedProperties cannot be null");

            query = query.GroupJoin(ParsingConfig.DefaultEFCore21,
                                    innerObject,
                                    outerKey,
                                    innerKey,
                                    GroupJoinSelector);

            string selectedProperties = GenerateSelectedPropertiesString(outerSelectedProperties,
                                                                         innerSelectedProperties);

            query = query.SelectMany(SelectManyCollectionSelector,
                                     selectedProperties,
                                     SelectManyOuter,
                                     SelectManyInner);

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerSelectedProperties"></param>
        /// <param name="innerSelectedProperties"></param>
        /// <returns></returns>
        private static string GenerateSelectedPropertiesString(List<string> outerSelectedProperties,
                                                               List<string> innerSelectedProperties)
        {
            if (outerSelectedProperties.Count == 0 && innerSelectedProperties.Count == 0)
                throw new ArgumentException("outerSelectedProperties or innerSelectedProperties must contains a value");

            StringBuilder sb = new StringBuilder();

            sb.Append("new { ");
            outerSelectedProperties.ForEach(p => sb.Append($"{SelectManyOuterSelection}.{p},"));
            innerSelectedProperties.ForEach(p => sb.Append($"{SelectManyInnerSelection} == null ? null : {SelectManyInnerSelection}.{p},"));
            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");

            return sb.ToString();
        }


        /// <summary>
        /// Determina il nome da assegnare all'aggregato principale (tabella principale)
        /// </summary>
        private static string SelectManyOuter { get => "source"; }

        /// <summary>
        /// Determina il nome da assegnare alle proprietà della join (tabella secondaria)
        /// </summary>
        private static string SelectManyInner { get => "detail"; }


        /// <summary>
        /// Determina il prefisso per le select delle join (tabella principale)
        /// </summary>
        private static string SelectManyOuterSelection { get => $"{SelectManyOuter}.{GroupJoinInnerPrefix}"; }

        /// <summary>
        /// Determina il prefisso per le select delle join (tabella secondaria)
        /// </summary>
        private static string SelectManyInnerSelection { get => SelectManyInner; }


        /// <summary>
        /// Determina il prefisso per le select delle join (tabella principale)
        /// </summary>
        private static string GroupJoinInnerPrefix { get => "A"; }

        /// <summary>
        /// Determina il prefisso per le select delle join (tabella secondaria)
        /// </summary>
        private static string GroupJoinOuterPrefix { get => "B"; }

        /// <summary>
        /// Selector per la groupjoin
        /// </summary>
        private static string GroupJoinSelector { get => $"new (outer AS {GroupJoinInnerPrefix}, inner AS {GroupJoinOuterPrefix})"; }

        /// <summary>
        /// Selector per la parte di join
        /// </summary>
        private static string SelectManyCollectionSelector { get => $"{GroupJoinOuterPrefix}.DefaultIfEmpty()"; }
    }
}
