using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BOR_WS.EXTFUN
{
    public class CRRB_ServiceContext: DbContext
    {
        public CRRB_ServiceContext() : base("CRRB_ServiceContext")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 0;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}