using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Troy.Model.Branch;
using Troy.Model.Purchase;

namespace Troy.Web.Models
{
    public class PurchaseViewModels
    {
        public PurchaseQuotation PurchaseQuotation { get; set; }

        public PurchaseQuotationItem PurchaseQuotationItem { get; set; }

        public List<PurchaseQuotation> PurchaseQuotationList { get; set; }

        //public Status QuotationStatus { get; set; }

        public IEnumerable<SelectListItem> QuotationStatus { get; set; }

        public List<BranchList> BranchList { get; set; }

        public string SearchQuery { get; set; }

        public string SearchColumn { get; set; }

    }
}