using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Data.Base;
using Demo.Core.Data.DbContext;
using Demo.Core.Models;

namespace Demo.Core.Data.Repository.Imp
{
    public class ConsumeRecordRepository:EfRepositoryBase<ConsumeRecord>,IConsumeRecordRepository
    {
        public ConsumeRecordRepository(DemoRepositotyContext db) : base(db)
        {
        }
    }
}
