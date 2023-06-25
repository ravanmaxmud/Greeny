﻿using GrennyWebApplication.Areas.Admin.ViewModels.Role;
using GrennyWebApplication.Database.Models;


namespace GrennyWebApplication.Areas.Admin.ViewModels.User
{
    public class UserAddViewModel
    {

        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? RoleId { get; set; }
        public List<RoleViewModel>? Roles { get; set; }   
    }
}
