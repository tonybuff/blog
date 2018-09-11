using System.Collections.Generic;
using Demo.Core;
using Demo.Component.Tools;
using Demo.Core.Models;
using Demo.Site.Models;

namespace Demo.Site
{
    public interface IAccountSiteContract:IAccountService
    {

        OperationResult Login(LoginModel info);

        void Logout();

        OperationResult GetUserLoginLogList(string name);

        OperationResult GetUserLoginLogPages(string userName, int page, int size, out int total);
    }
}
