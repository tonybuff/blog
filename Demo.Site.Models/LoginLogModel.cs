using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Site.Models
{
    public class LoginLogModel
    {

        /// <summary>
        /// 获取或设置 登录记录编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 登录IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
