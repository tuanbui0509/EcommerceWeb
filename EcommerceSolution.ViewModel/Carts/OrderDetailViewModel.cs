using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.ViewModels.Carts
{
    public class OrderDetailViewModel
    {
        public Guid OrderId { set; get; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}