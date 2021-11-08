using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.Data.Model
{
    public class OrderDetailModel
    {
        public Guid OrderId { set; get; }
        public Guid ProductId { set; get; }
        public int Quantity { set; get; }
        public float Price { set; get; }
    }
}
