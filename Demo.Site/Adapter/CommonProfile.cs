using AutoMapper;
using Demo.Core.Models;
using Demo.Site.Models;

namespace Demo.Site.Adapter
{
    /// <summary>
    /// 配置对象映射
    /// </summary>
    public class CommonProfile:Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<LoginLog, LoginLogModel>();
            Mapper.CreateMap<ConsumeRecord, ConsumeRecordModel>();

        }
    }
}
