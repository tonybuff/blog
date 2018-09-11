using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Models
{
    [Description("角色信息表")]
    [Table("UserRole")]
    public class UserRole : Base.Entity
    {
        public HashSet<Member> _Member;

        public UserRole()
        {
            Id = Component.Tools.CombHelper.NewComb();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置 角色类型
        /// </summary>
        public RoleType RoleType { get; set; }

        /// <summary>
        /// 获取或设置 角色类型的数值表示，用于数据库存储
        /// </summary>
        public int RoleTypeNum { get; set; }

        /// <summary>
        /// 拥有此角色用户组
        /// </summary>
        public virtual ICollection<Member> Members
        {
            get { return _Member ?? (_Member = new HashSet<Member>()); }
            set { _Member = new HashSet<Member>(value); }
        }

    }
}
