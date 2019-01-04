using System;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var resultlist = GetBySql("UserName,age");
            if (resultlist != null)
            {
                var s = resultlist.UserName;
            }
            
        }

        /// <summary>
        /// 传入需要查询的字段
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public dynamic GetBySql(string column)
        {
            dynamic current = null;
            //数据源
            var list = new List<User> { new User { UserName = "a", age = 11 } };
            //匿名查询字段
            var selectField = string.Format("new ({0})", string.Join(",", column));
            var resultlist= list.Where(f=>f.UserName =="b").Select(selectField);
            foreach (dynamic item in resultlist)
            {
                current = item;
                break;
            }
            return current;
        }
    }

    public class User
    {
        public string UserName { get; set; }
        public int age { get; set; }
    }
}
