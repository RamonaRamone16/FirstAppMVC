using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class BrandCreateModel
    {
        [Required(ErrorMessage = "Поле Name дложно быть заполнено")]
        public string Name { get; set; }
    }
}
