﻿using GrennyWebApplication.Database.Models.Common;
using GrennyWebApplication.Database.Models.Enums;
namespace GrennyWebApplication.Database.Models
{
    public class Order : BaseEntity<string>, IAuditable
    {
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
         
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
