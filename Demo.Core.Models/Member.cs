using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo.Core.Models
{
    [Description("用户信息表")]
    [Table("Member")]
    public class Member : Base.Entity
    {

        public HashSet<LoginLog> _LoginLogs;




        /// <summary>
        /// 用户标示
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PassWord { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UnickName { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 盐值
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        public virtual UserRole UserRole { get; set; }

        /// <summary>
        /// 获取用户的登录日志
        /// </summary>
        public virtual ICollection<LoginLog> LoginLogs
        {
            get { return _LoginLogs ?? (_LoginLogs = new HashSet<LoginLog>()); }
            set { _LoginLogs = new HashSet<LoginLog>(value); }
        }

       
    }

}
