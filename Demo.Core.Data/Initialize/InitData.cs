using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Demo.Component.Tools;
using Demo.Core.Data.DbContext;
using Demo.Core.Models;

namespace Demo.Core.Data.Initialize
{
    public class InitData:CreateDatabaseIfNotExists<DemoDbContext>
    {
        protected override void Seed(DemoDbContext context)
        {
            var userRole = new UserRole
            {
                RoleType = RoleType.User,
                RoleTypeNum = (int)RoleType.User,
                Description = "普通账号",
                Name = "普通账号",

            };
            var adminRole = new UserRole
            {
                RoleType = RoleType.Admin,
                RoleTypeNum = (int)RoleType.Admin,
                Description = "拥有管理所有资源的权限",
                Name = "管理员账号",

            };
            context.UserRoles.Add(userRole);
            context.UserRoles.Add(adminRole);
            context.SaveChanges();
            var salt = PublicHelper.CreateVerifyCode(4);
            var password = PublicHelper.MD5("123456" + salt);
            var member = new Member
                {
                    UserName = "admin",
                    UnickName = "管理员",
                    Email = "471447434@qq.com",
                    PassWord = password,
                    Salt=salt,
                    UserRole = adminRole
                };
            context.Members.Add(member);
            context.SaveChanges();
           
           
        }

    }
}
