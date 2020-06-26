using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class ProductFilter
    {
        public string Name { get; set; }

        [Display(Name = "Price From")]
        public decimal? PriceFrom { get; set; }

        [Display(Name = "Price To")]
        public decimal? PriceTo { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        public List<ProductModel> Products { get; set; }
        public List<ProductModel> ViewedProducts { get; set; }
        public SortDirection SortDirection { get; set; }
        public SortType SortType { get; set; }
        public int? CurrentPage { get; set; }
        public PagingModel Paging { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }
    }
}
