using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Demo.Component.Tools.Exception;
using Demo.Component.Tools.Extensions;

namespace Demo.Component.Tools
{
    /// <summary>
    /// 公共方法辅助类
    /// </summary>
    public static class PublicHelper
    {
        /// <summary>
        /// 检验参数合法性，数值类型不能小于0，引用类型不能为null，否则抛出相应异常
        /// </summary>
        /// <param name="arg"> 待检参数 </param>
        /// <param name="argName"> 待检参数名称 </param>
        /// <param name="canZero"> 数值类型是否可以等于0 </param>
        /// <exception cref="ComponentException" />
        public static void CheckArgument(object arg, string argName, bool canZero = false)
        {
            if (arg == null)
            {
                var exception = new ArgumentNullException(argName);
                throw ThrowComponentException(string.Format("参数{0}为空引发异常", argName), exception);
            }
            Type type = arg.GetType();
            if (type.IsValueType && type.IsNumeric())
            {
                bool flag = !canZero ? arg.CastTo(0.0) <= 0.0 : arg.CastTo(0.0) < 0.0;
                if (flag)
                {
                    ArgumentOutOfRangeException e = new ArgumentOutOfRangeException(argName);
                    throw ThrowComponentException(string.Format("参数 {0} 不在有效范围内引发异常。具体信息请查看系统日志。", argName), e);
                }
            }
            if (type == typeof(Guid) && (Guid)arg == Guid.Empty)
            {
                ArgumentNullException e = new ArgumentNullException(argName);
                throw ThrowComponentException(string.Format("参数{0}为空Guid引发异常。", argName), e);
            }
        }

        /// <summary>
        /// 向调用层抛组件异常
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static ComponentException ThrowComponentException(string msg, System.Exception exception = null)
        {
            if (!string.IsNullOrEmpty(msg) && exception != null)
            {
                msg = exception.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知组件异常，详情请查看日志";
            }
            return exception == null
                       ? new ComponentException(string.Format("组件异常{0}", msg))
                       : new ComponentException(string.Format("组件异常{0}", msg), exception);
        }

        /// <summary>
        ///向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static DataAccessException ThrowDataAccessException(string msg, System.Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知数据访问层异常，详情请查看日志信息。";
            }
            return e == null
                ? new DataAccessException(string.Format("数据访问层异常：{0}", msg))
                : new DataAccessException(string.Format("数据访问层异常：{0}", msg), e);
        }

        /// <summary>
        ///向调用层抛出数据访问层异常
        /// </summary>
        /// <param name="msg"> 自定义异常消息 </param>
        /// <param name="e"> 实际引发异常的异常实例 </param>
        public static BusinessException ThrowBusinessException(string msg, System.Exception e = null)
        {
            if (string.IsNullOrEmpty(msg) && e != null)
            {
                msg = e.Message;
            }
            else if (string.IsNullOrEmpty(msg))
            {
                msg = "未知业务逻辑层异常，详情请查看日志信息。";
            }
            return e == null ? new BusinessException(string.Format("业务逻辑层异常：{0}", msg)) : new BusinessException(string.Format("业务逻辑层异常：{0}", msg), e);
        }

        /// <summary>
        /// 创建验证码信息
        /// </summary>
        /// <param name="codeLen"></param>
        /// <returns></returns>
        public static string CreateVerifyCode(int codeLen)
        {
            string[] arr = "0,1,2,3,4,5,6,7,8,9".Split(new char[] { ',' });
            string code = "";
            int randValue = -1;
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code = code + arr[randValue];
            }
            return code;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret = ret + b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// 移出cookie值
        /// </summary>
        /// <param name="strName"></param>
        public static void RemoveCookie(string strName)
        {
            HttpContext.Current.Request.Cookies.Remove(strName);
        }

        //public static string Hash(string password, string salt)
        //{
        //    // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
        //    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password,
        //        salt: Convert.FromBase64String(salt),
        //        prf: KeyDerivationPrf.HMACSHA1,
        //        iterationCount: 10000,
        //        numBytesRequested: 256 / 8));

        //    return hashed;
        //}

        public static string GetSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }


    }


}
