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
    /// <summary>
    /// 角色类型枚举类
    /// </summary>
    [Description("角色类型")]
    public enum RoleType
    {
        [Description("用户角色")]
        User = 0,

        [Description("管理角色")]
        Admin = 1
    }
}
