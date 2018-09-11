using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Models;
using Demo.Component.Data;

namespace Demo.Core.Data.Repository
{
    /// <summary>
    /// 仓储操作接口---用户信息
    /// </summary>
    public interface IMemberRepository:IRepository<Member>
    {
        Member CheckLogin(Expression<Func<Member, bool>> expression);
        Member GetMemberByUserName(string name);
    }
}
