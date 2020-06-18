using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class ProductEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Brand")]
        public int? BrandId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }
    }
}
