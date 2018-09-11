using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Tools;
using Demo.Core;
using Demo.Core.Models;
using Demo.Site.Models;

namespace Demo.Site
{
    public interface IConsumeRecordSiteContract:IConsumeRecordService
    {
        OperationResult Insert(ConsumeRecordModel record);

        OperationResult GetConsumeRecordByPage(string consumeType, int page, int size, out int total);
    }
}
