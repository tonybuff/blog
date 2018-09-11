using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Data;
using Demo.Component.Tools;
using Demo.Core.Data.DbContext;
using Demo.Component.Tools.Exception;
using Demo.Core.Models.Base;

namespace Demo.Core.Data.Base
{
    /// <summary>
    /// EntityFramework仓储操作数据访问基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EfRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

        #region 属性
        private readonly DemoRepositotyContext _efContext;

        protected EfRepositoryBase(DemoRepositotyContext db)
        {
            if (db == null)
                throw new Exception("demoRepository为空");
            _efContext = db;

        }

        public DemoRepositotyContext Db
        {
            get { return _efContext; }
        }

        /// <summary>
        /// 获取当前上下文的实体
        /// </summary>
        public IQueryable<TEntity> Entities
        {
            get { return _efContext.Set<TEntity>(); }
        }
        #endregion

        /// <summary>
        /// 执行通用新增操作
        /// </summary>
        /// <param name="entity">要插入的实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns>返回受影响的行数</returns>
        public int Insert(TEntity entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            _efContext.RegisterNew(entity);
            return isSave ? _efContext.Commit() : 0;
        }

        /// <summary>
        /// 执行通用批量新增实体集合
        /// </summary>
        /// <param name="entities">要插入的新增实体集</param>
        /// <param name="isSave">是否保存</param>
        /// <returns>返回受影响的行数</returns>
        public int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            PublicHelper.CheckArgument(entities, "entity");
            _efContext.RegisterNew(entities);
            return isSave ? _efContext.Commit() : 0;
        }

        /// <summary>
        /// 执行通用删除操作
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="isSave">是否保存</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(object id, bool isSave = true)
        {
            PublicHelper.CheckArgument(id, "Id");
            var entity = _efContext.Set<TEntity>().Find(id);
            return isSave ? Delete(entity) : 0;
        }

        /// <summary>
        /// 执行通用删除操作
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="isSave">是否保存</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(TEntity entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            _efContext.RegisterDelete(entity);
            return isSave ? _efContext.Commit() : 0;
        }

        /// <summary>
        /// 执行通用批量删除操作
        /// </summary>
        /// <param name="entities">要删除的实体集</param>
        /// <param name="isSave">是否执行保存</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            _efContext.RegisterDelete(entities);
            return isSave ? _efContext.Commit() : 0;
        }

        /// <summary>
        /// 执行批量删除指定条件表达式的实体
        /// </summary>
        /// <param name="predicate">要删除的指定表达式</param>
        /// <param name="isSave">是否保存</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            PublicHelper.CheckArgument(predicate,"predicate");
            _efContext.RegisterDelete(predicate);
            return isSave ? _efContext.Commit() : 0;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        /// <param name="isSave">是否执行保存</param>
        /// <returns>操作影响的行数</returns>
        public int Update(TEntity entity, bool isSave = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            _efContext.RegisterModified(entity);
            return isSave ? _efContext.Commit() : 0;
        }

        /// <summary>
        /// 查找指定主键的实体记录
        /// </summary>
        /// <param name="key">指定主键</param>
        /// <returns>符合编号的记录，不存在返回null</returns>
        public TEntity GetByKey(object key)
        {
            PublicHelper.CheckArgument(key, "key");
            return _efContext.Set<TEntity>().Find(key);
        }

        /// <summary>
        /// 分页查询实体记录
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="size">显示条数</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPages(int page, int size, out int total)
        {
            PublicHelper.CheckArgument(page,"page");
            PublicHelper.CheckArgument(size, "size");
            total = _efContext.Set<TEntity>().Count();
            return _efContext.Set<TEntity>().Skip((page - 1)*size).Take(size);
        }

        /// <summary>
        /// 根据条件分页查询实体记录
        /// </summary>
        /// <param name="prodicate">查询条件表达式</param>
        /// <param name="page">当前页</param>
        /// <param name="size">显示条数</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPages(Expression<Func<TEntity, bool>> prodicate, int page, int size,
                                             out int total)
        {
            PublicHelper.CheckArgument(page, "page");
            PublicHelper.CheckArgument(size, "size");
            PublicHelper.CheckArgument(prodicate, "prodicate");
            total = _efContext.Set<TEntity>().Count();
            return _efContext.Set<TEntity>().Where(prodicate).OrderByDescending(f=>f.CreateTime).Skip((page - 1) * size).Take(size);
        }


    }
}
