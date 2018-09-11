using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Models
{
    [Description("消费记录")]
    [Table("ConsumeRecord")]
    public class ConsumeRecord:Base.Entity
    {

        public int Id { get; set; }

        /// <summary>
        /// 消费类型
        /// </summary>
        public string ConsumeType { get; set;}

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 消费人
        /// </summary>
        public string User { get; set; }
    }
}
