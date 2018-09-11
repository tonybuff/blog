using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Models.Base;

namespace Demo.Component.Data
{
    public interface IUnitOfWorkContext:IUnitOfWork,IDisposable
    {
        /// <summary>
        /// 为指定的类型返回DbSet，以便数据库上下文中为实体执行CRUD操作
        /// </summary>
        /// <typeparam name="TEntity">应为其返回一个集的实体类型。</typeparam>
        /// <returns>返回一个指定类型的实体，即System.Data.Entity.DbSet 实例</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

        /// <summary>
        /// 注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">要注册的类型</typeparam>
        /// <param name="entity">要注册的对象</param>
        void RegisterNew<TEntity>(TEntity entity) where TEntity : Entity;

        /// <summary>
        /// 批量注册多个对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">要注册的类型</typeparam>
        /// <param name="entities">要注册的对象集合</param>
        void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;

        /// <summary>
        /// 注册一个修改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">要注册的类型</typeparam>
        /// <param name="entity">要注册的修改对象</param>
        void RegisterModified<TEntity>(TEntity entity) where TEntity : Entity;

        /// <summary>
        /// 注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">要注册的删除实体类型</typeparam>
        /// <param name="entity">要注册的删除对象</param>
        void RegisterDelete<TEntity>(TEntity entity) where TEntity : Entity;

        /// <summary>
        /// 批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">要注册的删除实体类型</typeparam>
        /// <param name="entities">要注册的删除对象集合</param>
        void RegisterDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;

        /// <summary>
        /// 批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">要注册删除的实体类型</typeparam>
        /// <param name="predicate">要删除的实体对应条件表达式</param>
        void RegisterDelete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity;
    }
}
