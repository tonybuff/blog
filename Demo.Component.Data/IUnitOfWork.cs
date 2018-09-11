using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Component.Data
{
    /// <summary>
    /// 业务单元操作接口
    /// </summary>
    public interface IUnitOfWork
    {

        #region 属性
        /// <summary>
        /// 获取当前单元操作是否提交
        /// </summary>
        bool IsCommited { get; } 
        #endregion

        #region 方法
        /// <summary>
        /// 提交单元操作结果
        /// </summary>
        /// <returns></returns>
        int Commit();

       /// <summary>
       /// 当前单元操作，事务回滚到未提交状态
       /// </summary>
        void RollBack();

        #endregion

    }
}
