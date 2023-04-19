using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
 
namespace WebApplication1.Models
{
    public class Products
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }


        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        public string Img { get; set; }

        [Display(Name = "Product Color")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public virtual ProductType prodType { get; set; }

        [Required]
        [Display(Name = "Tage Name")]
        public int TagesNameId { get; set; }

       [ForeignKey("TagesNameId")]
        public virtual TagesName Tages { get; set; }
    }
}
