using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.Component.Tools;
using Demo.Component.Tools.Extensions;
using Demo.Core;
using Demo.Core.Data.Repository;
using Demo.Core.Imp;
using Demo.Core.Models;
using Demo.Core.Models.Extension;
using Demo.Site.Models;

namespace Demo.Site.Imp
{
    public class ConsumeRecordsSiteService : ConsumeRecordService,IConsumeRecordSiteContract
    {
        private readonly IConsumeRecordRepository _consumeRecordRepository;

        public ConsumeRecordsSiteService(IConsumeRecordRepository consumeRecordRepository)
            : base(consumeRecordRepository)
        {
            if(consumeRecordRepository==null)
                throw new ArgumentNullException("consumeRecordRepository");
            _consumeRecordRepository = consumeRecordRepository;
        }

        /// <summary>
        /// 新增消费记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public OperationResult Insert(ConsumeRecordModel record)
        {
            var model = new ConsumeRecord
                {
                    ConsumeType = record.ConsumeType,
                    Money = record.Money,
                    User = record.User
                };
            return base.AddNewConsumeRecords(model);
        }

        /// <summary>
        /// 分页查询消费记录
        /// </summary>
        /// <param name="consumeType"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public OperationResult GetConsumeRecordByPage(string consumeType, int page, int size, out int total)
        {
            Expression<Func<ConsumeRecord, bool>> fliter = f => f.Id > 0;
            if (!string.IsNullOrWhiteSpace(consumeType))
                fliter.And(f => f.ConsumeType == consumeType);
            var result = _consumeRecordRepository.GetPages(fliter, page, size, out total);
            if (result.Any())
                return new OperationResult(OperationResultType.Success, "查询成功", result.ToList().ProjectedAsCollection<ConsumeRecordModel>());
            return new OperationResult(OperationResultType.QueryNull,"查无数据");
        }
    }
}
