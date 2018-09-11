using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Data.DbContext;

namespace Demo.Core.Data.Initialize
{
    /// <summary>
    /// 数据库初始化类
    /// </summary>
    public static class DatabaseInitialize
    {
        /// <summary>
        /// 如果数据库不存在，则创建数据库
        /// </summary>
        public static void Init()
        {
            Database.SetInitializer(new InitData());
            //using (var db = new DemoDbContext())
            //{
            //    db.Database.Initialize(false);
            //}
        }


    }
}
