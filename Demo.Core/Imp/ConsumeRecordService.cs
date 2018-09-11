using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Tools;
using Demo.Component.Tools.Extensions;
using Demo.Core.Data.Repository;
using Demo.Core.Models;

namespace Demo.Core.Imp
{
    public class ConsumeRecordService : CoreServiceBase, IConsumeRecordService
    {
        private readonly IConsumeRecordRepository _consumeRecordRepository;

        public ConsumeRecordService(IConsumeRecordRepository consumeRecordRepository)
        {
            if (consumeRecordRepository == null)
                throw new ArgumentNullException("consumeRecordRepository");
            _consumeRecordRepository = consumeRecordRepository;
        }

        /// <summary>
        /// 新增消费记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public OperationResult AddNewConsumeRecords(ConsumeRecord record)
        {
            UnitOfWork.Commit();
            if (string.IsNullOrWhiteSpace(record.ConsumeType))
                return new OperationResult(OperationResultType.NoChanged, "消费记录类型不能为空");
            if (record.Money == 0)
                return new OperationResult(OperationResultType.NoChanged, "消费金额不能为零");
            _consumeRecordRepository.Insert(record);
            return new OperationResult(OperationResultType.Success, "新增成功", record);
        }
    }
}
