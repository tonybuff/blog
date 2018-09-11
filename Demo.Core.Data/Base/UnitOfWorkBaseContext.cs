using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Data;
using Demo.Core.Data.DbContext;
using Demo.Core.Models.Base;

namespace Demo.Core.Data.Base
{/// <summary>
    /// 单元操作实现基类
    /// </summary>
    public abstract class UnitOfWorkBaseContext : IUnitOfWorkContext
    {

        /// <summary>
        /// 获取当前数据访问的上下文对象
        /// </summary>
        private readonly DemoDbContext _dbContext;

        protected UnitOfWorkBaseContext(DemoDbContext db)
        {
            _dbContext = db;
        }


        /// <summary>
        /// 获取 当前单元操作是否已被提交
        /// </summary>
        public bool IsCommited { get; private set; }

        /// <summary>
        /// 提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (IsCommited)
            {
                return 0;
            }
            try
            {
                int result = _dbContext.SaveChanges();
                IsCommited = true;
                return result;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException)
                {
                    throw;
                }
                throw;
            }
        }

        /// <summary>
        /// 把当前单元操作回滚成未提交状态
        /// </summary>
        public void RollBack()
        {
            IsCommited = false;
        }

        /// <summary>
        /// 提交当前单元操作，并释放当前数据库上下文对象
        /// </summary>
        public void Dispose()
        {
            if (!IsCommited)
                Commit();
            _dbContext.Dispose();
        }

        /// <summary>
        /// 为指定的类型返回 System.Data.Entity.DbSet，这将允许对上下文中的给定实体执行 CRUD 操作
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>指定类型的DbSet实例</returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要注册的对象</param>
        public void RegisterNew<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbContext.Entry(entity).State = EntityState.Added;
            IsCommited = false;
        }

        /// <summary>
        /// 批量注册新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entities">要注册的对象集合</param>
        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            try
            {
                _dbContext.Configuration.AutoDetectChangesEnabled = false;
                foreach (var entity in entities)
                {
                    RegisterNew(entity);
                }

            }
            finally
            {
                _dbContext.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// 注册一个新的修改对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要注册的修改对象</param>
        public void RegisterModified<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbContext.Entry(entity).State = EntityState.Modified;
            IsCommited = false;
        }

        /// <summary>
        /// 注册一个要删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">要注册的删除的对象</param>
        public void RegisterDelete<TEntity>(TEntity entity) where TEntity : Entity
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            IsCommited = false;
        }

        /// <summary>
        /// 批量注册删除对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entities">要注册的删除对象集合</param>
        public void RegisterDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            try
            {
                _dbContext.Configuration.AutoDetectChangesEnabled = false;
                foreach (var entity in entities)
                {
                    RegisterDelete(entity);
                }
            }
            finally
            {
                _dbContext.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        /// 批量注册删除对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="predicate">要删除的实体对应条件表达式</param>
        public void RegisterDelete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity
        {
            var entities = _dbContext.Set<TEntity>().Where(predicate.Compile());
            if(entities.Any())
              RegisterDelete(entities);

        }
    }
}
