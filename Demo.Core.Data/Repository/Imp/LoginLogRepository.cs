using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Data;
using Demo.Core.Data.Base;
using Demo.Core.Data.DbContext;
using Demo.Core.Models;
using Microsoft.Practices.Unity;

namespace Demo.Core.Data.Repository.Imp
{
    /// <summary>
    /// 仓储操作实现——登录记录信息
    /// </summary>
    public class LoginLogRepository : EfRepositoryBase<LoginLog>, ILoginLogRepository
    {
        public LoginLogRepository(DemoRepositotyContext demoRepositoty)
            : base(demoRepositoty)
        {
        }
    }
}
