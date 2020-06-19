
using System.Collections.Generic;

namespace FirstAppMVC.Models
{
    public class BasketItemCreate
    {
        public int ProductId { get; set; }
        public int Count { get; set; }

        public BasketItemCreate(int productId, int count)
        {
            ProductId = productId;
            Count = count;
        }

    }
}
