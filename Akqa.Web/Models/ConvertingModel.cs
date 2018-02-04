using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Akqa.Web.Models
{
    public class ConvertingModel
    {
        [Required]
        public string Name { get; set;}

        [Required]
        public decimal Number { get; set; }        
    }
}