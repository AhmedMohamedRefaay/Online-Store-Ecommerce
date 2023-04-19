﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ProductType
    {
       public int Id { get; set; }

        [Required]
        [Display(Name ="ProductType")]
       public string productType { get; set; }
    }
}
