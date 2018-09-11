using System;
using System.ComponentModel.DataAnnotations;
using Demo.Component.Tools;


namespace Demo.Core.Models
{
    /// <summary>
    /// 用户登录日志实体
    /// </summary>
    public class LoginLog:Base.Entity
    {
        /// <summary>
        /// 初始化一个 登录记录实体类 的新实例
        /// </summary>
        public LoginLog()
        {
            Id = CombHelper.NewComb();
        }

        /// <summary>
        /// 获取或设置 登录记录编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 获取或设置 登录IP地址
        /// </summary>
        [Required]
        [StringLength(15)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 获取或设置 用户信息
        /// </summary>
        public virtual Member Member { get; set; }
    }
}
