using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstAppMVC.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string Basket { get; set; }
    }
}
