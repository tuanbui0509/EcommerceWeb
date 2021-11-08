using EcommerceSolution.Data.EntityBase;
using EcommerceSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class Order : AuditEntity<Guid>
    {
        //public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public OrderStatus Status { set; get; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public AppUser AppUser { get; set; }


    }
}
