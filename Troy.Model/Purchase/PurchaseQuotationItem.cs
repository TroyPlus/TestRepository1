using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troy.Model.Purchase
{
    [Table("tblPurchaseQuotationItem")]
    public class PurchaseQuotationItem
    {
        [Key]
        public int Purchase_Quote_Id { get; set; }

        public int Product_id { get; set; }

        [Required]
        [Display(Name = "Required Quantity")]
        public int Required_qty { get; set; }

        [Required]
        [Display(Name = "Required Date")]
        public DateTime Required_date { get; set; }

        public int Quoted_qty { get; set; }

        public DateTime Quoted_date { get; set; }

        public decimal Discount_percent { get; set; }

        [Required]
        [Display(Name = "VAT Code")]
        public int Vat_Code { get; set; }

        public decimal Unit_price { get; set; }

        public int Created_User_Id { get; set; }

        public int Created_Branc_Id { get; set; }

        public DateTime Created_Date { get; set; }

        public int Modified_User_Id { get; set; }

        public int Modified_Branch_Id { get; set; }

        public DateTime Modified_Date { get; set; }

    }
}
