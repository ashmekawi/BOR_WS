using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BOR_WS.EXTFUN
{
    public class CRA00Context :  DbContext
    {
        public CRA00Context() : base("CRA00")
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