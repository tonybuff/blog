using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Data;
using Demo.Core.Models;

namespace Demo.Core.Data.Repository
{
    /// <summary>
    /// 仓储操作接口，用户登录日志记录
    /// </summary>
    public interface ILoginLogRepository : IRepository<LoginLog>
    {
    }
}
