using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Demo.Component.Tools;

namespace Demo.Site
{
    /// <summary>
    /// 用户登录帮助类
    /// （主要提供账户登录票证颁发，登录票证注销）
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        public static bool IsAuthenticated
        {
            get 
            {
                string authCookie = PublicHelper.GetCookie("blog");
                if (!string.IsNullOrWhiteSpace(authCookie))
                {
                    FormsAuthenticationTicket ticket=FormsAuthentication.Decrypt(authCookie);
                    if (ticket != null)
                    {
                        FormsIdentity identity=new FormsIdentity(ticket);
                        if (identity.IsAuthenticated)
                        {
                            if (Ticket[3] != "2.0")
                            {
                                CloseTicket();
                                HttpContext.Current.Session.RemoveAll();
                                return false;
                            }
                            return true;
                        }
                        HttpContext.Current.Session.RemoveAll();
                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session.Abandon();
                        return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 登录票证信息
        /// </summary>
        public static string[] Ticket
        {
            get
            {
                string authCookie = PublicHelper.GetCookie("blog");
                if (!string.IsNullOrEmpty(authCookie))
                {
                    FormsAuthenticationTicket tickets = FormsAuthentication.Decrypt(authCookie);
                    if (tickets != null)
                    {
                        FormsIdentity identity = new FormsIdentity(tickets);
                        if (identity.IsAuthenticated)
                        {
                            string str = HttpUtility.UrlDecode(tickets.Name);
                            if (str.IndexOf("##", System.StringComparison.Ordinal) < 0)
                            {
                                CloseTicket();
                                HttpContext.Current.Response.Redirect("http://www.blog.com/account/logon?returnUrl=" + HttpContext.Current.Request.Url);
                                return null;
                            }
                            return Regex.Split(str, "##");
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 注销登录票证
        /// </summary>
        public static void CloseTicket()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            PublicHelper.RemoveCookie("USERLOGINDATE");
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 颁发票证
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="Mail"></param>
        /// <param name="remberMe"></param>
        public static void OpenTicket(int UserId, string UserName, string Mail, bool remberMe)
        {
            
            FormsAuthentication.SetAuthCookie(HttpUtility.UrlEncode(string.Concat(new object[] { Mail, "##", UserId, "##", UserName, "##2.0" })), remberMe);
        }



    }

}
