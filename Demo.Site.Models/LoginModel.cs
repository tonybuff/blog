using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Demo.Site.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 账号（可邮箱、用户名）
        /// </summary>
        [Required]
        [Display(Name = "账号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string PassWord { get; set; }

        /// <summary>
        /// 记住密码
        /// </summary>
        [Display(Name = "记住登录")]
        public bool IsRememberLogin { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
