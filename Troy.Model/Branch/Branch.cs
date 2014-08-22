using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Troy.Model.Branch
{
    [Table("tblBranch")]
    public class Branch
    {
        [Key]
        public int Branch_Id { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Branch Code is required.")]
        [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = @"Special characters ( ,@/)(=][|\!`’%$#^”&* ) are not allowed in the name.")]
        [StringLength(3)]
        [Column(TypeName = "char")]
        [Display(Name = "Branch Code")]
        [Remote("CheckForDuplication", "Branch")]
        public string Branch_Cde { get; set; }

        [Required(ErrorMessage = "Branch Name is required.")]
        [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = @"Special characters ( ,@/)(=][|\!`’%$#^”&* ) are not allowed in the name.")]
        [StringLength(30)]
        [Column(TypeName = "char")]
        public string Branch_Name { get; set; }

        [Required(ErrorMessage = "Branch Address is required.")]
        [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = @"Special characters ( / - ) are not allowed in the name.")]
        [StringLength(50)]
        [Column(TypeName = "char")]
        public string Address1 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "char")]
        public string Address2 { get; set; }

        [StringLength(50)]
        [Column(TypeName = "char")]

        public string Address3 { get; set; }

        [Required]
        [StringLength(3)]
        [Column(TypeName = "char")]
        public string Country_Cde { get; set; }

        [Required]
        [StringLength(3)]
        [Column(TypeName = "char")]
        public string State_Cde { get; set; }

        [Required]
        [StringLength(3)]
        [Column(TypeName = "char")]
        public string City_Cde { get; set; }

        [Required(ErrorMessage = "PinCode is required.")]
        [RegularExpression(@"^[0-9 + /'' ']+$", ErrorMessage = @"Special characters ( / - ) are allowed in the code.")]

        [StringLength(10)]
        [Column(TypeName = "char")]
        public string Pin_Cod { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Order number is required.")]
        [Display(Name = "Order number")]
        [Remote("CheckForDuplication", "Branch")]
        public int Order_Num { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char")]
        public string IsActive { get; set; }

        [Required]
        public int Created_User_Id { get; set; }

        [Required]
        public int Created_Branc_Id { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Created_Dte { get; set; }
        [Required]
        public int Modified_User_Id { get; set; }

        [Required]
        public int Modified_Branch_Id { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Modified_Dte { get; set; }


    }

    [Table("tblCountry")]
    public class Country
    {
        [Key]
        public int Country_Id { get; set; }

        [StringLength(3)]
        [Column(TypeName = "char")]
        public string Country_Cde { get; set; }

        [StringLength(30)]
        [Column(TypeName = "char")]
        public string Country_Name { get; set; }
        [StringLength(30)]

        [Column(TypeName = "char")]
        public string SAP_Country_Cde { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char")]
        public string IsDefault { get; set; }
    }


    [Table("tblState")]
    public class State
    {
        [Key]
        public int State_Id { get; set; }

        [StringLength(3)]
        [Column(TypeName = "char")]
        public string State_Cde { get; set; }
        [StringLength(30)]
        [Column(TypeName = "char")]
        public string State_Name { get; set; }
        [StringLength(30)]
        [Column(TypeName = "char")]
        public string SAP_State_Cde { get; set; }
        [StringLength(3)]
        [Column(TypeName = "char")]
        public string Country_Cde { get; set; }
        [StringLength(1)]
        [Column(TypeName = "char")]
        public string IsDefault { get; set; }
    }


    [Table("tblCity")]
    public class City
    {
        [Key]
        public int City_Id { get; set; }

        [StringLength(3)]
        [Column(TypeName = "char")]
        public string City_Cde { get; set; }
        [StringLength(30)]
        [Column(TypeName = "char")]
        public string City_Name { get; set; }
        [StringLength(3)]
        [Column(TypeName = "char")]
        public string State_Cde { get; set; }
        [StringLength(3)]
        [Column(TypeName = "char")]
        public string Country_Cde { get; set; }
        [StringLength(1)]
        [Column(TypeName = "char")]
        public string IsDefault { get; set; }
    }
}
