using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troy.Model.Purchase
{
    [Table("tblPurchaseQuotation")]
    public class PurchaseQuotation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PurchaseId")]
        public int Purchase_Quote_Id { get; set; }

        [Required]
        [Display(Name = "Vendor")]
        public int Vendor { get; set; }

        [Required]
        [Display(Name = "Reference No")]
        public string Reference_Number { get; set; }

        [Display(Name = "Quotation Status")]
        public string Quotation_Status { get; set; }

        [Required]
        [Display(Name = "Posting Date")]
        public DateTime Posting_Date { get; set; }

        [Required]
        [Display(Name = "Valid Date")]
        public DateTime Valid_Date { get; set; }

        [Display(Name = "Required Date")]
        public DateTime Required_Date { get; set; }

        [Required]
        [Display(Name = "Ship To")]
        public int Ship_To { get; set; }

        [Required]
        public int? Fright { get; set; }

        [Required]
        public int? Loading { get; set; }


        public int? Discount { get; set; }

        public string Remarks { get; set; }

        public int Creating_Branch { get; set; }

        public int Created_User_Id { get; set; }

        public int Created_Branc_Id { get; set; }

        public DateTime Created_Date { get; set; }

        public int Modified_User_Id { get; set; }

        public int Modified_Branch_Id { get; set; }

        public DateTime Modified_Date { get; set; }

    }
}
