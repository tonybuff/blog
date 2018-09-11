using System;
using System.Collections.Generic;
using Demo.Component.Tools;
using Demo.Core.Data.Repository;
using Demo.Core.Imp;
using Demo.Core.Models;
using Demo.Site.Models;
using System.Web;
using System.Web.Security;
using Demo.Core.Models.Extension;

namespace Demo.Site.Imp
{
    /// <summary>
    /// 账户管理业务逻辑类
    /// </summary>
    public class AccountSiteService : AccountService, IAccountSiteContract
    {
        #region 依赖注入
        private readonly IMemberRepository _memberRepository;

        private readonly ILoginLogRepository _loginLogRepository;

        public AccountSiteService(IMemberRepository memberRepository, ILoginLogRepository loginLogRepository)
            : base(memberRepository, loginLogRepository)
        {
            if (memberRepository == null)
                throw new ArgumentNullException("memberRepository");
            if (loginLogRepository == null)
                throw new ArgumentNullException("loginLogRepository");
            _memberRepository = memberRepository;
            _loginLogRepository = loginLogRepository;
        }

        #endregion

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">登录信息</param>
        /// <returns></returns>
        public OperationResult Login(LoginModel model)
        {
            PublicHelper.CheckArgument(model, "loginInfo");
            var loginInfo = new LoginInfo
            {
                Access = model.Account,
                Password = model.PassWord,
                IpAddress = HttpContext.Current.Request.UserHostAddress
            };
            //验证登录信息是否正确
            OperationResult result = base.Login(loginInfo);
            if (result.ResultType == OperationResultType.Success)
            {
                var member = (Member)result.AppendData;
                
                //颁发票证
                UserHelper.OpenTicket(member.Id,member.UserName,member.Email,model.IsRememberLogin);

            }
            return result;
        }


        /// <summary>
        /// 退出登录
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 根据用户名查询用户登录日志
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public OperationResult GetUserLoginLogList(string name)
        {
            var member = _memberRepository.GetMemberByUserName(name);
            if (member != null)
                return new OperationResult(OperationResultType.Success, "查询成功", member.LoginLogs.ProjectedAsCollection<LoginLogModel>());
            return new OperationResult(OperationResultType.QueryNull, "记录未找到");
        }

        public OperationResult GetUserLoginLogPages(string userName,int page, int size, out int total)
        {
            var loginLogs = _loginLogRepository.GetPages(f => f.Member.UserName == userName, page, size, out total);
            if (loginLogs != null)
                return new OperationResult(OperationResultType.Success, "查询成功", loginLogs.ProjectedAsCollection<LoginLogModel>()); ;
            return new OperationResult(OperationResultType.QueryNull, "记录未找到");
        }

    }
}
