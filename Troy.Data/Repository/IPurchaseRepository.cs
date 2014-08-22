using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troy.Model.Branch;
using Troy.Model.Purchase;

namespace Troy.Data.Repository
{
    public interface IPurchaseRepository
    {
        List<PurchaseQuotation> GetAllQuotation();

        List<PurchaseQuotation> GetFilterQuotation(string searchColumn, string searchString, Guid userId);

        PurchaseQuotation FindOneQuotationById(int qId);

        PurchaseQuotationItem FindOneQuotationItemById(int qId);

        List<BranchList> GetAddressList();

        bool AddNewQuotation(PurchaseQuotation Quotation, PurchaseQuotationItem QuotationItem);

    }
}
