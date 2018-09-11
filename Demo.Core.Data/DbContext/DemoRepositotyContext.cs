using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Data.Base;

namespace Demo.Core.Data.DbContext
{

    public class DemoRepositotyContext : UnitOfWorkBaseContext
    {
        private readonly DemoDbContext _dbContext;
        public DemoRepositotyContext(DemoDbContext _db) : base(_db)
        {
            _dbContext = _db;
        }
    }
}
