using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class ProductCreateModel
    {
        [Required(ErrorMessage = "Поле Name не должно быть пустым")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Price не должно быть пустым")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        public SelectList CategorySelect { get; set; }
        public SelectList BrandSelect { get; set; }
    }
}
