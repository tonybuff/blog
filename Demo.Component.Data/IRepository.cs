using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Models.Base;

namespace Demo.Component.Data
{
    /// <summary>
    /// 定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public interface IRepository<TEntity>
    {
        #region 属性

        /// <summary>
        /// 获取当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; } 

        #endregion

        #region 方法

        /// <summary>
        /// 插入实体记录
        /// </summary>
        /// <param name="entity">插入的实体对象</param>
        /// <param name="isSave">是否保存</param>
        /// <returns>返回受影响的行数</returns>
        int Insert(TEntity entity, bool isSave=true);


        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(object id, bool isSave = true);

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(TEntity entity, bool isSave = true);

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities, bool isSave = true);

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true);

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        int Update(TEntity entity, bool isSave = true);

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        TEntity GetByKey(object key);

        /// <summary>
        /// 分页查询实体记录
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="size">显示条数</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPages(int page, int size, out int total);

        /// <summary>
        /// 根据条件分页查询实体记录
        /// </summary>
        /// <param name="prodicate">查询条件表达式</param>
        /// <param name="page">当前页</param>
        /// <param name="size">显示条数</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPages(Expression<Func<TEntity,bool>> prodicate,int page, int size, out int total);
        #endregion
    }
}
