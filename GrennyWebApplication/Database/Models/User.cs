﻿using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class User : BaseEntity<Guid>, IAuditable
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Basket? Basket { get; set; }

        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public UserActivation? UserActivation { get; set; }

        public List<Order>? Orders { get; set; }
        public List<PasswordForget>? PasswordForget { get; set; }

    }
}
