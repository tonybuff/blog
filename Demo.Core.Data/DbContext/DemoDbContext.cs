using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Data.Base;
using Demo.Core.Models;
using Demo.Core.Data.Mapping;

namespace Demo.Core.Data.DbContext
{
    /// <summary>
    /// EF数据访问上下文
    /// </summary>
    public class DemoDbContext : System.Data.Entity.DbContext
    {

        public DemoDbContext()
            : base("DemoDbContext")
        {
            
        }

        #region 数据库实体成员

        /// <summary>
        /// 成员信息表
        /// </summary>
        public IDbSet<Member> Members { get; set; }

        /// <summary>
        /// 成员登录信息
        /// </summary>
        public IDbSet<LoginLog> LoginLogs { get; set; }

        /// <summary>
        /// 用户权限信息表
        /// </summary>
        public IDbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 消费记录
        /// </summary>
        public IDbSet<ConsumeRecord> ConsumeRecords { get; set; }

        #endregion

        /// <summary>
        /// 数据库创建实体表时执行
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserRoleEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
