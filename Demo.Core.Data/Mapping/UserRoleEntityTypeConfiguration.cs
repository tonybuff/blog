﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Models;

namespace Demo.Core.Data.Mapping
{
    class UserRoleEntityTypeConfiguration:EntityTypeConfiguration<UserRole>
    {
        public UserRoleEntityTypeConfiguration()
        {
            HasKey(o => o.Id);

            

         
        }

    }
}
