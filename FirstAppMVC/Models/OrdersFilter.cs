using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class OrdersFilter
    {

        [Display(Name = "Price From")]
        public decimal? PriceFrom { get; set; }

        [Display(Name = "Price To")]
        public decimal? PriceTo { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
