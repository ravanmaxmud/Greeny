﻿using GrennyWebApplication.Areas.Admin.ViewModels.Role;

namespace GrennyWebApplication.Areas.Admin.ViewModels.User
{
    public class UserUpdateViewModel
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? RoleId { get; set; }
        public List<RoleViewModel>? Roles { get; set; }
    }
}
