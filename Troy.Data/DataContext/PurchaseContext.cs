using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troy.Model.Branch;
using Troy.Model.Purchase;

namespace Troy.Data.DataContext
{
    public class PurchaseContext : DbContext
    {
        public PurchaseContext()
            : base("DefaultConnection")
        { }

        public DbSet<PurchaseQuotation> PurchaseQuotation { get; set; }

        public DbSet<PurchaseQuotationItem> PurchaseQuotationItem { get; set; }

        public DbSet<Branch> Branch { get; set; }

    }
}
