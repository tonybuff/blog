using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Tools;
using Demo.Core.Models;

namespace Demo.Core
{
    /// <summary>
    /// 消费记录服务接口
    /// </summary>
    public interface IConsumeRecordService
    {
        OperationResult AddNewConsumeRecords(ConsumeRecord record);
    }
}
