using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Tools;
using Demo.Core.Data;
using Demo.Core.Models;

namespace Demo.Core
{
    /// <summary>
    /// 账户服务接口
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <returns></returns>
        OperationResult Login(LoginInfo loginInfo);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        OperationResult GetUserList(int? uid,string uNiceName,int page,int size,out int total);
    }
}
