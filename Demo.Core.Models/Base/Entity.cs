using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Core.Models.Base
{
    /// <summary>
    /// 可持久到数据库的领域模型的基类。
    /// </summary>
    [Serializable]
     public abstract class Entity
    {
         /// <summary>
         /// 数据实体基类
         /// </summary>
         protected Entity()
         {
             CreateTime = DateTime.Now;
             IsDelete = false;
         }

         /// <summary>
         /// 创建时间
         /// </summary>
         [DataType(DataType.DateTime)]
         public DateTime CreateTime { get; set; }

         /// <summary>
         /// 获取或设置  获取或设置是否禁用，逻辑上的删除，非物理删除
         /// </summary>
         public bool IsDelete { get; set; }

         /// <summary>
         /// 获取或设置 版本控制标示，用于处理并发
         /// </summary>
         [ConcurrencyCheck]
         [Timestamp]
         public byte[] TimeSpan { get; set; }
    }
}
