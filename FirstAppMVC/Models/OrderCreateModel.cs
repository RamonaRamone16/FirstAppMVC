using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class OrderCreateModel
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public string Brand { get; set; }

        [Display(Name = "Product Price")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Укажите количество")]
        [Range(1, Double.PositiveInfinity, ErrorMessage = "Количество не может быть отрицательным или нулевым")]
        public int Count { get; set; }
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Data { get; set; }
    }
}
