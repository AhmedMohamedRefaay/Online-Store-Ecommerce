using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TagesName
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="TageName")]
        public string Name { get; set;}
    
    }
}
