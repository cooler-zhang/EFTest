using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFTest.Api
{
    public class MigrationDbContext : MyDbContext
    {
        public MigrationDbContext()
            :base("MyDbContext4")
        {
        }
    }
}