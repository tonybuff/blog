using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Data.Base;
using Demo.Core.Data.DbContext;
using Demo.Core.Models;
using Demo.Component.Data;
using Microsoft.Practices.Unity;

namespace Demo.Core.Data.Repository.Imp
{
    /// <summary>
    /// 仓储操作实现，人员管理
    /// </summary>
    public class MemberRepository:EfRepositoryBase<Member>,IMemberRepository
    {
        public MemberRepository(DemoRepositotyContext demoRepositoty)
            : base(demoRepositoty)
        {
        }

        public Member CheckLogin(Expression<Func<Member,bool>> expression)
        {
            return base.Entities.FirstOrDefault(expression);
        }

        public Member GetMemberByUserName(string name)
        {
            return base.Entities.FirstOrDefault(f => f.UserName == name);
        }
    }
}
