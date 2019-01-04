using System;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using Demo.Component.Tools;
using Demo.Core.Models;
using Demo.Core.Data.Repository;
using Demo.Component.Tools.Extensions;
namespace Demo.Core.Imp
{
    public class AccountService : CoreServiceBase, IAccountService
    {
        #region 依赖注入
        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        protected readonly IMemberRepository MemberRepository;

        /// <summary>
        ///  获取或设置 用户登录日志数据访问对象
        /// </summary>
        protected readonly ILoginLogRepository LoginLogRepository;

        protected AccountService(IMemberRepository memberRepository, ILoginLogRepository loginLogRepository)
        {
            if (memberRepository == null)
                throw new ArgumentNullException("memberRepository");
            if (loginLogRepository == null)
                throw new ArgumentNullException("loginLogRepository");
            MemberRepository = memberRepository;
            LoginLogRepository = loginLogRepository;
        }

        #endregion

        /// <summary>
        /// 判断用户登录
        /// </summary>
        /// <param name="loginInfo">用户登录信息</param>
        /// <returns>操作结果</returns>
        public OperationResult Login(LoginInfo loginInfo)
        {

            var member =
                MemberRepository.CheckLogin(
                    m => m.Email == loginInfo.Access || m.UserName == loginInfo.Access);
            if (member == null)
                return new OperationResult(OperationResultType.QueryNull, "指定账号的用户不存在。");

            if (!PublicHelper.MD5(loginInfo.Password + member.Salt).EndsWith(member.PassWord, true, null))
                return new OperationResult(OperationResultType.Error, "密码错误");
            var loginLog = new LoginLog { IpAddress = loginInfo.IpAddress, Member = member };
            LoginLogRepository.Insert(loginLog);
            return new OperationResult(OperationResultType.Success, "登录成功", member);
        }

        /// <summary>
        /// 根据用户名或者用户Id查询用户数据列表
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="uNiceName">用户昵称</param>
        /// <param name="page">当前页</param>
        /// <param name="size">显示条数</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
        public OperationResult GetUserList(int? uid, string uNiceName, int page, int size, out int total)
        {
            Expression<Func<Member, bool>> fliter = f => f.Id > 0;
            if (uid.HasValue)
                fliter.And(f => f.Id == uid);
            if (!string.IsNullOrWhiteSpace(uNiceName))
                fliter.And(f => f.UnickName.Contains(uNiceName));
            var result = MemberRepository.GetPages(fliter, page, size, out total).ToList();
            return result.Any() ? new OperationResult(OperationResultType.Success, "查询成功", result) : new OperationResult(OperationResultType.QueryNull, "没有数据");
        }
    }
}
