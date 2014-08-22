using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troy.Data.DataContext;
using Troy.Model.Branch;
using Troy.Model.Purchase;

namespace Troy.Data.Repository
{
    public class PurchaseRepository : BaseRepository, IPurchaseRepository
    {
        private PurchaseContext purchaseContext = new PurchaseContext();

        public List<PurchaseQuotation> GetAllQuotation()
        {
            List<PurchaseQuotation> qList = new List<PurchaseQuotation>();

            qList = (from p in purchaseContext.PurchaseQuotation
                     select p).ToList();

            return qList;
        }

        public List<PurchaseQuotation> GetFilterQuotation(string searchColumn, string searchString, Guid userId)
        {
            List<PurchaseQuotation> qList = new List<PurchaseQuotation>();

            if (searchColumn == null)
            {
                searchColumn = "";
                searchString = "";
            }

            purchaseContext.Database.Initialize(force: false);

            var cmd = purchaseContext.Database.Connection.CreateCommand();
            cmd.CommandText = "[dbo].[USP_GetPurchaseQuotation]";

            var searchParam = new SqlParameter();
            searchParam.ParameterName = "@SearchColumn";
            searchParam.SqlDbType = SqlDbType.NVarChar;
            searchParam.SqlValue = searchColumn;
            //searchParam.ParameterDirection = ParameterDirection.Output;

            var stringParam = new SqlParameter();
            stringParam.ParameterName = "@SearchString";
            stringParam.SqlDbType = SqlDbType.NVarChar;
            stringParam.SqlValue = searchString;
            //stringParam.ParameterDirection = ParameterDirection.Output;

            //cmd.Parameters.Add(searchParam);
            //cmd.Parameters.Add(stringParam);

            cmd.Parameters.Add(new SqlParameter("@SearchColumn", searchColumn));
            cmd.Parameters.Add(new SqlParameter("@SearchString", searchString));

            try
            {
                purchaseContext.Database.Connection.Open();
                // Run the sproc  
                var reader = cmd.ExecuteReader();

                var result = ((IObjectContextAdapter)purchaseContext)
                    .ObjectContext
                    .Translate<PurchaseQuotation>(reader, "PurchaseQuotation", MergeOption.AppendOnly);


                foreach (var item in result)
                {
                    PurchaseQuotation model = new PurchaseQuotation()
                    {
                        Purchase_Quote_Id = item.Purchase_Quote_Id,
                        Created_Branc_Id = item.Created_Branc_Id,
                        Created_Date = item.Created_Date,
                        Created_User_Id = item.Created_User_Id,
                        Creating_Branch = item.Creating_Branch,
                        Discount = item.Discount,
                        Fright = item.Fright,
                        Loading = item.Loading,
                        Modified_Branch_Id = item.Modified_Branch_Id,
                        Modified_Date = item.Modified_Date,
                        Modified_User_Id = item.Modified_User_Id,
                        Posting_Date = item.Posting_Date,
                        Quotation_Status = item.Quotation_Status,
                        Reference_Number = item.Reference_Number,
                        Remarks = item.Remarks,
                        Required_Date = item.Required_Date,
                        Ship_To = item.Ship_To,
                        Valid_Date = item.Valid_Date,
                        Vendor = item.Vendor
                    };

                    qList.Add(model);
                }
            }
            finally
            {
                purchaseContext.Database.Connection.Close();
            }

            return qList;
        }

        public List<PurchaseQuotation> GetAllQuotationByFilter()
        {
            List<PurchaseQuotation> qList = new List<PurchaseQuotation>();

            return qList;
        }

        public PurchaseQuotation FindOneQuotationById(int qId)
        {
            return (from p in purchaseContext.PurchaseQuotation
                    where p.Purchase_Quote_Id == qId
                    select p).FirstOrDefault();
        }

        public PurchaseQuotationItem FindOneQuotationItemById(int qId)
        {
            return (from p in purchaseContext.PurchaseQuotationItem
                    where p.Purchase_Quote_Id == qId
                    select p).FirstOrDefault();

        }

        public List<BranchList> GetAddressList()
        {
            var item = (from a in purchaseContext.Branch
                        select new BranchList
                        {
                            BranchName = a.Branch_Name,
                            BranchId = a.Branch_Id
                        }).ToList();

            return item;
        }

        public bool AddNewQuotation(PurchaseQuotation Quotation, PurchaseQuotationItem QuotationItem)
        {
            try
            {
                purchaseContext.PurchaseQuotation.Add(Quotation);

                purchaseContext.PurchaseQuotationItem.Add(QuotationItem);

                purchaseContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
