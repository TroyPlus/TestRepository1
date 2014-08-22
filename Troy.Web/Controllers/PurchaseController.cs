using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Troy.Data.Repository;
using Troy.Model;
using Troy.Web.Models;

namespace Troy.Web.Controllers
{
    public class PurchaseController : BaseController
    {

        private readonly IPurchaseRepository purchaseDb;

        private readonly IManufactureRepository manufactureDb;

        //inject dependency
        public PurchaseController(IPurchaseRepository prepository, IManufactureRepository mrepository)
        {
            this.purchaseDb = prepository;
            this.manufactureDb = mrepository;
        }

        // GET: Purchase
        public ActionResult Index(string searchColumn, string searchQuery)
        {
            var qList = purchaseDb.GetFilterQuotation(searchColumn, searchQuery, Guid.Empty);   //GetUserId();

            PurchaseViewModels model = new PurchaseViewModels();

            model.PurchaseQuotationList = qList;

            var branchlist = purchaseDb.GetAddressList().ToList();

            model.BranchList = branchlist;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string submitButton, PurchaseViewModels model, HttpPostedFileBase file)
        {
            if (submitButton == "Save")
            {
                model.PurchaseQuotation.Created_Branc_Id = 1;
                model.PurchaseQuotation.Created_Date = DateTime.Now;
                model.PurchaseQuotation.Created_User_Id = 1;  //GetUserId()
                model.PurchaseQuotation.Creating_Branch = 2;  //GetBranch 
                model.PurchaseQuotation.Modified_User_Id = 1;
                model.PurchaseQuotation.Modified_Date = DateTime.Now;
                model.PurchaseQuotation.Modified_Branch_Id = 1;

                model.PurchaseQuotationItem.Created_Branc_Id = 1;
                model.PurchaseQuotationItem.Created_Date = DateTime.Now;
                model.PurchaseQuotationItem.Created_User_Id = 1;  //GetUserId()
                model.PurchaseQuotationItem.Modified_Branch_Id = 1;
                model.PurchaseQuotationItem.Modified_Date = DateTime.Now;
                model.PurchaseQuotationItem.Modified_User_Id = 1;
                model.PurchaseQuotationItem.Quoted_qty = 10; //GetQuantity()
                model.PurchaseQuotationItem.Quoted_date = DateTime.Now.AddDays(2);

                if (purchaseDb.AddNewQuotation(model.PurchaseQuotation, model.PurchaseQuotationItem))
                {
                    return RedirectToAction("Index", "Purchase");
                }
                else
                {
                    ModelState.AddModelError("", "Quotation Not Saved");
                }
            }
            else if (submitButton == "Search")
            {
                return RedirectToAction("Index", "Purchase", new { model.SearchColumn, model.SearchQuery });
            }

            if (Convert.ToString(Request.Files["FileUpload"]).Length > 0)
            {
                try
                {

                    string fileExtension = System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                    string fileName = System.IO.Path.GetFileName(Request.Files["FileUpload"].FileName.ToString());

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/App_Data/ExcelFiles"), fileName);

                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }
                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }

                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();
                        string exquery;
                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }

                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            DataSet ds = new DataSet();
                            int sheets = k + 1;

                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                            exquery = string.Format("Select * from [{0}]", excelSheets[k]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(exquery, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }

                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    List<Manufacture> mlist = new List<Manufacture>();

                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        Manufacture mItem = new Manufacture();
                                        if (ds.Tables[0].Rows[i]["Manufacturer_Name"] != null)
                                        {
                                            mItem.Manufacturer_Name = ds.Tables[0].Rows[i]["Manufacturer_Name"].ToString();
                                        }
                                        else
                                        {
                                            return Json(new { success = false, Error = "Manufacture name cannot be null it the excel sheet" }, JsonRequestBehavior.AllowGet);
                                        }

                                        if (ds.Tables[0].Rows[i]["Level"] != null)
                                        {
                                            mItem.Manufacture_Level = Convert.ToInt32(ds.Tables[0].Rows[i]["Level"]);
                                        }
                                        else
                                        {
                                            return Json(new { success = false, Error = "Level field cannot be null in the excel sheet" }, JsonRequestBehavior.AllowGet);
                                        }
                                        if (ds.Tables[0].Rows[i]["IsActive"] != null)
                                        {
                                            mItem.IsActive = ds.Tables[0].Rows[i]["IsActive"].ToString();
                                        }
                                        else
                                        {
                                            return Json(new { success = false, Error = "IsActive field cannot be null in the excel sheet" }, JsonRequestBehavior.AllowGet);
                                        }
                                        mItem.Created_User_Id = 1; //GetUserId();
                                        mItem.Created_Branc_Id = 2; //GetBranchId();
                                        mItem.Created_Dte = DateTime.Now;
                                        mItem.Modified_User_Id = 2; //GetUserId();
                                        mItem.Modified_Branch_Id = 2; //GetBranchId();
                                        mItem.Modified_Dte = DateTime.Now;
                                        mlist.Add(mItem);
                                    }

                                    if (manufactureDb.InsertFileUploadDetails(mlist))
                                    {
                                        return Json(new { success = true, Message = "File Uploaded Successfully" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, Error = "Excel file is empty" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, Error = "File Upload failed :" + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }

            return RedirectToAction("Index", "Purchase");
        }

        public PartialViewResult _CreatePartial()
        {
            return PartialView();
        }

        public PartialViewResult _EditPartial(int id)
        {
            PurchaseViewModels model = new PurchaseViewModels();
            model.PurchaseQuotation = purchaseDb.FindOneQuotationById(id);
            model.PurchaseQuotationItem = purchaseDb.FindOneQuotationItemById(id);
            return PartialView(model);
        }

        public PartialViewResult _ViewPartial(int id)
        {
            PurchaseViewModels model = new PurchaseViewModels();
            model.PurchaseQuotation = purchaseDb.FindOneQuotationById(id);
            model.PurchaseQuotationItem = purchaseDb.FindOneQuotationItemById(id);
            return PartialView(model);
        }

    }
}