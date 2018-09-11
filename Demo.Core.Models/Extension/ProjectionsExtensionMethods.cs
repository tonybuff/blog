using System.Collections.Generic;
using AutoMapper;

namespace Demo.Core.Models.Extension
{
    public static class ProjectionsExtensionMethods
    {
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="TProjection">对象类型</typeparam>
        /// <returns></returns>
        public static TProjection ProjectedAs<TProjection>(this Base.Entity item)
            where TProjection : class,new()
        {
            return Mapper.Map<TProjection>(item);
        }

        /// <summary>
        /// 对象集合转换
        /// </summary>
        /// <typeparam name="TProjection">对象类型</typeparam>
        /// <returns></returns>
        public static List<TProjection> ProjectedAsCollection<TProjection>(this IEnumerable<Base.Entity> items)
            where TProjection : class,new()
        {
            return Mapper.Map<List<TProjection>>(items);
        }
    }
}
